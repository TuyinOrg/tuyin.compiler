using Toe.SPIRV.Reflection.Types;

namespace Toe.SPIRV.Reflection
{
    internal struct NodePin
    {
        public NodePin(Node node, string name, SpirvTypeBase type)
        {
            Node = node;
            Name = name;
            Type = type;
        }
        public Node Node { get; }
        public string Name { get; }
        public SpirvTypeBase Type { get; }
    }
}