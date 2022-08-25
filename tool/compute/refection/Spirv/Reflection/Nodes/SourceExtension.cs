using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SourceExtension : Node
    {
        public SourceExtension()
        {
        }

        public SourceExtension(string extension, string debugName = null)
        {
            this.Extension = extension;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSourceExtension;

        public string Extension { get; set; }

        public SourceExtension WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSourceExtension)op, treeBuilder);
        }

        public SourceExtension SetUp(Action<SourceExtension> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSourceExtension op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Extension = op.Extension;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SourceExtension object.</summary>
        /// <returns>A string that represents the SourceExtension object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SourceExtension({Extension}, {DebugName})";
        }
    }
}