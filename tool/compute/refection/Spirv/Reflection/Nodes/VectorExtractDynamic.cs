using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class VectorExtractDynamic : Node
    {
        public VectorExtractDynamic()
        {
        }

        public VectorExtractDynamic(SpirvTypeBase resultType, Node vector, Node index, string debugName = null)
        {
            this.ResultType = resultType;
            this.Vector = vector;
            this.Index = index;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpVectorExtractDynamic;

        public Node Vector { get; set; }

        public Node Index { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Vector;
                yield return Index;
        }

        public VectorExtractDynamic WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpVectorExtractDynamic)op, treeBuilder);
        }

        public VectorExtractDynamic SetUp(Action<VectorExtractDynamic> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpVectorExtractDynamic op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Vector = treeBuilder.GetNode(op.Vector);
            Index = treeBuilder.GetNode(op.Index);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the VectorExtractDynamic object.</summary>
        /// <returns>A string that represents the VectorExtractDynamic object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"VectorExtractDynamic({ResultType}, {Vector}, {Index}, {DebugName})";
        }
    }
}