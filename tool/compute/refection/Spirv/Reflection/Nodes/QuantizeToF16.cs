using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class QuantizeToF16 : Node
    {
        public QuantizeToF16()
        {
        }

        public QuantizeToF16(SpirvTypeBase resultType, Node value, string debugName = null)
        {
            this.ResultType = resultType;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpQuantizeToF16;

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

        public QuantizeToF16 WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpQuantizeToF16)op, treeBuilder);
        }

        public QuantizeToF16 SetUp(Action<QuantizeToF16> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpQuantizeToF16 op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Value = treeBuilder.GetNode(op.Value);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the QuantizeToF16 object.</summary>
        /// <returns>A string that represents the QuantizeToF16 object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"QuantizeToF16({ResultType}, {Value}, {DebugName})";
        }
    }
}