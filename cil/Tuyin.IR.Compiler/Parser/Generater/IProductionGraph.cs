using libgraph;

namespace Tuyin.IR.Compiler.Parser.Generater
{
    interface IProductionGraph<TMetadata> : IGraph<GraphState<TMetadata>, GraphEdge<TMetadata>>
    {
        Lexicon Lexicon { get; }

        int GetMetadataCompreValue(TMetadata metadata);

        IEnumerable<GraphEdge<TMetadata>> GetLefts(GraphState<TMetadata> state);

        IEnumerable<GraphEdge<TMetadata>> GetRights(GraphState<TMetadata> state);
    }
}
