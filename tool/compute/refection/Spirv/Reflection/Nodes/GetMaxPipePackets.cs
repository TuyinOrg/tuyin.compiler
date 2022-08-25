using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GetMaxPipePackets : Node
    {
        public GetMaxPipePackets()
        {
        }

        public GetMaxPipePackets(SpirvTypeBase resultType, Node pipe, Node packetSize, Node packetAlignment, string debugName = null)
        {
            this.ResultType = resultType;
            this.Pipe = pipe;
            this.PacketSize = packetSize;
            this.PacketAlignment = packetAlignment;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGetMaxPipePackets;

        public Node Pipe { get; set; }

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
                yield return PacketSize;
                yield return PacketAlignment;
        }

        public GetMaxPipePackets WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGetMaxPipePackets)op, treeBuilder);
        }

        public GetMaxPipePackets SetUp(Action<GetMaxPipePackets> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGetMaxPipePackets op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Pipe = treeBuilder.GetNode(op.Pipe);
            PacketSize = treeBuilder.GetNode(op.PacketSize);
            PacketAlignment = treeBuilder.GetNode(op.PacketAlignment);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GetMaxPipePackets object.</summary>
        /// <returns>A string that represents the GetMaxPipePackets object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GetMaxPipePackets({ResultType}, {Pipe}, {PacketSize}, {PacketAlignment}, {DebugName})";
        }
    }
}