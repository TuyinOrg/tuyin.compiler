using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ConstantPipeStorage : Node
    {
        public ConstantPipeStorage()
        {
        }

        public ConstantPipeStorage(SpirvTypeBase resultType, uint packetSize, uint packetAlignment, uint capacity, string debugName = null)
        {
            this.ResultType = resultType;
            this.PacketSize = packetSize;
            this.PacketAlignment = packetAlignment;
            this.Capacity = capacity;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpConstantPipeStorage;

        public uint PacketSize { get; set; }

        public uint PacketAlignment { get; set; }

        public uint Capacity { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public ConstantPipeStorage WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpConstantPipeStorage)op, treeBuilder);
        }

        public ConstantPipeStorage SetUp(Action<ConstantPipeStorage> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpConstantPipeStorage op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            PacketSize = op.PacketSize;
            PacketAlignment = op.PacketAlignment;
            Capacity = op.Capacity;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ConstantPipeStorage object.</summary>
        /// <returns>A string that represents the ConstantPipeStorage object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ConstantPipeStorage({ResultType}, {PacketSize}, {PacketAlignment}, {Capacity}, {DebugName})";
        }
    }
}