using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Nop : Node
    {
        public Nop()
        {
        }

        public Nop(string debugName = null)
        {
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpNop;

        public Nop WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpNop)op, treeBuilder);
        }

        public Nop SetUp(Action<Nop> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpNop op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Nop object.</summary>
        /// <returns>A string that represents the Nop object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Nop({DebugName})";
        }
    }
}