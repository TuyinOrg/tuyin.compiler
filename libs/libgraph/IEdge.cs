using System.Collections.Generic;

namespace libgraph
{
    public interface IEdge<out TVertex> where TVertex : IVertex
    {
        bool Optional => Flags.HasFlag(EdgeFlags.Optional);

        EdgeFlags Flags { get; }

        bool ConnectSubset => Subset != null;

        TVertex Subset { get; }

        TVertex Source { get; }

        TVertex Target { get; }

        IEnumerable<char> GetLinkChars(int max);
    }
}
