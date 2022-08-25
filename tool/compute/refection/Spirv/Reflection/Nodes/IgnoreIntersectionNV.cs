using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class IgnoreIntersectionNV : ExecutableNode, INodeWithNext
    {
        public IgnoreIntersectionNV()
        {
        }

        public IgnoreIntersectionNV(string debugName = null)
        {
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpIgnoreIntersectionNV;

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

        public IgnoreIntersectionNV WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpIgnoreIntersectionNV)op, treeBuilder);
        }

        public IgnoreIntersectionNV SetUp(Action<IgnoreIntersectionNV> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpIgnoreIntersectionNV op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the IgnoreIntersectionNV object.</summary>
        /// <returns>A string that represents the IgnoreIntersectionNV object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"IgnoreIntersectionNV({DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static IgnoreIntersectionNV ThenIgnoreIntersectionNV(this INodeWithNext node, string debugName = null)
        {
            return node.Then(new IgnoreIntersectionNV(debugName));
        }
    }
}