using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class String : Node
    {
        public String()
        {
        }

        public String(string value, string debugName = null)
        {
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpString;

        public string Value { get; set; }

        public String WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpString)op, treeBuilder);
        }

        public String SetUp(Action<String> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpString op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Value = op.Value;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the String object.</summary>
        /// <returns>A string that represents the String object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"String({Value}, {DebugName})";
        }
    }
}