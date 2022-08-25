using libgraph;
using System;

namespace libmath
{

    internal class GraphThree<TVertex, TEdge> : Algorithm<IEnumerable<int>, IGraph<TVertex, TEdge>>
        where TEdge : IEdge<TVertex>
        where TVertex : IVertex
    {
        public override IEnumerable<int> Compute(IGraph<TVertex, TEdge> arg)
        {
            throw new NotImplementedException();
        }
    }
}
