using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class NoLine : ExecutableNode, INodeWithNext
    {
        public NoLine()
        {
        }

        public NoLine(string debugName = null)
        {
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpNoLine;

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

        public NoLine WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpNoLine)op, treeBuilder);
        }

        public NoLine SetUp(Action<NoLine> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpNoLine op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the NoLine object.</summary>
        /// <returns>A string that represents the NoLine object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"NoLine({DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static NoLine ThenNoLine(this INodeWithNext node, string debugName = null)
        {
            return node.Then(new NoLine(debugName));
        }
    }
}