using libgraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using Tuyin.IR.Analysis.Data;
using Tuyin.IR.Analysis.Data.Instructions;
using Tuyin.IR.Reflection;
using Tuyin.IR.Reflection.Instructions;
using String = Tuyin.IR.Reflection.Instructions.String;

namespace Tuyin.IR.Analysis.Passes
{
    class DAGAnalysis : IAnalysis<DAGAnalysisOpation, DAG>
    {
        public DAG Run(DAGAnalysisOpation input)
        {
            ushort index = 1;
            var nodes = new Dictionary<string, DAGNode>();
            var edges = DynamicArray<AnalysisEdge>.Create(input.CFG.Statments.Count * 3);

            DAGStoreNode GetParent(Address addr, int si) 
            {
                if (addr == null || addr.Parent == null)
                    return null;

                if (!nodes.ContainsKey(addr.Parent.Value))
                    nodes[addr.Parent.Value] = new DAGStoreNode(addr.Parent, index++, si, true);

                return nodes[addr.Parent.Value] as DAGStoreNode;
            }

            DAGNode GetState(AstNode node, int si) 
            {
                if (node?.NodeType == AstNodeType.Identifier)
                {
                    DAGNode state = null;

                    var parent = GetParent(node as Address, si);
                    var addr = node as Address;
                    if (!nodes.ContainsKey(addr.Value))
                    {
                        state = nodes[addr.Value] = new DAGStoreNode(addr, index++, si, true);

                        if (parent != null)
                            parent.Childrens.Add(state);
                    }
                    else state = nodes[addr.Value];

                    return state;
                }

                var name = new Address($"Auto[{index}]");
                var code = new Microcode(GetOpCode(node), GetOperand(node, input.CFG.Metadatas));
                var dag = new DAGMicrocodeNode(name, index++, si, code, false);
                nodes[name.Value] = dag;
                return dag;
            }

            AnalysisEdge CreateEdge(AnalysisNode left, AstNode ast, int si)
            {
                DAGNode subset = null;

                var right = GetState(ast, si);
                if (right is DAGStoreNode store)
                {
                    var childrens = store.Childrens.Columns.Values.Select(x => x.Node).ToArray();
                    subset = new DAGLoadNode(null, index++, si, false, store, childrens);
                    foreach (var child in childrens)
                        child.ReferenceAmount++;
                }
                var edge = new AnalysisEdge(EdgeFlags.None, subset, left, right, ast.SourceSpan);
                left.Rights.Add(edge);
                right.Lefts.Add(edge);
                return edge;
            }

            bool[] vailds = new bool[input.CFG.Statments.Count];
            if (input.Settings.ClearInvaildStatment)
            {
                for (var i = 0; i < input.CFG.Vertices.Count; i++)
                {
                    var scope = input.CFG.Vertices[i];
                    for (var x = scope.Scope.Start; x < scope.Vaild; x++)
                    {
                        vailds[x] = true;
                    }
                }
            }

            for (var i = 0; i < input.CFG.Statments.Count; i++) 
            {
                if (input.Settings.ClearInvaildStatment && !vailds[i])
                    continue;

                var stmt = input.CFG.Statments[i];
                if (stmt is Store store)
                {
                    var id = store.Source as Identifier;
                    var val = store.Value;
                    var left = GetState(id, i);
                    if (val is Phi phi)
                    {
                        var rights = phi.Eexpressions.Cast<Identifier>();
                        foreach (var rid in rights)
                            edges.Add(CreateEdge(left, rid, i));
                    }
                    else
                    {
                        var rights = val.GetNodes().ToArray();
                        switch (rights.Length)
                        {
                            case 1:
                                edges.Add(CreateEdge(left, rights[0], i));
                                break;
                            case 2:
                                edges.Add(CreateEdge(left, rights[1], i));
                                edges.Add(CreateEdge(edges[^1].Target, rights[0], i));
                                break;
                            case 3:
                                edges.Add(CreateEdge(left, rights[2], i));
                                edges.Add(CreateEdge(edges[^1].Target, rights[0], i));
                                edges.Add(CreateEdge(edges[^2].Target, rights[1], i));
                                break;
                        }
                    }
                }
                else if(stmt is Return ret && ret.Expression != null)
                    edges.Add(CreateEdge(GetState(ret, i), ret.Expression, i));
            }

            // 清理引用计数
            if (input.Settings.ClearInvaildMember)
            {
                foreach (var key in nodes.Keys.ToArray())
                {
                    var node = nodes[key];
                    if (node.Parent != null && node.ReferenceAmount == 0)
                    {
                        nodes.Remove(key);
                        foreach (var left in node.Lefts)
                        {
                            left.Source.Rights.Remove(left);
                            edges.Remove(left);
                        }

                        foreach (var right in node.Rights)
                        {
                            right.Target.Lefts.Remove(right);
                            edges.Remove(right);
                        }
                    }
                }
            }

            return new DAG(input.CFG.Metadatas, edges, nodes.Values.ToArray());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private Union8 GetOperand(AstNode node, Metadatas metadatas)
        {
            return node.NodeType switch
            {
                AstNodeType.Goto => new Union8() { Ushort0 = (ushort)(node as Goto).Label.Index },
                AstNodeType.Test =>  new Union8() { Ushort0 = (ushort)(node as Test).Label.Index },
                AstNodeType.Call => new Union8() { Byte0 = (byte)(node as Call).Arguments.Count },
                AstNodeType.Integer => new Union8() { Int0 = (int)(node as Integer).Value },
                AstNodeType.Float => new Union8() { Single0 = (float)(node as Float).Value },
                AstNodeType.String => new Union8() { Int0 = metadatas.GetMetadataIndex((node as String).Value) } ,
                AstNodeType.Member => new Union8() { Int0 = metadatas.GetMetadataIndex((node as Member).Field.Value) },
                AstNodeType.Identifier => new Union8() { Int0 = metadatas.GetMetadataIndex((node as Identifier).Value) },
                AstNodeType.Literal => new Union8() { Ushort0 = (ushort)(node as Literal).Members.Count },

                _ => default
            };
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private OpCode GetOpCode(AstNode node)
        {
            return node.NodeType switch
            {
                AstNodeType.Add => OpCode.Add,
                AstNodeType.And => OpCode.And,
                AstNodeType.Div => OpCode.Div,
                AstNodeType.Equal => OpCode.Ceq,
                AstNodeType.GreaterThen => OpCode.Cgt,
                AstNodeType.LeftShift => OpCode.Shl,
                AstNodeType.LessThen => OpCode.Clt,
                AstNodeType.Mul => OpCode.Mul,
                AstNodeType.Or => OpCode.Or,
                AstNodeType.Rem => OpCode.Rem,
                AstNodeType.RightShift => OpCode.Shr,
                AstNodeType.Sub => OpCode.Sub,
                AstNodeType.Xor => OpCode.Xor,
                AstNodeType.Neg => OpCode.Neg,
                AstNodeType.Goto => OpCode.Goto,
                AstNodeType.Test => OpCode.Test,
                AstNodeType.Return => OpCode.Ret,
                AstNodeType.Call => OpCode.Call,
                AstNodeType.Integer => OpCode.Ldc,
                AstNodeType.Float => OpCode.Ldr,
                AstNodeType.String => OpCode.Ldstr,
                AstNodeType.Element => OpCode.Ldelem,
                AstNodeType.Identifier => OpCode.Load,
                AstNodeType.Literal => OpCode.New,
                AstNodeType.Store => OpCode.Store,

                _ => throw new NotImplementedException()
            };
        }
    }

    class DAGAnalysisOpation
    {
        internal DAGAnalysisOpation(CFG cfg, EnvironmentSettings settings)
        {
            CFG = cfg;
            Settings = settings;
        }

        public CFG CFG { get; }

        public EnvironmentSettings Settings { get; }
    }
}
