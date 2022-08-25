using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupShuffleINTEL : Node
    {
        public SubgroupShuffleINTEL()
        {
        }

        public SubgroupShuffleINTEL(SpirvTypeBase resultType, Node data, Node invocationId, string debugName = null)
        {
            this.ResultType = resultType;
            this.Data = data;
            this.InvocationId = invocationId;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupShuffleINTEL;

        public Node Data { get; set; }

        public Node InvocationId { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Data;
                yield return InvocationId;
        }

        public SubgroupShuffleINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupShuffleINTEL)op, treeBuilder);
        }

        public SubgroupShuffleINTEL SetUp(Action<SubgroupShuffleINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupShuffleINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Data = treeBuilder.GetNode(op.Data);
            InvocationId = treeBuilder.GetNode(op.InvocationId);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupShuffleINTEL object.</summary>
        /// <returns>A string that represents the SubgroupShuffleINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupShuffleINTEL({ResultType}, {Data}, {InvocationId}, {DebugName})";
        }
    }
}