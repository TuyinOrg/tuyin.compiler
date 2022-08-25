using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL : Node
    {
        public SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL()
        {
        }

        public SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL(SpirvTypeBase resultType, Node lumaModePenalty, Node lumaPackedNeighborModes, Node lumaPackedNonDcPenalty, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.LumaModePenalty = lumaModePenalty;
            this.LumaPackedNeighborModes = lumaPackedNeighborModes;
            this.LumaPackedNonDcPenalty = lumaPackedNonDcPenalty;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL;

        public Node LumaModePenalty { get; set; }

        public Node LumaPackedNeighborModes { get; set; }

        public Node LumaPackedNonDcPenalty { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return LumaModePenalty;
                yield return LumaPackedNeighborModes;
                yield return LumaPackedNonDcPenalty;
                yield return Payload;
        }

        public SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL)op, treeBuilder);
        }

        public SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL SetUp(Action<SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcSicSetIntraLumaModeCostFunctionINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            LumaModePenalty = treeBuilder.GetNode(op.LumaModePenalty);
            LumaPackedNeighborModes = treeBuilder.GetNode(op.LumaPackedNeighborModes);
            LumaPackedNonDcPenalty = treeBuilder.GetNode(op.LumaPackedNonDcPenalty);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcSicSetIntraLumaModeCostFunctionINTEL({ResultType}, {LumaModePenalty}, {LumaPackedNeighborModes}, {LumaPackedNonDcPenalty}, {Payload}, {DebugName})";
        }
    }
}