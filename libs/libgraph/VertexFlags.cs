using System;

namespace libgraph
{
    [Flags]
    public enum VertexFlags : ushort
    {
        None            = 0,
        EndPoint        = 1
    }
}
