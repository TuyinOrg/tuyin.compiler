using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ReadPipe : Node
    {
        public ReadPipe()
        {
        }

        public ReadPipe(SpirvTypeBase resultType, Node pipe, Node pointer, Node packetSize, Node packetAlignment, string debugName = null)
        {
            this.ResultType = resultType;
            this.Pipe = pipe;
            this.Pointer = pointer;
            this.PacketSize = packetSize;
            this.PacketAlignment = packetAlignment;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpReadPipe;

        public Node Pipe { get; set; }

        public Node Pointer { get; set; }

        public Node PacketSize { get; set; }

        public Node PacketAlignment { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pipe;
                yield return Pointer;
                yield return PacketSize;
                yield return PacketAlignment;
        }

        public ReadPipe WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpReadPipe)op, treeBuilder);
        }

        public ReadPipe SetUp(Action<ReadPipe> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpReadPipe op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Pipe = treeBuilder.GetNode(op.Pipe);
            Pointer = treeBuilder.GetNode(op.Pointer);
            PacketSize = treeBuilder.GetNode(op.PacketSize);
            PacketAlignment = treeBuilder.GetNode(op.PacketAlignment);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ReadPipe object.</summary>
        /// <returns>A string that represents the ReadPipe object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ReadPipe({ResultType}, {Pipe}, {Pointer}, {PacketSize}, {PacketAlignment}, {DebugName})";
        }
    }
}