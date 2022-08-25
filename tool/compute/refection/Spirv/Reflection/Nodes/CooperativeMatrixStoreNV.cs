using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CooperativeMatrixStoreNV : ExecutableNode, INodeWithNext
    {
        public CooperativeMatrixStoreNV()
        {
        }

        public CooperativeMatrixStoreNV(Node pointer, Node @object, Node stride, Node columnMajor, Spv.MemoryAccess memoryAccess, string debugName = null)
        {
            this.Pointer = pointer;
            this.Object = @object;
            this.Stride = stride;
            this.ColumnMajor = columnMajor;
            this.MemoryAccess = memoryAccess;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCooperativeMatrixStoreNV;

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

        public Node Pointer { get; set; }

        public Node Object { get; set; }

        public Node Stride { get; set; }

        public Node ColumnMajor { get; set; }

        public Spv.MemoryAccess MemoryAccess { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pointer;
                yield return Object;
                yield return Stride;
                yield return ColumnMajor;
        }

        public CooperativeMatrixStoreNV WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCooperativeMatrixStoreNV)op, treeBuilder);
        }

        public CooperativeMatrixStoreNV SetUp(Action<CooperativeMatrixStoreNV> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCooperativeMatrixStoreNV op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Pointer = treeBuilder.GetNode(op.Pointer);
            Object = treeBuilder.GetNode(op.Object);
            Stride = treeBuilder.GetNode(op.Stride);
            ColumnMajor = treeBuilder.GetNode(op.ColumnMajor);
            MemoryAccess = op.MemoryAccess;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CooperativeMatrixStoreNV object.</summary>
        /// <returns>A string that represents the CooperativeMatrixStoreNV object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CooperativeMatrixStoreNV({Pointer}, {Object}, {Stride}, {ColumnMajor}, {MemoryAccess}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static CooperativeMatrixStoreNV ThenCooperativeMatrixStoreNV(this INodeWithNext node, Node pointer, Node @object, Node stride, Node columnMajor, Spv.MemoryAccess memoryAccess, string debugName = null)
        {
            return node.Then(new CooperativeMatrixStoreNV(pointer, @object, stride, columnMajor, memoryAccess, debugName));
        }
    }
}