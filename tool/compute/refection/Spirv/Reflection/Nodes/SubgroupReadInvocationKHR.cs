using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupReadInvocationKHR : Node
    {
        public SubgroupReadInvocationKHR()
        {
        }

        public SubgroupReadInvocationKHR(SpirvTypeBase resultType, Node value, Node index, string debugName = null)
        {
            this.ResultType = resultType;
            this.Value = value;
            this.Index = index;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupReadInvocationKHR;

        public Node Value { get; set; }

        public Node Index { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Value;
                yield return Index;
        }

        public SubgroupReadInvocationKHR WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupReadInvocationKHR)op, treeBuilder);
        }

        public SubgroupReadInvocationKHR SetUp(Action<SubgroupReadInvocationKHR> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupReadInvocationKHR op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Value = treeBuilder.GetNode(op.Value);
            Index = treeBuilder.GetNode(op.Index);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupReadInvocationKHR object.</summary>
        /// <returns>A string that represents the SubgroupReadInvocationKHR object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupReadInvocationKHR({ResultType}, {Value}, {Index}, {DebugName})";
        }
    }
}