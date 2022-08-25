using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL : Node
    {
        public SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL()
        {
        }

        public SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL(SpirvTypeBase resultType, Node srcImage, Node packedReferenceIds, Node packedReferenceFieldPolarities, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.SrcImage = srcImage;
            this.PackedReferenceIds = packedReferenceIds;
            this.PackedReferenceFieldPolarities = packedReferenceFieldPolarities;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL;

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

        public SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL)op, treeBuilder);
        }

        public SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL SetUp(Action<SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SrcImage = treeBuilder.GetNode(op.SrcImage);
            PackedReferenceIds = treeBuilder.GetNode(op.PackedReferenceIds);
            PackedReferenceFieldPolarities = treeBuilder.GetNode(op.PackedReferenceFieldPolarities);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcRefEvaluateWithMultiReferenceInterlacedINTEL({ResultType}, {SrcImage}, {PackedReferenceIds}, {PackedReferenceFieldPolarities}, {Payload}, {DebugName})";
        }
    }
}