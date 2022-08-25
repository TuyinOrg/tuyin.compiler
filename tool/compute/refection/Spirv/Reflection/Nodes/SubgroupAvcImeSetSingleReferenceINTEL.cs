using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeSetSingleReferenceINTEL : Node
    {
        public SubgroupAvcImeSetSingleReferenceINTEL()
        {
        }

        public SubgroupAvcImeSetSingleReferenceINTEL(SpirvTypeBase resultType, Node refOffset, Node searchWindowConfig, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.RefOffset = refOffset;
            this.SearchWindowConfig = searchWindowConfig;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeSetSingleReferenceINTEL;

        public Node RefOffset { get; set; }

        public Node SearchWindowConfig { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return RefOffset;
                yield return SearchWindowConfig;
                yield return Payload;
        }

        public SubgroupAvcImeSetSingleReferenceINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeSetSingleReferenceINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeSetSingleReferenceINTEL SetUp(Action<SubgroupAvcImeSetSingleReferenceINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeSetSingleReferenceINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            RefOffset = treeBuilder.GetNode(op.RefOffset);
            SearchWindowConfig = treeBuilder.GetNode(op.SearchWindowConfig);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeSetSingleReferenceINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeSetSingleReferenceINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeSetSingleReferenceINTEL({ResultType}, {RefOffset}, {SearchWindowConfig}, {Payload}, {DebugName})";
        }
    }
}