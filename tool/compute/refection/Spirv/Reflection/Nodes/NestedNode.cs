using System;
using System.Collections.Generic;
using System.Text;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal class NestedNode
    {
        private readonly Node _node;

        public NestedNode(Node node)
        {
            _node = node;
        }

        public Node Node => _node;

        public override string ToString()
        {
            return _node.ToString();
        }
    }
}
