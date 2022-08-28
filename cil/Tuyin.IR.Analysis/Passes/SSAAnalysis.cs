using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Tuyin.IR.Analysis.Data;
using Tuyin.IR.Analysis.Data.Instructions;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;
using E = System.Linq.Expressions.Expression;

namespace Tuyin.IR.Analysis.Passes
{
    class SSAAnalysis : AstVisitor<SSA>, IAnalysis<SSAAnalysisOpation, IReadOnlyList<Statment>>, IDisposable
    {
        private int mAliasIndex;
        private int mStatmentIndex;
        private int mStatmentOffset;
        private VisitMode mVisitMode;
        private Branch mBranchInfo;
        private TwoKeyDictionary<int, string, SSAReference> mDefUse;

        internal SSAAnalysis()
        {
            mDefUse = new TwoKeyDictionary<int, string, SSAReference>();
        }

        private void ResetParams()
        {
            mDefUse.Clear();
            mAliasIndex = 0;
            mStatmentIndex = -1;
            mStatmentOffset = 0;
            mVisitMode = VisitMode.Get;
        }

        public IReadOnlyList<Statment> Run(SSAAnalysisOpation input)
        {
            ResetParams();

            var ssas = new List<Statment>();
            var inserts = new List<SSAIntertStatment>();
            var statments = new List<Statment>(input.Statments);
            mBranchInfo = input.Branch;

            var list = new List<int>();
            var stacks = new Stack<(int index, int start)>();
            stacks.Push((mBranchInfo.StatmentParentBranches[0], 0));
            for (var i = 0; i < mBranchInfo.StatmentParentBranches.Length; i++)
            {
                var index = mBranchInfo.StatmentParentBranches[i];
            RECHECK:
                var curr = stacks.Peek();
                if (curr.index < index)
                {
                    // 进入
                    stacks.Push((index, list.Count));
                }
                else if (curr.index > index)
                {
                    // 退出当前
                    var item = stacks.Pop();
                    var count = list.Count - item.start;
                    var range = list.GetRange(item.start - 1, count + 1);
                    list.RemoveRange(item.start, count);

                    var lefts = range.Distinct().ToArray();
                    var phi = new SSAPhi(item.index, lefts);
                    var insert = new SSAIntertStatment(i, i, phi);
                    inserts.Add(insert);

                    if (stacks.Count > 0) goto RECHECK;
                }

                list.Add(mBranchInfo.StatmentBranches[i]);
            }

            // 声明可变label
            int insertIndex = 0;
            for (int i = 0; i < statments.Count; i++)
            {
                var stmt = statments[i];
                SSALabel label = null;
                switch (stmt.NodeType)
                {
                    case AstNodeType.Goto:
                        var br = statments[i] as Goto;
                        insertIndex = br.Label.Index;
                        label = new SSALabel();
                        statments[i] = new Goto(label);
                        break;
                    case AstNodeType.Test:
                        var test = statments[i] as Test;
                        insertIndex = test.Label.Index;
                        label = new SSALabel();
                        statments[i] = new Test(label, test.Expression);
                        break;
                }

                if (label != null)
                    inserts.Add(new SSAIntertStatment(insertIndex - 1, insertIndex, new ResetLabel(label)));
            }

            // 排序并插入文法
            var insertsSort = inserts.OrderBy(x => x.InsertCompre).ToArray();
            for (var i = 0; i < insertsSort.Length; i++)
            {
                var insert = insertsSort[i];
                statments.Insert(insert.InsertIndex + i, insert.InsertStatment);
            }

            // 访问原始文法并生成ssa文法和use-def数据
            foreach (var stmt in statments)
            {
                var ssaStatment = Visit(stmt);
                if (ssaStatment != null)
                {
                    ssas.AddRange(ssaStatment.Statments);
                    mStatmentOffset = ssas.Count;
                }
            }

            return ssas;
        }

        public override SSA Visit(AstNode node)
        {
            SSA result = null;
            if (node is ResetLabel label)
                result = VisitLabel(label);
            else if (node is SSAPhi phi)
                result = VisitPhi(phi);
            else
                result = base.Visit(node);

            if (result?.Statment != null)
                result.Statment.SourceSpan = node.SourceSpan;

            return result;
        }

        public override SSA VisitStatement(Statment node)
        {
            mStatmentIndex++;
            var result = base.VisitStatement(node);

            if (result?.Statment != null)
                result.Statment.Metadatas = node.Metadatas;

            return result;
        }

        private SSA VisitLabel(ResetLabel label)
        {
            (label.Label as SSALabel).UpdateIndex(mStatmentOffset);
            return null;
        }

        private SSA VisitPhi(SSAPhi phi)
        {
            // 查找分支相同变量引用
            var branches = phi.ForwardBranchs.Where(x => mDefUse.ContainsPrimaryKey(x)).SelectMany(x => mDefUse.GetDictionary2(x).Select(y => new { Name = y.Key, Branch = x, Use = y.Value })).GroupBy(x => x.Name).ToDictionary(x => x.Key, x => x.ToArray());

            // 查询交集
            var sets = new List<SSA>();
            foreach (var branch in branches)
            {
                var depends = branch.Value.Select(x => mDefUse[x.Branch, branch.Key].Source).ToArray();
                var index = mDefUse.ContainsKey(phi.BranchIndex, branch.Key) ? mDefUse[phi.BranchIndex, branch.Key].UseIndex + 1 : 0;
                var get = SSAReference.Create(null, mStatmentIndex, branch.Key, phi.BranchIndex, index);
                var set = new SSA(mStatmentIndex, true, null, new Store(get.Source, new Phi(depends)));

                mDefUse[phi.BranchIndex, branch.Key] = get;
                sets.Add(set);
            }

            return new SSA(mStatmentIndex, false, null, null, sets.ToArray());
        }

        public override SSA VisitTest(Test ast)
        {
            var cond = Visit(ast.Expression);
            if (cond.Value.ConstantExpression != null)
            {
                var v = E.Lambda(cond.Value.ConstantExpression).Compile().DynamicInvoke();
                if ((int)v >= 1)
                    return null;

                return new SSA(mStatmentIndex, true, ast, new Goto(ast.Label));
            }

            return new SSA(mStatmentIndex, true, ast, new Test(ast.Label, cond.Source), cond);
        }

        public override SSA VisitGoto(Goto ast)
        {
            return new SSA(mStatmentIndex, true, ast, new Goto(ast.Label));
        }

        public override SSA VisitAdd(Add ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            if (left.Value.ConstantExpression != null && right.Value.ConstantExpression != null)
            {
                var e = E.Add(left.Value.ConstantExpression, right.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = v is int ? new Integer((int)v) : new Float((float)v) as Expression;
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i));
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Add(left.Source, right.Source)), left, right);
        }

        public override SSA VisitAnd(And ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            if (left.Value.ConstantExpression != null && right.Value.ConstantExpression != null)
            {
                var e = E.And(left.Value.ConstantExpression, right.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = v is int ? new Integer((int)v) : new Float((float)v) as Expression;
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i));
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new And(left.Source, right.Source)), left, right);
        }

        public override SSA VisitCall(Call ast)
        {
            var args = ast.Arguments.Reverse().Select(x => Visit(x)).ToArray();
            var method = Visit(ast.Method);
            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Call(method.Source, args.Select(x => x.Source))), args);
        }

        public override SSA VisitDiv(Div ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            if (left.Value.ConstantExpression != null && right.Value.ConstantExpression != null)
            {
                var e = E.Divide(left.Value.ConstantExpression, right.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = v is int ? new Integer((int)v) : new Float((float)v) as Expression;
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i));
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Div(left.Source, right.Source)), left, right);
        }

        public override SSA VisitElement(Element ast)
        {
            var index = Visit(ast.Index);
            var source = Visit(ast.Source);
            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Element(index.Source, source.Source)), index, source);
        }

        public override SSA VisitEqual(Equal ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Equal(left.Source, right.Source)), left, right);
        }

        public override SSA VisitFloat(Float ast)
        {
            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), ast));
        }

        public override SSA VisitGreaterThen(GreaterThen ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            if (left.Value.ConstantExpression != null && right.Value.ConstantExpression != null)
            {
                var e = E.GreaterThan(left.Value.ConstantExpression, right.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = (bool)v ? new Integer(1) : new Integer(0) as Expression;
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i));
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new GreaterThen(left.Source, right.Source)), left, right);
        }

        public override SSA VisitIdentifier(Identifier ast)
        {
            var refer = GetSSAReference(mVisitMode, null, ast.Value, mStatmentIndex);
            if (refer.Value.ConstantExpression != null)
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), refer.Value));

            return refer;
        }

        public override SSA VisitInteger(Integer ast)
        {
            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), ast));
        }

        public override SSA VisitLeftShift(LeftShift ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            if (left.Value.ConstantExpression != null && right.Value.ConstantExpression != null)
            {
                var e = E.LeftShift(left.Value.ConstantExpression, right.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = v is int ? new Integer((int)v) : new Float((float)v) as Expression;
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i));
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new LeftShift(left.Source, right.Source)), left, right);
        }

        public override SSA VisitLessThen(LessThen ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            if (left.Value.ConstantExpression != null && right.Value.ConstantExpression != null)
            {
                var e = E.LessThan(left.Value.ConstantExpression, right.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = (bool)v ? new Integer(1) : new Integer(0) as Expression;
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i));
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new LessThen(left.Source, right.Source)), left, right);
        }

        public override SSA VisitLiteral(Literal ast)
        {
            var members = ast.Members.ToDictionary(x => x.Key, x => Visit(x.Value));
            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Literal(members.ToDictionary(x => x.Key, x => x.Value.Source))));
        }

        public override SSA VisitMember(Member ast)
        {
            var tmp = mVisitMode;
            mVisitMode = VisitMode.Get;
            var src = Visit(ast.Source) as SSAReference;
            mVisitMode = tmp;

            return GetSSAReference(mVisitMode, src, ast.Field.Value, mStatmentIndex);

            if (ast.Source.NodeType == AstNodeType.Identifier)
                src = GetSSAReference(mVisitMode, src, ast.Field.Value, mStatmentIndex);

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Member(src.Source, ast.Field)), src);
        }

        public override SSA VisitMul(Mul ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            if (left.Value.ConstantExpression != null && right.Value.ConstantExpression != null)
            {
                var e = E.Multiply(left.Value.ConstantExpression, right.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = v is int ? new Integer((int)v) : new Float((float)v) as Expression;
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i));
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Mul(left.Source, right.Source)), right, left);
        }

        public override SSA VisitNeg(Neg ast)
        {
            var source = Visit(ast.Source);
            if (source.Value.ConstantExpression != null)
            {
                var e = E.Negate(source.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = v is bool ? new Integer((bool)v ? 0 : 1) : (v is int ? new Integer((int)v) : new Float((float)v) as Expression);
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i), source);
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Neg(source.Source)), source);
        }

        public override SSA VisitOr(Or ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            if (left.Value.ConstantExpression != null && right.Value.ConstantExpression != null)
            {
                var e = E.Or(left.Value.ConstantExpression, right.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = v is int ? new Integer((int)v) : new Float((float)v) as Expression;
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i));
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Or(left.Source, right.Source)), left, right);
        }

        public override SSA VisitRem(Rem ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            if (left.Value.ConstantExpression != null && right.Value.ConstantExpression != null)
            {
                var e = E.Modulo(left.Value.ConstantExpression, right.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = v is int ? new Integer((int)v) : new Float((float)v) as Expression;
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i));
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Rem(left.Source, right.Source)), left, right);
        }

        public override SSA VisitSub(Sub ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            if (left.Value.ConstantExpression != null && right.Value.ConstantExpression != null)
            {
                var e = E.Subtract(left.Value.ConstantExpression, right.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = v is int ? new Integer((int)v) : new Float((float)v) as Expression;
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i));
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Sub(left.Source, right.Source)), left, right);
        }

        public override SSA VisitRightShift(RightShift ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            if (left.Value.ConstantExpression != null && right.Value.ConstantExpression != null)
            {
                var e = E.RightShift(left.Value.ConstantExpression, right.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = v is int ? new Integer((int)v) : new Float((float)v) as Expression;
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i));
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new RightShift(left.Source, right.Source)), left, right);
        }

        public override SSA VisitString(Reflection.Instructions.String ast)
        {
            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), ast));
        }

        public override SSA VisitXor(Xor ast)
        {
            var right = Visit(ast.Right);
            var left = Visit(ast.Left);
            if (left.Value.ConstantExpression != null && right.Value.ConstantExpression != null)
            {
                var e = E.Power(left.Value.ConstantExpression, right.Value.ConstantExpression).Reduce();
                var v = E.Lambda(e).Compile().DynamicInvoke();
                var i = v is int ? new Integer((int)v) : new Float((float)v) as Expression;
                return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), i));
            }

            return new SSA(mStatmentIndex, true, ast, new Store(new Address(CreateAlias()), new Xor(left.Source, right.Source)), right, left);
        }

        public override SSA VisitStore(Store ast)
        {
            mVisitMode = VisitMode.Get;
            var val = Visit(ast.Value);
            mVisitMode = VisitMode.Set;
            var src = Visit(ast.Source);
            mVisitMode = VisitMode.Get;

            if (val.Dependencies.Count == 0)
                return new SSA(mStatmentIndex, true, ast, new Store(src.Value, val.Value));

            return new SSA(mStatmentIndex, true, ast, new Store(src.Source, val.Source), src, val);
        }

        public override SSA VisitReturn(Return ast)
        {
            var val = Visit(ast.Expression);
            if(val.Value.ConstantExpression != null)
                return new SSA(mStatmentIndex, true, ast, new Return(val.Value));

            return new SSA(mStatmentIndex, true, ast, new Return(val.Source), val);
        }

        private SSAReference GetSSAReference(VisitMode mode, SSAReference parent, string id, int stmtIndex) 
        {
            if (string.IsNullOrWhiteSpace(id))
                id = CreateAlias();

            var key = id;
            if (parent != null)
                key = $"{parent.Source}.{id}";

            var bi = mBranchInfo.StatmentBranches[stmtIndex];
            if (mode == VisitMode.Get)
            {
                var ci = bi;
                while (true)
                {
                    if (mDefUse.ContainsKey(ci, id))
                    {
                        bi = ci;
                        break;
                    }

                    ci = mBranchInfo.BranchParentBranches[ci];
                    if (ci == 0)
                        break;
                }
            }

            if (!mDefUse.ContainsKey(bi, key))
                mDefUse[bi, key] =
                  SSAReference.Create(
                      parent?.Reference as Address,
                      stmtIndex,
                      id,
                      bi,
                      mode == VisitMode.Get ? 0 : -1);

            var refer = mDefUse[bi, key];
            if (mode == VisitMode.Set)
                mDefUse[bi, key] = refer
                    = SSAReference.Create(
                        parent?.Reference as Address,
                        stmtIndex,
                        id,
                        refer.BranchIndex,
                        refer.UseIndex + 1);

            return refer;
        }

        private string CreateAlias()
        {
            return CreateAlias(string.Empty);
        }

        private string CreateAlias(string prefix)
        {
            return $"%{prefix}{Convert.ToString(mAliasIndex++, 16)}";
        }

        public void Dispose()
        {
            mDefUse = null;
        }

        enum VisitMode
        {
            Get,
            Set
        }

        class SSAPhi : Statment
        {
            public SSAPhi(int key, IReadOnlyList<int> forwards)
            {
                BranchIndex = key;
                ForwardBranchs = forwards;
            }

            public override AstNodeType NodeType => AstNodeType.Store;

            public IReadOnlyList<int> ForwardBranchs { get; }

            public int BranchIndex { get; }

            public override IEnumerable<AstNode> GetNodes()
            {
                yield return this;
            }
        }

        class SSAReference : SSA, IEquatable<SSAReference>
        {
            private SSAReference(int stmtIndex, Address addr)
                : base(stmtIndex, false, addr, null)
            {
                Identifier = addr.Identifier;
                UseIndex = addr.UseIndex;
                BranchIndex = addr.BranchIndex;
            }

            public override Expression Source => Reference as Expression;

            public override Expression Value => Source;

            public string Identifier { get; }

            public int BranchIndex { get; }

            public int UseIndex { get; }

            public override bool Equals(object obj)
            {
                return obj is SSAReference get && Equals(get);
            }

            public bool Equals(SSAReference other)
            {
                return Identifier == other.Identifier &&
                       BranchIndex == other.BranchIndex &&
                       UseIndex == other.UseIndex;
            }

            public override int GetHashCode()
            {
                return HashCode.Combine(Identifier, BranchIndex, UseIndex);
            }

            public override string ToString()
            {
                return $"{Identifier}.{BranchIndex}.{UseIndex}";
            }

            public static SSAReference Create(Address parent, int stmtIndex, string identifier, int branchIndex, int useIndex)
            {
                return new SSAReference(stmtIndex, new Address(parent, identifier, branchIndex, useIndex));
            }
        }

        class SSALabel : Label
        {
            private int mTarget;

            public SSALabel()
            {
                mTarget = -1;
            }

            public override int Index => mTarget;

            internal void UpdateIndex(int index)
            {
                Debug.Assert(mTarget == -1, "Can not set ssa label two times.");
                mTarget = index;
            }
        }

        struct SSAIntertStatment
        {
            public SSAIntertStatment(int insertCompre, int insertIndex, Statment insertStatment)
            {
                InsertCompre = insertCompre;
                InsertIndex = insertIndex;
                InsertStatment = insertStatment;
            }

            public int InsertCompre { get; }

            public int InsertIndex { get; }

            public Statment InsertStatment { get; }
        }
    }

    class SSAAnalysisOpation 
    {
        public SSAAnalysisOpation(Branch branch, IReadOnlyList<Statment> input)
        {
            Branch = branch;
            Statments = input;
        }

        public Branch Branch { get; }

        public IReadOnlyList<Statment> Statments { get; }
    }
}
