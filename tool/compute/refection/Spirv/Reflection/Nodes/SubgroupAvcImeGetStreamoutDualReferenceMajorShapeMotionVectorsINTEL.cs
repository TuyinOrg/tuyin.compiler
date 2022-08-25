using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL : Node
    {
        public SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL()
        {
        }

        public SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL(SpirvTypeBase resultType, Node payload, Node majorShape, Node direction, string debugName = null)
        {
            this.ResultType = resultType;
            this.Payload = payload;
            this.MajorShape = majorShape;
            this.Direction = direction;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL;

        public Node Payload { get; set; }

        public Node MajorShape { get; set; }

        public Node Direction { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Payload;
                yield return MajorShape;
                yield return Direction;
        }

        public SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL SetUp(Action<SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Payload = treeBuilder.GetNode(op.Payload);
            MajorShape = treeBuilder.GetNode(op.MajorShape);
            Direction = treeBuilder.GetNode(op.Direction);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeGetStreamoutDualReferenceMajorShapeMotionVectorsINTEL({ResultType}, {Payload}, {MajorShape}, {Direction}, {DebugName})";
        }
    }
}