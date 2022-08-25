using libgraph;

namespace Tuyin.IR.Analysis
{
    public interface IAnalysisEdge<TVertex> : IEdge<TVertex>
        where TVertex : IVertex
    {
    }
}
