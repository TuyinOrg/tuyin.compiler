using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class BeginInvocationInterlockEXT : ExecutableNode, INodeWithNext
    {
        public BeginInvocationInterlockEXT()
        {
        }

        public BeginInvocationInterlockEXT(string debugName = null)
        {
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpBeginInvocationInterlockEXT;

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

        public BeginInvocationInterlockEXT WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpBeginInvocationInterlockEXT)op, treeBuilder);
        }

        public BeginInvocationInterlockEXT SetUp(Action<BeginInvocationInterlockEXT> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpBeginInvocationInterlockEXT op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the BeginInvocationInterlockEXT object.</summary>
        /// <returns>A string that represents the BeginInvocationInterlockEXT object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"BeginInvocationInterlockEXT({DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static BeginInvocationInterlockEXT ThenBeginInvocationInterlockEXT(this INodeWithNext node, string debugName = null)
        {
            return node.Then(new BeginInvocationInterlockEXT(debugName));
        }
    }
}