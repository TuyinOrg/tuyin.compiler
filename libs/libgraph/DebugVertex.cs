namespace libgraph
{
    public class DebugVertex : IVertex
    {
        public ushort Index { get; }

        public string Id { get; }

        public string Tips { get; }

        public VertexFlags Flags { get; }

        public DebugVertex(ushort index, string id, VertexFlags flags)
            : this(index, id, flags, null)
        {
        }

        public DebugVertex(ushort index, string id, VertexFlags flags, string tips) 
        {
            Index = index;
            Flags = flags;
            Id = id;
            Tips = tips;
        }
    }
}
