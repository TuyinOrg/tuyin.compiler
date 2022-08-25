using libgraph;

namespace Tuyin.IR.Compiler.Parser.Generater
{
    class GraphFigure<TMetadata> : GraphState<TMetadata>, IProductionGraph<TMetadata> where TMetadata : struct
    {
        private List<GraphEdge<TMetadata>> mEdges;
        private List<GraphState<TMetadata>> mStates;

        public GraphBox<TMetadata> GraphBox { get; }

        public string DisplayName { get; }

        public IReadOnlyList<GraphEdge<TMetadata>> Edges => mEdges;

        public IReadOnlyList<GraphState<TMetadata>> States => mStates;

        public GraphState<TMetadata> Main => this;

        public GraphState<TMetadata> Exit { get; }

        public Lexicon Lexicon => GraphBox.Lexicon;

        IEnumerable<GraphState<TMetadata>> IGraph<GraphState<TMetadata>, GraphEdge<TMetadata>>.Vertices => States;

        internal GraphFigure(ushort index, GraphBox<TMetadata> box, string figureName)                                                              
            : base(index, GraphStateFlags.Figure)
        {
            GraphBox = box;
            DisplayName = figureName;

            mEdges = new List<GraphEdge<TMetadata>>();
            mStates = new List<GraphState<TMetadata>>();

            Exit = GraphBox.Exit;
        }

        internal GraphFigure(ushort index, GraphBox<TMetadata> box, string figureName, GraphState<TMetadata> exit)
            : this(index, box, figureName)
        {
            Exit = exit;
        }

        public GraphState<TMetadata> State()                                                                                                        
        {
            var state = GraphBox.State();
            mStates.Add(state);
            return state;
        }

        public GraphState<TMetadata> Empty()                                                                                                        
        {
            var state = GraphBox.Empty();
            mStates.Add(state);
            return state;
        }

        public GraphEdge<TMetadata> Edge(GraphState<TMetadata> left, GraphState<TMetadata> right, GraphEdgeValue value, TMetadata metadata)         
        {
            var edge = GraphBox.Edge(left, right, value, metadata);
            edge.Descrption = value.ToString();
            mEdges.Add(edge);
            return edge;
        }

        public void Remove(GraphEdge<TMetadata> edge)                                                                                               
        {
            GraphBox.Remove(edge);
            mEdges.Remove(edge);
        }

        public int GetMetadataCompreValue(TMetadata metadata)                                                                                       
        {
            return GraphBox.GetMetadataCompreValue(metadata);
        }

        public IEnumerable<GraphEdge<TMetadata>> GetLefts(GraphState<TMetadata> state)
        {
            return mEdges.AsParallel().Where(x => x.Target == state);
        }

        public IEnumerable<GraphEdge<TMetadata>> GetRights(GraphState<TMetadata> state)
        {
            return mEdges.AsParallel().Where(x => x.Source == state);
        }
    }
}
