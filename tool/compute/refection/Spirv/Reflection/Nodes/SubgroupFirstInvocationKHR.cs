using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupFirstInvocationKHR : Node
    {
        public SubgroupFirstInvocationKHR()
        {
        }

        public SubgroupFirstInvocationKHR(SpirvTypeBase resultType, Node value, string debugName = null)
        {
            this.ResultType = resultType;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupFirstInvocationKHR;

        public Node Value { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Value;
        }

        public SubgroupFirstInvocationKHR WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupFirstInvocationKHR)op, treeBuilder);
        }

        public SubgroupFirstInvocationKHR SetUp(Action<SubgroupFirstInvocationKHR> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupFirstInvocationKHR op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Value = treeBuilder.GetNode(op.Value);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupFirstInvocationKHR object.</summary>
        /// <returns>A string that represents the SubgroupFirstInvocationKHR object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupFirstInvocationKHR({ResultType}, {Value}, {DebugName})";
        }
    }
}