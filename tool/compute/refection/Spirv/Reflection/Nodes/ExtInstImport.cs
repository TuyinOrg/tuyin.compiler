using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ExtInstImport : Node
    {
        public ExtInstImport()
        {
        }

        public ExtInstImport(string name, string debugName = null)
        {
            this.Name = name;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpExtInstImport;

        public string Name { get; set; }

        public ExtInstImport WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpExtInstImport)op, treeBuilder);
        }

        public ExtInstImport SetUp(Action<ExtInstImport> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpExtInstImport op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Name = op.Name;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ExtInstImport object.</summary>
        /// <returns>A string that represents the ExtInstImport object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ExtInstImport({Name}, {DebugName})";
        }
    }
}