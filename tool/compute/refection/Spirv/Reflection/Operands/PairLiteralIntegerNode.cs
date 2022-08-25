using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Operands
{
    internal struct PairLiteralIntegerNode
    {
        public uint LiteralInteger { get; }
        public Node Node { get; }

        public PairLiteralIntegerNode(uint integer, Node node)
        {
            LiteralInteger = integer;
            Node = node;
        }

        public override string ToString()
        {
            return $"{LiteralInteger}: {Node}";
        }
    }
}