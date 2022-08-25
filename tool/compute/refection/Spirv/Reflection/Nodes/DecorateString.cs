using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class DecorateString : Node
    {
        public DecorateString()
        {
        }

        public DecorateString(Node target, Spv.Decoration decoration, string debugName = null)
        {
            this.Target = target;
            this.Decoration = decoration;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpDecorateString;

        public Node Target { get; set; }

        public Spv.Decoration Decoration { get; set; }

        public DecorateString WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpDecorateString)op, treeBuilder);
        }

        public DecorateString SetUp(Action<DecorateString> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpDecorateString op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Target = treeBuilder.GetNode(op.Target);
            Decoration = op.Decoration;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the DecorateString object.</summary>
        /// <returns>A string that represents the DecorateString object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"DecorateString({Target}, {Decoration}, {DebugName})";
        }
    }
}