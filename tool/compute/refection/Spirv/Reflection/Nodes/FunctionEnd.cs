using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class FunctionEnd : ExecutableNode, INodeWithNext
    {
        public FunctionEnd()
        {
        }

        public FunctionEnd(string debugName = null)
        {
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpFunctionEnd;

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

        public FunctionEnd WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpFunctionEnd)op, treeBuilder);
        }

        public FunctionEnd SetUp(Action<FunctionEnd> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpFunctionEnd op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the FunctionEnd object.</summary>
        /// <returns>A string that represents the FunctionEnd object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"FunctionEnd({DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static FunctionEnd ThenFunctionEnd(this INodeWithNext node, string debugName = null)
        {
            return node.Then(new FunctionEnd(debugName));
        }
    }
}