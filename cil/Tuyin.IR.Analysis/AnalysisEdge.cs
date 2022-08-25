using libgraph;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tuyin.IR.Analysis
{
    public struct AnalysisEdge : IAnalysisEdge<AnalysisNode>
    {
        internal AnalysisEdge(AnalysisNode subset, AnalysisNode source, AnalysisNode target)
            : this(default, subset, source, target)
        {
        }

        internal AnalysisEdge(EdgeFlags flags, AnalysisNode subset, AnalysisNode source, AnalysisNode target)
            : this(flags, subset, source, target, default)
        {
        }

        internal AnalysisEdge(EdgeFlags flags, AnalysisNode subset, AnalysisNode source, AnalysisNode target, SourceSpan sourceSpan)
        {
            Flags = flags;
            Subset = subset;
            Source = source;
            Target = target;
            SourceSpan = sourceSpan;
        }

        public EdgeFlags Flags { get; internal set; }

        public AnalysisNode Subset { get; }

        public AnalysisNode Source { get; }

        public AnalysisNode Target { get; }

        public SourceSpan SourceSpan { get; }

        public IEnumerable<char> GetLinkChars(int max)
        {
            throw new NotImplementedException();
        }
    }

    static class AnalysisEdgeHelper 
    {
        public static IEnumerable<AnalysisNode> GetEntries(this IEnumerable<AnalysisEdge> edges) 
        {
            return edges.Where(x => x.Source.Lefts.Count == 0).Select(x => x.Source).Distinct();
        }

        public static IEnumerable<AnalysisNode> GetExits(this IEnumerable<AnalysisEdge> edges)
        {
            return edges.Where(x => x.Source.Rights.Count == 0).Select(x => x.Target).Distinct();
        }
    }
}
