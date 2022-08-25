using libgraph;

namespace Tuyin.IR.Compiler.Parser.Generater
{
    class GraphEdge<TMetadata> : IEdge<GraphState<TMetadata>>
    {
        public EdgeFlags Flags { get; internal set; }

        public TMetadata Metadata { get; internal set; }

        public GraphState<TMetadata> Source { get; internal set; }

        public GraphState<TMetadata> Target { get; internal set; }

        public GraphEdgeValue Value { get; }

        public GraphState<TMetadata> Subset { get; internal set; }

        public string Descrption { get; internal set; }

        internal GraphEdge(GraphState<TMetadata> source, GraphState<TMetadata> target, GraphEdgeValue value, TMetadata metadata) 
        {
            Source = source;
            Target = target;
            Value = value;
            Metadata = metadata;
        }

        public bool Contains(char val) 
        {
            return Value.Contains(val);
        }

        public IEnumerable<char> GetLinkChars(int max)
        {
            return Value.GetChars(max);
        }
    }
}
