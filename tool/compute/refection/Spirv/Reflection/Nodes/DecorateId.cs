using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class DecorateId : Node
    {
        public DecorateId()
        {
        }

        public DecorateId(Node target, Spv.Decoration decoration, string debugName = null)
        {
            this.Target = target;
            this.Decoration = decoration;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpDecorateId;

        public Node Target { get; set; }

        public Spv.Decoration Decoration { get; set; }

        public DecorateId WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpDecorateId)op, treeBuilder);
        }

        public DecorateId SetUp(Action<DecorateId> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpDecorateId op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Target = treeBuilder.GetNode(op.Target);
            Decoration = op.Decoration;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the DecorateId object.</summary>
        /// <returns>A string that represents the DecorateId object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"DecorateId({Target}, {Decoration}, {DebugName})";
        }
    }
}