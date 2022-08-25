using libgraph;
using System.Collections.Generic;
using System.Linq;

namespace Tuyin.IR.Analysis
{
    public interface IAnalysisGraph<TVertex> : IGraph<AnalysisNode, AnalysisEdge>
        where TVertex : IAnalysisNode<AnalysisEdge>
    {
        IEnumerable<TVertex> Entries => Vertices.Cast<TVertex>().Where(x => x.Lefts.Count == 0);

        IEnumerable<TVertex> Exits => Vertices.Cast<TVertex>().Where(x => x.Rights.Count == 0);

        void SaveToFile(string fileName);
    }
}
