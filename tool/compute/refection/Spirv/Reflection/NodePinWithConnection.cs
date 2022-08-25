using Toe.SPIRV.Reflection.Types;

namespace Toe.SPIRV.Reflection
{
    internal struct NodePinWithConnection
    {
        public NodePinWithConnection(Node node, string name, SpirvTypeBase type, NodePin? connectedPin = null)
        {
            Node = node;
            Name = name;
            Type = type;
            ConnectedPin = connectedPin;
        }
        public Node Node { get; }
        public string Name { get; }
        public SpirvTypeBase Type { get; }
        public NodePin? ConnectedPin { get; }
    }
}