using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL : Node
    {
        public SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL()
        {
        }

        public SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL(SpirvTypeBase resultType, Node payload, Node majorShape, string debugName = null)
        {
            this.ResultType = resultType;
            this.Payload = payload;
            this.MajorShape = majorShape;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL;

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

        public SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL SetUp(Action<SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Payload = treeBuilder.GetNode(op.Payload);
            MajorShape = treeBuilder.GetNode(op.MajorShape);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeGetStreamoutSingleReferenceMajorShapeReferenceIdsINTEL({ResultType}, {Payload}, {MajorShape}, {DebugName})";
        }
    }
}