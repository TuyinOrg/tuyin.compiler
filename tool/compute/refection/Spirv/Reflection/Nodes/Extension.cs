using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Extension : Node
    {
        public Extension()
        {
        }

        public Extension(string name, string debugName = null)
        {
            this.Name = name;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpExtension;

        public string Name { get; set; }

        public Extension WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpExtension)op, treeBuilder);
        }

        public Extension SetUp(Action<Extension> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpExtension op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Name = op.Name;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Extension object.</summary>
        /// <returns>A string that represents the Extension object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Extension({Name}, {DebugName})";
        }
    }
}