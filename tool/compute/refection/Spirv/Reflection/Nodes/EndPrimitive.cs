using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class EndPrimitive : ExecutableNode, INodeWithNext
    {
        public EndPrimitive()
        {
        }

        public EndPrimitive(string debugName = null)
        {
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpEndPrimitive;

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

        public EndPrimitive WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpEndPrimitive)op, treeBuilder);
        }

        public EndPrimitive SetUp(Action<EndPrimitive> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpEndPrimitive op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the EndPrimitive object.</summary>
        /// <returns>A string that represents the EndPrimitive object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"EndPrimitive({DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static EndPrimitive ThenEndPrimitive(this INodeWithNext node, string debugName = null)
        {
            return node.Then(new EndPrimitive(debugName));
        }
    }
}