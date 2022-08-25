using libgraph;
using Tuyin.IR.Compiler.Parser.Productions;

namespace Tuyin.IR.Compiler.Parser.Generater
{
    abstract class GraphBox<TMetadata> : IProductionGraph<TMetadata> where TMetadata : struct
    {
        private List<GraphEdge<TMetadata>> mEdges;
        private List<GraphState<TMetadata>> mStates;
        private Dictionary<Token, GraphFigure<TMetadata>> mTokenFigures;
        private LevelCollection<List<GraphEdge<TMetadata>>> mCollectNewEdges;

        IEnumerable<GraphState<TMetadata>> IGraph<GraphState<TMetadata>, GraphEdge<TMetadata>>.Vertices => mStates;

        public IReadOnlyList<GraphState<TMetadata>> States => mStates;

        public IReadOnlyList<GraphEdge<TMetadata>> Edges => mEdges;

        public IList<GraphFigure<TMetadata>> Figures { get; }

        public Lexicon Lexicon { get; }

        public GraphState<TMetadata> Exit { get; }

        public GraphBox(Lexicon lexicon)                                                    
        {
            mCollectNewEdges = new LevelCollection<List<GraphEdge<TMetadata>>>();
            mTokenFigures = new Dictionary<Token, GraphFigure<TMetadata>>();
            mEdges = new List<GraphEdge<TMetadata>>();
            mStates = new List<GraphState<TMetadata>>();

            Lexicon = lexicon;
            Figures = new List<GraphFigure<TMetadata>>();

            Exit = State();
        }
        
        public GraphTable<TMetadata> Tabulate()                                                
        {
            return GraphTable<TMetadata>.FromGraph(this, Lexicon.Tokens.Count + Figures.Count);
        }

        public GraphFigure<TMetadata> Figure(string figureName)                             
        {
            var figure = new GraphFigure<TMetadata>((ushort)mStates.Count, this, figureName);
            Figures.Add(figure);
            mStates.Add(figure);
            return figure;
        }

        public GraphFigure<TMetadata> TemporaryFigure()                                     
        {
            return new GraphFigure<TMetadata>(1, this, "Temporary", new GraphState<TMetadata>(0, GraphStateFlags.None));
        }

        public GraphState<TMetadata> State()
        {
            var state = new GraphState<TMetadata>((ushort)States.Count, GraphStateFlags.None);
            mStates.Add(state);
            return state;
        }

        public GraphState<TMetadata> Empty()
        {
            var state = new GraphState<TMetadata>((ushort)States.Count, GraphStateFlags.Empty);
            mStates.Add(state);
            return state;
        }

        public GraphEdge<TMetadata> Edge(GraphState<TMetadata> left, GraphState<TMetadata> right, GraphEdgeValue value, TMetadata metadata)
        {
            var edge = new GraphEdge<TMetadata>(left, right, value, metadata);
            mEdges.Add(edge);

            if (mCollectNewEdges.Count > 0)
                mCollectNewEdges.Current.Add(edge);

            return edge;
        }

        public void Remove(GraphEdge<TMetadata> edge)
        {
            mEdges.Remove(edge);
        }

        internal void StartCollect(List<GraphEdge<TMetadata>> buffer = null)
        {
            mCollectNewEdges.Push(buffer ?? new List<GraphEdge<TMetadata>>());
        }

        internal List<GraphEdge<TMetadata>> EndCollect()
        {
            var list = mCollectNewEdges.Pop();

            if (mCollectNewEdges.Count > 0)
            {
                mCollectNewEdges.Current.AddRange(list);
            }

            return list;
        }

        internal GraphFigure<TMetadata> GetTokenFigure(GraphEdgeStep<TMetadata> last, Token token) 
        {
            if (!mTokenFigures.ContainsKey(token))
            {
                var figure = Figure(token.Description);
                new LiterallProuction(token.Expression, default).Create(figure, last, new Entry<TMetadata>(figure));
                mTokenFigures[token] = figure;
            }

            return mTokenFigures[token];
        }

        public abstract TMetadata GetTokenMetadata(Token token);

        public abstract int GetMetadataCompreValue(TMetadata metadata);

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
