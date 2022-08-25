using libgraph;

namespace Tuyin.IR.Compiler.Parser.Generater
{
    class GraphState<TMetadata> : IVertex
    {
        public GraphStateFlags Flags { get; private set; }

        public ushort Index { get; }

        public virtual string Descrption => Index.ToString();

        internal GraphState(ushort index, GraphStateFlags flags) 
        {
            Index = index;
            Flags = flags;
        }
    }
}
