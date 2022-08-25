using System;
using System.Collections.Generic;
using Tuyin.IR.Analysis.Data;

namespace Tuyin.IR.Analysis.Passes
{
    /// <summary>
    /// 强连通分量分析
    /// </summary>
    class SCCAnalysis : IAnalysis<SCCAnalysisOpation, SCC>
    {
        public unsafe SCC Run(SCCAnalysisOpation input)
        {
            var endPoints = DynamicArray<SCCRange>.Create(input.Edges.Count / 2);
            var visitor = new bool[input.Edges.Count];
            for (var i = 0; i < input.Edges.Count; i++) 
            {
                var edge = input.Edges[i];
                if (edge.Source.Index > edge.Target.Index) 
                {
                    Array.Fill(visitor, true, edge.Target.Index, edge.Source.Index - edge.Target.Index + 1);
                    endPoints.Add(new SCCRange(edge.Target.Index, edge.Source.Index + 1, true));
                }
            }

            for (var i = 1; i < visitor.Length; i++)
                if (!visitor[i])
                    endPoints.Add(new SCCRange(i, i + 1, false));

            return new SCC(endPoints);
        }
    }

    class SCCAnalysisOpation
    {
        public SCCAnalysisOpation(IReadOnlyList<AnalysisEdge> edges)
        {
            Edges = edges;
        }

        public IReadOnlyList<AnalysisEdge> Edges { get; }
    }
}
