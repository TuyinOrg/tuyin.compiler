using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Capability : Node
    {
        public Capability()
        {
        }

        public Capability(Spv.Capability value, string debugName = null)
        {
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCapability;

        public Spv.Capability Value { get; set; }

        public Capability WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCapability)op, treeBuilder);
        }

        public Capability SetUp(Action<Capability> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCapability op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Value = op.Value;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Capability object.</summary>
        /// <returns>A string that represents the Capability object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Capability({Value}, {DebugName})";
        }
    }
}