using System;
using System.Collections.Generic;

namespace Tuyin.IR.Analysis
{
    public abstract class AnalysisGraphBase<TVertex> : IAnalysisGraph<TVertex>
        where TVertex : IAnalysisNode<AnalysisEdge>
    {
        public abstract IReadOnlyList<TVertex> Vertices { get; }

        public abstract IReadOnlyList<AnalysisEdge> Edges { get; }

        public virtual void SaveToFile(string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
