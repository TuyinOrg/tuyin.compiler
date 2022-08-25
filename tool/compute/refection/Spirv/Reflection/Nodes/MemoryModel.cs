using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class MemoryModel : Node
    {
        public MemoryModel()
        {
        }

        public MemoryModel(Spv.AddressingModel addressingModel, Spv.MemoryModel value, string debugName = null)
        {
            this.AddressingModel = addressingModel;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpMemoryModel;

        public Spv.AddressingModel AddressingModel { get; set; }

        public Spv.MemoryModel Value { get; set; }

        public MemoryModel WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpMemoryModel)op, treeBuilder);
        }

        public MemoryModel SetUp(Action<MemoryModel> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpMemoryModel op, SpirvInstructionTreeBuilder treeBuilder)
        {
            AddressingModel = op.AddressingModel;
            Value = op.Value;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the MemoryModel object.</summary>
        /// <returns>A string that represents the MemoryModel object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"MemoryModel({AddressingModel}, {Value}, {DebugName})";
        }
    }
}