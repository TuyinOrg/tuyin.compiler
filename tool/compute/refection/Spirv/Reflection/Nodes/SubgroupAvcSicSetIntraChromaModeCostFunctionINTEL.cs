using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL : Node
    {
        public SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL()
        {
        }

        public SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL(SpirvTypeBase resultType, Node chromaModeBasePenalty, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.ChromaModeBasePenalty = chromaModeBasePenalty;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL;

        public Node ChromaModeBasePenalty { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return ChromaModeBasePenalty;
                yield return Payload;
        }

        public SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL)op, treeBuilder);
        }

        public SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL SetUp(Action<SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcSicSetIntraChromaModeCostFunctionINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            ChromaModeBasePenalty = treeBuilder.GetNode(op.ChromaModeBasePenalty);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcSicSetIntraChromaModeCostFunctionINTEL({ResultType}, {ChromaModeBasePenalty}, {Payload}, {DebugName})";
        }
    }
}