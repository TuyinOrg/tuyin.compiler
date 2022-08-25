using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceSetInterShapePenaltyINTEL : Node
    {
        public SubgroupAvcMceSetInterShapePenaltyINTEL()
        {
        }

        public SubgroupAvcMceSetInterShapePenaltyINTEL(SpirvTypeBase resultType, Node packedShapePenalty, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.PackedShapePenalty = packedShapePenalty;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceSetInterShapePenaltyINTEL;

        public Node PackedShapePenalty { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return PackedShapePenalty;
                yield return Payload;
        }

        public SubgroupAvcMceSetInterShapePenaltyINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceSetInterShapePenaltyINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceSetInterShapePenaltyINTEL SetUp(Action<SubgroupAvcMceSetInterShapePenaltyINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceSetInterShapePenaltyINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            PackedShapePenalty = treeBuilder.GetNode(op.PackedShapePenalty);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceSetInterShapePenaltyINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceSetInterShapePenaltyINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceSetInterShapePenaltyINTEL({ResultType}, {PackedShapePenalty}, {Payload}, {DebugName})";
        }
    }
}