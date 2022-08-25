using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL : Node
    {
        public SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL()
        {
        }

        public SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL(SpirvTypeBase resultType, Node srcImage, Node packedReferenceIds, Node packedReferenceFieldPolarities, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.SrcImage = srcImage;
            this.PackedReferenceIds = packedReferenceIds;
            this.PackedReferenceFieldPolarities = packedReferenceFieldPolarities;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL;

        public Node SrcImage { get; set; }

        public Node PackedReferenceIds { get; set; }

        public Node PackedReferenceFieldPolarities { get; set; }

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
                yield return PackedReferenceFieldPolarities;
                yield return Payload;
        }

        public SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL)op, treeBuilder);
        }

        public SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL SetUp(Action<SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SrcImage = treeBuilder.GetNode(op.SrcImage);
            PackedReferenceIds = treeBuilder.GetNode(op.PackedReferenceIds);
            PackedReferenceFieldPolarities = treeBuilder.GetNode(op.PackedReferenceFieldPolarities);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcSicEvaluateWithMultiReferenceInterlacedINTEL({ResultType}, {SrcImage}, {PackedReferenceIds}, {PackedReferenceFieldPolarities}, {Payload}, {DebugName})";
        }
    }
}