using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class WritePackedPrimitiveIndices4x8NV : ExecutableNode, INodeWithNext
    {
        public WritePackedPrimitiveIndices4x8NV()
        {
        }

        public WritePackedPrimitiveIndices4x8NV(Node indexOffset, Node packedIndices, string debugName = null)
        {
            this.IndexOffset = indexOffset;
            this.PackedIndices = packedIndices;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpWritePackedPrimitiveIndices4x8NV;

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

        public Node IndexOffset { get; set; }

        public Node PackedIndices { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return IndexOffset;
                yield return PackedIndices;
        }

        public WritePackedPrimitiveIndices4x8NV WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpWritePackedPrimitiveIndices4x8NV)op, treeBuilder);
        }

        public WritePackedPrimitiveIndices4x8NV SetUp(Action<WritePackedPrimitiveIndices4x8NV> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpWritePackedPrimitiveIndices4x8NV op, SpirvInstructionTreeBuilder treeBuilder)
        {
            IndexOffset = treeBuilder.GetNode(op.IndexOffset);
            PackedIndices = treeBuilder.GetNode(op.PackedIndices);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the WritePackedPrimitiveIndices4x8NV object.</summary>
        /// <returns>A string that represents the WritePackedPrimitiveIndices4x8NV object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"WritePackedPrimitiveIndices4x8NV({IndexOffset}, {PackedIndices}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static WritePackedPrimitiveIndices4x8NV ThenWritePackedPrimitiveIndices4x8NV(this INodeWithNext node, Node indexOffset, Node packedIndices, string debugName = null)
        {
            return node.Then(new WritePackedPrimitiveIndices4x8NV(indexOffset, packedIndices, debugName));
        }
    }
}