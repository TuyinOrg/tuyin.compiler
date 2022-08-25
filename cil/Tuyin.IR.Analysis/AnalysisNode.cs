using System.Collections.Generic;

namespace Tuyin.IR.Analysis
{
    public abstract class AnalysisNode : IAnalysisNode<AnalysisEdge>
    {
        public AnalysisNode(ushort index)
        {
            Index = index;
            Rights = new List<AnalysisEdge>();
            Lefts = new List<AnalysisEdge>();
        }

        public List<AnalysisEdge> Lefts { get; }

        public List<AnalysisEdge> Rights { get; }

        public ushort Index { get; }
    }
}
