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

            var nodes = new CFGBlockNode[scopes.Count];
            for(var x = 0; x < scopes.Count; x++)
                nodes[x] = new CFGBlockNode(index++, scopes[x]);

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
                if (scope.Case0 != -1) edges.Add(Edge(nodes[i], nodes[nexts[scope.Case0]]));
                if (scope.Case1 != -1) edges.Add(Edge(nodes[i], nodes[nexts[scope.Case1]]));
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
