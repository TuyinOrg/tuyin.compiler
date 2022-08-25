using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeSetWeightedSadINTEL : Node
    {
        public SubgroupAvcImeSetWeightedSadINTEL()
        {
        }

        public SubgroupAvcImeSetWeightedSadINTEL(SpirvTypeBase resultType, Node packedSadWeights, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.PackedSadWeights = packedSadWeights;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeSetWeightedSadINTEL;

        public Node PackedSadWeights { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return PackedSadWeights;
                yield return Payload;
        }

        public SubgroupAvcImeSetWeightedSadINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeSetWeightedSadINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeSetWeightedSadINTEL SetUp(Action<SubgroupAvcImeSetWeightedSadINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeSetWeightedSadINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            PackedSadWeights = treeBuilder.GetNode(op.PackedSadWeights);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeSetWeightedSadINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeSetWeightedSadINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeSetWeightedSadINTEL({ResultType}, {PackedSadWeights}, {Payload}, {DebugName})";
        }
    }
}