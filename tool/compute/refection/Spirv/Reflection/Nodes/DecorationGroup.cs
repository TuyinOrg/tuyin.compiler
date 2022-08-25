using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class DecorationGroup : Node
    {
        public DecorationGroup()
        {
        }

        public DecorationGroup(string debugName = null)
        {
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpDecorationGroup;

        public DecorationGroup WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpDecorationGroup)op, treeBuilder);
        }

        public DecorationGroup SetUp(Action<DecorationGroup> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpDecorationGroup op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the DecorationGroup object.</summary>
        /// <returns>A string that represents the DecorationGroup object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"DecorationGroup({DebugName})";
        }
    }
}