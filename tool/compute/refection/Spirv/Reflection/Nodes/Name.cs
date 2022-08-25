using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Name : Node
    {
        public Name()
        {
        }

        public Name(Node target, string value, string debugName = null)
        {
            this.Target = target;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpName;

        public Node Target { get; set; }

        public string Value { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Target;
        }

        public Name WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpName)op, treeBuilder);
        }

        public Name SetUp(Action<Name> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpName op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Target = treeBuilder.GetNode(op.Target);
            Value = op.Value;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Name object.</summary>
        /// <returns>A string that represents the Name object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Name({Target}, {Value}, {DebugName})";
        }
    }
}