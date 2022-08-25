using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcSicConfigureSkcINTEL : Node
    {
        public SubgroupAvcSicConfigureSkcINTEL()
        {
        }

        public SubgroupAvcSicConfigureSkcINTEL(SpirvTypeBase resultType, Node skipBlockPartitionType, Node skipMotionVectorMask, Node motionVectors, Node bidirectionalWeight, Node sadAdjustment, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.SkipBlockPartitionType = skipBlockPartitionType;
            this.SkipMotionVectorMask = skipMotionVectorMask;
            this.MotionVectors = motionVectors;
            this.BidirectionalWeight = bidirectionalWeight;
            this.SadAdjustment = sadAdjustment;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcSicConfigureSkcINTEL;

        public Node SkipBlockPartitionType { get; set; }

        public Node SkipMotionVectorMask { get; set; }

        public Node MotionVectors { get; set; }

        public Node BidirectionalWeight { get; set; }

        public Node SadAdjustment { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SkipBlockPartitionType;
                yield return SkipMotionVectorMask;
                yield return MotionVectors;
                yield return BidirectionalWeight;
                yield return SadAdjustment;
                yield return Payload;
        }

        public SubgroupAvcSicConfigureSkcINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcSicConfigureSkcINTEL)op, treeBuilder);
        }

        public SubgroupAvcSicConfigureSkcINTEL SetUp(Action<SubgroupAvcSicConfigureSkcINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcSicConfigureSkcINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SkipBlockPartitionType = treeBuilder.GetNode(op.SkipBlockPartitionType);
            SkipMotionVectorMask = treeBuilder.GetNode(op.SkipMotionVectorMask);
            MotionVectors = treeBuilder.GetNode(op.MotionVectors);
            BidirectionalWeight = treeBuilder.GetNode(op.BidirectionalWeight);
            SadAdjustment = treeBuilder.GetNode(op.SadAdjustment);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcSicConfigureSkcINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcSicConfigureSkcINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcSicConfigureSkcINTEL({ResultType}, {SkipBlockPartitionType}, {SkipMotionVectorMask}, {MotionVectors}, {BidirectionalWeight}, {SadAdjustment}, {Payload}, {DebugName})";
        }
    }
}