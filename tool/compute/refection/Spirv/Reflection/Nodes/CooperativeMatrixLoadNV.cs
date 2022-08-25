using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CooperativeMatrixLoadNV : Node
    {
        public CooperativeMatrixLoadNV()
        {
        }

        public CooperativeMatrixLoadNV(SpirvTypeBase resultType, Node pointer, Node stride, Node columnMajor, Spv.MemoryAccess memoryAccess, string debugName = null)
        {
            this.ResultType = resultType;
            this.Pointer = pointer;
            this.Stride = stride;
            this.ColumnMajor = columnMajor;
            this.MemoryAccess = memoryAccess;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCooperativeMatrixLoadNV;

        public Node Pointer { get; set; }

        public Node Stride { get; set; }

        public Node ColumnMajor { get; set; }

        public Spv.MemoryAccess MemoryAccess { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pointer;
                yield return Stride;
                yield return ColumnMajor;
        }

        public CooperativeMatrixLoadNV WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCooperativeMatrixLoadNV)op, treeBuilder);
        }

        public CooperativeMatrixLoadNV SetUp(Action<CooperativeMatrixLoadNV> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCooperativeMatrixLoadNV op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Pointer = treeBuilder.GetNode(op.Pointer);
            Stride = treeBuilder.GetNode(op.Stride);
            ColumnMajor = treeBuilder.GetNode(op.ColumnMajor);
            MemoryAccess = op.MemoryAccess;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CooperativeMatrixLoadNV object.</summary>
        /// <returns>A string that represents the CooperativeMatrixLoadNV object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CooperativeMatrixLoadNV({ResultType}, {Pointer}, {Stride}, {ColumnMajor}, {MemoryAccess}, {DebugName})";
        }
    }
}