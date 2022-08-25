using System.Collections.Generic;

namespace libgraph
{
    public interface IRange : IEnumerable<int>
    {
        int this[int index] { get; }

        bool Contains(int number);

        int IndexOf(int number);
    }
}