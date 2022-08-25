using libfsm;
using libgraph;

namespace Tuyin.IR.Compiler.Parser.Generater
{
    class GraphTable<TMetadata> : FATable<TMetadata> where TMetadata : struct
    {
        private int mMax;
        private FATableFlags mFlags;
        private IProductionGraph<TMetadata> mGraph;

        private GraphTable(IProductionGraph<TMetadata> graph, int max, FATableFlags flags)
        {
            mMax = max;
            mFlags = flags;
            mGraph = graph;
        }

        protected override bool MetadataConflictResolution(IShiftRightMemoryModel model, MetadataGroup<TMetadata>[] groups, out TMetadata result)
        {
            // 找到expression最大循环请求
            var compreVals = groups.SelectMany(y => 
                y.Transitions.Select(x => new 
                { 
                    Metadata = x.Metadata, 
                    CompreValue = mGraph.GetMetadataCompreValue(x.Metadata)
                })).ToArray();

            // 查找compreValue最小的Metadata
            var minMetadata = compreVals.OrderBy(x => x.CompreValue).First(); // .MinItem(x => x.CompreValue);

            result = minMetadata.Metadata;
            return true;
        }

        public DebugGraph CreateDebugGraph(string debugName)
        {
            return DebugHelper.FromTransitions(debugName, mGraph, Transitions, Actions);
        }

        public void Build()
        {
            Generate(
                  mGraph,
                  mMax,
                  edge => edge.Metadata,
                  mFlags);
        }

        public bool[] GetBinarySkips(Lexicon lexicon)
        {
            bool[] skips = new bool[lexicon.Tokens.Count];
            for (var i = 0; i < lexicon.Tokens.Count; i++)
            {
                var token = lexicon.Tokens[i];
                skips[token.Index] = token.IsSkip;
            }

            return skips;
        }

        public static GraphTable<TMetadata> FromGraph(IProductionGraph<TMetadata> graph, int tokenMaxValue) 
        {
            var table = new GraphTable<TMetadata>(
                graph,
                tokenMaxValue,
                FATableFlags.MemoryLayout |
                FATableFlags.ConflictResolution |
                FATableFlags.FitFragmentMerge |
                FATableFlags.Optimize);

            return table;
        }
    }
}
