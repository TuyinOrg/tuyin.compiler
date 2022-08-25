using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcSicEvaluateWithMultiReferenceINTEL : Node
    {
        public SubgroupAvcSicEvaluateWithMultiReferenceINTEL()
        {
        }

        public SubgroupAvcSicEvaluateWithMultiReferenceINTEL(SpirvTypeBase resultType, Node srcImage, Node packedReferenceIds, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.SrcImage = srcImage;
            this.PackedReferenceIds = packedReferenceIds;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcSicEvaluateWithMultiReferenceINTEL;

        public Node SrcImage { get; set; }

        public Node PackedReferenceIds { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SrcImage;
                yield return PackedReferenceIds;
                yield return Payload;
        }

        public SubgroupAvcSicEvaluateWithMultiReferenceINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcSicEvaluateWithMultiReferenceINTEL)op, treeBuilder);
        }

        public SubgroupAvcSicEvaluateWithMultiReferenceINTEL SetUp(Action<SubgroupAvcSicEvaluateWithMultiReferenceINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcSicEvaluateWithMultiReferenceINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SrcImage = treeBuilder.GetNode(op.SrcImage);
            PackedReferenceIds = treeBuilder.GetNode(op.PackedReferenceIds);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcSicEvaluateWithMultiReferenceINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcSicEvaluateWithMultiReferenceINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcSicEvaluateWithMultiReferenceINTEL({ResultType}, {SrcImage}, {PackedReferenceIds}, {Payload}, {DebugName})";
        }
    }
}