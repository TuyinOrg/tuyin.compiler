using System;
using System.Collections.Generic;

namespace libgraph
{
    public struct DebugEdge : IEdge<DebugVertex>
    {
        public string Descrption { get; }

        public DebugVertex Source { get; }

        public DebugVertex Target { get; }

        public EdgeFlags Flags { get; }

        public string Tips { get; }

        public DebugVertex Subset => throw new NotImplementedException();

        public DebugEdge(EdgeFlags flags, string descrption, DebugVertex source, DebugVertex target) 
            : this(flags, descrption, source, target, null)
        {

        }

        public DebugEdge(EdgeFlags flags, string descrption, DebugVertex source, DebugVertex target, string tips)
        {
            Flags = flags;
            Descrption = descrption;
            Source = source; 
            Target = target;
            Tips = tips;
        }

        public IEnumerable<char> GetLinkChars(int max)
        {
            throw new NotImplementedException();
        }
    }
}
