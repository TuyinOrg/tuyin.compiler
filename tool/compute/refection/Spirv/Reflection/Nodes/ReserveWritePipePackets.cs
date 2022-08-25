using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ReserveWritePipePackets : Node
    {
        public ReserveWritePipePackets()
        {
        }

        public ReserveWritePipePackets(SpirvTypeBase resultType, Node pipe, Node numPackets, Node packetSize, Node packetAlignment, string debugName = null)
        {
            this.ResultType = resultType;
            this.Pipe = pipe;
            this.NumPackets = numPackets;
            this.PacketSize = packetSize;
            this.PacketAlignment = packetAlignment;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpReserveWritePipePackets;

        public Node Pipe { get; set; }

        public Node NumPackets { get; set; }

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
                yield return NumPackets;
                yield return PacketSize;
                yield return PacketAlignment;
        }

        public ReserveWritePipePackets WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpReserveWritePipePackets)op, treeBuilder);
        }

        public ReserveWritePipePackets SetUp(Action<ReserveWritePipePackets> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpReserveWritePipePackets op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Pipe = treeBuilder.GetNode(op.Pipe);
            NumPackets = treeBuilder.GetNode(op.NumPackets);
            PacketSize = treeBuilder.GetNode(op.PacketSize);
            PacketAlignment = treeBuilder.GetNode(op.PacketAlignment);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ReserveWritePipePackets object.</summary>
        /// <returns>A string that represents the ReserveWritePipePackets object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ReserveWritePipePackets({ResultType}, {Pipe}, {NumPackets}, {PacketSize}, {PacketAlignment}, {DebugName})";
        }
    }
}