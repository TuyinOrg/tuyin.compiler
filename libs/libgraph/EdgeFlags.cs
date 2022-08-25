using System;
using System.Collections.Generic;
using System.Text;

namespace libgraph
{
    [Flags]
    public enum EdgeFlags : ushort
    {
        None        = 0,
        Optional    = 1
    }
}
