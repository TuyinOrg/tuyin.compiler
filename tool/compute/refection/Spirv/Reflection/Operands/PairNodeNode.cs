using System;
using System.Collections.Generic;
using System.Text;

namespace Toe.SPIRV.Reflection.Operands
{
    internal struct PairNodeNode
    {
        public Node Node { get; set; }
        public Node Node2 { get; set; }

        public PairNodeNode(Node node, Node node2)
        {
            Node = node;
            Node2 = node2;
        }

        public override string ToString()
        {
            return $"({Node}, {Node2})";
        }
    }
}
