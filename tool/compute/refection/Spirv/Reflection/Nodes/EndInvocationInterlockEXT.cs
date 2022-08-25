using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class EndInvocationInterlockEXT : ExecutableNode, INodeWithNext
    {
        public EndInvocationInterlockEXT()
        {
        }

        public EndInvocationInterlockEXT(string debugName = null)
        {
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpEndInvocationInterlockEXT;

        /// <summary>
        /// Next operation in sequence
        /// </summary>
        public ExecutableNode Next { get; set; }

        public override ExecutableNode GetNext()
        {
            return Next;
        }

        public T Then<T>(T node) where T: ExecutableNode
        {
            Next = node;
            return node;
        }

        public EndInvocationInterlockEXT WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpEndInvocationInterlockEXT)op, treeBuilder);
        }

        public EndInvocationInterlockEXT SetUp(Action<EndInvocationInterlockEXT> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpEndInvocationInterlockEXT op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the EndInvocationInterlockEXT object.</summary>
        /// <returns>A string that represents the EndInvocationInterlockEXT object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"EndInvocationInterlockEXT({DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static EndInvocationInterlockEXT ThenEndInvocationInterlockEXT(this INodeWithNext node, string debugName = null)
        {
            return node.Then(new EndInvocationInterlockEXT(debugName));
        }
    }
}