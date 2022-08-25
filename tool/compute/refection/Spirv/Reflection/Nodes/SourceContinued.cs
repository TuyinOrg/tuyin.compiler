using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SourceContinued : Node
    {
        public SourceContinued()
        {
        }

        public SourceContinued(string continuedSource, string debugName = null)
        {
            this.ContinuedSource = continuedSource;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSourceContinued;

        public string ContinuedSource { get; set; }

        public SourceContinued WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSourceContinued)op, treeBuilder);
        }

        public SourceContinued SetUp(Action<SourceContinued> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSourceContinued op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ContinuedSource = op.ContinuedSource;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SourceContinued object.</summary>
        /// <returns>A string that represents the SourceContinued object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SourceContinued({ContinuedSource}, {DebugName})";
        }
    }
}