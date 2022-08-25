using System.Collections.Generic;
using System.Diagnostics;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;

namespace Tuyin.IR.Analysis.Data
{
    class SSA
    {
        private bool mPush;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual Expression Source => Statment.NodeType == AstNodeType.Store ? (Statment as Store).Source : null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public virtual Expression Value => Statment.NodeType == AstNodeType.Store ? (Statment as Store).Value : null;

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public int StatmentIndex { get; private set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public Statment Statment { get; private set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public IReadOnlyList<SSA> Dependencies { get; private set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public AstNode Reference { get; private set; }

        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public SourceSpan SourceSpan => Statment.SourceSpan;

        public IReadOnlyList<Statment> Statments { get; private set; }

        public SSA(int statmentIndex, bool push, AstNode refer, Statment stmt, params SSA[] dependencies)
        {
            CreteStatment(statmentIndex, push, refer, stmt, dependencies);
        }

        public SSA(int statmentIndex, bool push, AstNode refer, Statment stmt, IReadOnlyList<SSA> dependencies)
        {
            CreteStatment(statmentIndex, push, refer, stmt, dependencies);
        }

        void CreteStatment(int statmentIndex, bool push, AstNode refer, Statment stmt, IReadOnlyList<SSA> dependencies)
        {
            mPush = push;
            StatmentIndex = statmentIndex;
            Reference = refer;
            Statment = stmt;
            Dependencies = dependencies;

            var statments = new List<Statment>();
            BuildDependenices(statments, this);
            Statments = statments.ToArray();
        }

        protected void BuildDependenices(List<Statment> stmts, SSA ssa)
        {
            foreach (var depend in ssa.Dependencies)
                BuildDependenices(stmts, depend);

            // 尾递归优化?
            if (ssa.mPush)
                stmts.Add(ssa.Statment);
        }

        public override string ToString()
        {
            return $"{ToStringWithoutSpan()}".PadRight(20) + $"{SourceSpan}";
        }

        public string ToStringWithoutSpan() 
        {
            return Reference == null ? Statment.ToString() : Reference.ToString();
        }
    }
}
