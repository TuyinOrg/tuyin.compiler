using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL : Node
    {
        public SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL()
        {
        }

        public SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL(SpirvTypeBase resultType, Node payload, Node majorShape, string debugName = null)
        {
            this.ResultType = resultType;
            this.Payload = payload;
            this.MajorShape = majorShape;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL;

        public Node Payload { get; set; }

        public Node MajorShape { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Payload;
                yield return MajorShape;
        }

        public SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL SetUp(Action<SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Payload = treeBuilder.GetNode(op.Payload);
            MajorShape = treeBuilder.GetNode(op.MajorShape);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeMotionVectorsINTEL({ResultType}, {Payload}, {MajorShape}, {DebugName})";
        }
    }
}