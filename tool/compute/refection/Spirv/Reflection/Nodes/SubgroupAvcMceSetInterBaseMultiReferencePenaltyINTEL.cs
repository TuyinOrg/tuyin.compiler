using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL : Node
    {
        public SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL()
        {
        }

        public SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL(SpirvTypeBase resultType, Node referenceBasePenalty, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.ReferenceBasePenalty = referenceBasePenalty;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL;

        public Node ReferenceBasePenalty { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return ReferenceBasePenalty;
                yield return Payload;
        }

        public SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL SetUp(Action<SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            ReferenceBasePenalty = treeBuilder.GetNode(op.ReferenceBasePenalty);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceSetInterBaseMultiReferencePenaltyINTEL({ResultType}, {ReferenceBasePenalty}, {Payload}, {DebugName})";
        }
    }
}