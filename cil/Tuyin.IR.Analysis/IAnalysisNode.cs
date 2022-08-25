using libgraph;
using System.Collections.Generic;

namespace Tuyin.IR.Analysis
{
    public interface IAnalysisNode<TEdge> : IVertex
    {
        List<TEdge> Lefts { get; }

        List<TEdge> Rights { get; }
    }
}
