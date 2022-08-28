using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Tuyin.IR.Analysis.Data;
using Tuyin.IR.Reflection;

namespace Tuyin.IR.Analysis.Passes
{
    class CFGAnalysis : IAnalysis<CFGAnalysisOpation, CFG>
    {
        public CFG Run(CFGAnalysisOpation input)
        {
            ushort index = 1;
            var metadatas = new Metadatas();
            var scopes = new ScopeAnalysis().Run(new ScopeAnalysisOpation(input.Branch, input.Statments));
            var edges = DynamicArray<AnalysisEdge>.Create(1 + scopes.Count * 2);

            var blocks = new bool[input.Statments.Count];
            var nodes = new CFGBlockNode[scopes.Count];
            for (var x = 0; x < scopes.Count; x++)
            {
                var scope = scopes[x];
                var vaild = scope.End;
                for (var i = scope.Start; i < scope.End; i++) 
                {
                    var stmt = input.Statments[i];
                    if (stmt.NodeType == AstNodeType.Return || stmt.NodeType == AstNodeType.Goto)
                    {
                        vaild = i + 1;
                        blocks[input.Branch.StatmentBranches[i]] = true;
                        break;
                    }
                }

                nodes[x] = new CFGBlockNode(index++, vaild, scope);
            }

            var nexts = new int[input.Statments.Count];
            for (var i = 0; i < scopes.Count; i++)
            {
                var scope = scopes[i];
                for (var x = scope.Start; x < scope.End; x++)
                    nexts[x] = i;
            }

            for (var i = 0; i < scopes.Count; i++)
            {
                var scope = scopes[i];
                if (scope.Case0 != -1 && scope.Case1 < nexts.Length && !blocks[input.Branch.StatmentBranches[scope.Case0]]) 
                    edges.Add(Edge(nodes[i], nodes[nexts[scope.Case0]]));

                if (scope.Case1 != -1 && scope.Case1 < nexts.Length && !blocks[input.Branch.StatmentBranches[scope.Case1]])
                    edges.Add(Edge(nodes[i], nodes[nexts[scope.Case1]]));
            }


            return new CFG(edges, nodes, input.Statments, metadatas);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private AnalysisEdge Edge(CFGNode left, CFGNode right)
        {
            var edge = new AnalysisEdge(default, left, right);
            left.Rights.Add(edge);
            right.Lefts.Add(edge);
            return edge;
        }
    }

    class CFGAnalysisOpation 
    {
        public CFGAnalysisOpation(IReadOnlyList<Statment> input)
            : this(new BranchAnalysis().Run(input), input)
        {
        }

        public CFGAnalysisOpation(Branch branch, IReadOnlyList<Statment> input)
        {
            Branch = branch;
            Statments = input;
        }

        public Branch Branch { get; }

        public IReadOnlyList<Statment> Statments { get; }

    }
}
