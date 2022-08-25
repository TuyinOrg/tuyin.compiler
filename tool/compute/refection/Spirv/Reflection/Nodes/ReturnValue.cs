using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ReturnValue : ExecutableNode
    {
        public ReturnValue()
        {
        }

        public ReturnValue(Node value, string debugName = null)
        {
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpReturnValue;

        public Node Value { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Value;
        }

        public ReturnValue WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpReturnValue)op, treeBuilder);
        }

        public ReturnValue SetUp(Action<ReturnValue> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpReturnValue op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Value = treeBuilder.GetNode(op.Value);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ReturnValue object.</summary>
        /// <returns>A string that represents the ReturnValue object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ReturnValue({Value}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static ReturnValue ThenReturnValue(this INodeWithNext node, Node value, string debugName = null)
        {
            return node.Then(new ReturnValue(value, debugName));
        }
    }
}