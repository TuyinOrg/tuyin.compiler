using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL : Node
    {
        public SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL()
        {
        }

        public SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL(SpirvTypeBase resultType, Node packedReferenceIds, Node packedReferenceParameterFieldPolarities, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.PackedReferenceIds = packedReferenceIds;
            this.PackedReferenceParameterFieldPolarities = packedReferenceParameterFieldPolarities;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL;

        public Node PackedReferenceIds { get; set; }

        public Node PackedReferenceParameterFieldPolarities { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return PackedReferenceIds;
                yield return PackedReferenceParameterFieldPolarities;
                yield return Payload;
        }

        public SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL SetUp(Action<SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            PackedReferenceIds = treeBuilder.GetNode(op.PackedReferenceIds);
            PackedReferenceParameterFieldPolarities = treeBuilder.GetNode(op.PackedReferenceParameterFieldPolarities);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceGetInterReferenceInterlacedFieldPolaritiesINTEL({ResultType}, {PackedReferenceIds}, {PackedReferenceParameterFieldPolarities}, {Payload}, {DebugName})";
        }
    }
}