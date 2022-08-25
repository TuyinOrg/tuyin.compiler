using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcSicGetMotionVectorMaskINTEL : Node
    {
        public SubgroupAvcSicGetMotionVectorMaskINTEL()
        {
        }

        public SubgroupAvcSicGetMotionVectorMaskINTEL(SpirvTypeBase resultType, Node skipBlockPartitionType, Node direction, string debugName = null)
        {
            this.ResultType = resultType;
            this.SkipBlockPartitionType = skipBlockPartitionType;
            this.Direction = direction;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcSicGetMotionVectorMaskINTEL;

        public Node SkipBlockPartitionType { get; set; }

        public Node Direction { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SkipBlockPartitionType;
                yield return Direction;
        }

        public SubgroupAvcSicGetMotionVectorMaskINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcSicGetMotionVectorMaskINTEL)op, treeBuilder);
        }

        public SubgroupAvcSicGetMotionVectorMaskINTEL SetUp(Action<SubgroupAvcSicGetMotionVectorMaskINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcSicGetMotionVectorMaskINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SkipBlockPartitionType = treeBuilder.GetNode(op.SkipBlockPartitionType);
            Direction = treeBuilder.GetNode(op.Direction);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcSicGetMotionVectorMaskINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcSicGetMotionVectorMaskINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcSicGetMotionVectorMaskINTEL({ResultType}, {SkipBlockPartitionType}, {Direction}, {DebugName})";
        }
    }
}