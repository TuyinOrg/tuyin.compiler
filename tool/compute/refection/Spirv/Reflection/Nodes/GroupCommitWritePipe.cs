using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupCommitWritePipe : ExecutableNode, INodeWithNext
    {
        public GroupCommitWritePipe()
        {
        }

        public GroupCommitWritePipe(uint execution, Node pipe, Node reserveId, Node packetSize, Node packetAlignment, string debugName = null)
        {
            this.Execution = execution;
            this.Pipe = pipe;
            this.ReserveId = reserveId;
            this.PacketSize = packetSize;
            this.PacketAlignment = packetAlignment;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupCommitWritePipe;

        /// <summary>
        /// Next operation in sequence
        /// </summary>
        public ExecutableNode Next { get; set; }

        public override ExecutableNode GetNext()
        {
            return Next;
        }

        public T Then<T>(T node) where T: ExecutableNode
        {
            Next = node;
            return node;
        }

        public uint Execution { get; set; }

        public Node Pipe { get; set; }

        public Node ReserveId { get; set; }

        public Node PacketSize { get; set; }

        public Node PacketAlignment { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pipe;
                yield return ReserveId;
                yield return PacketSize;
                yield return PacketAlignment;
        }

        public GroupCommitWritePipe WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupCommitWritePipe)op, treeBuilder);
        }

        public GroupCommitWritePipe SetUp(Action<GroupCommitWritePipe> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupCommitWritePipe op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Execution = op.Execution;
            Pipe = treeBuilder.GetNode(op.Pipe);
            ReserveId = treeBuilder.GetNode(op.ReserveId);
            PacketSize = treeBuilder.GetNode(op.PacketSize);
            PacketAlignment = treeBuilder.GetNode(op.PacketAlignment);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupCommitWritePipe object.</summary>
        /// <returns>A string that represents the GroupCommitWritePipe object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupCommitWritePipe({Execution}, {Pipe}, {ReserveId}, {PacketSize}, {PacketAlignment}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static GroupCommitWritePipe ThenGroupCommitWritePipe(this INodeWithNext node, uint execution, Node pipe, Node reserveId, Node packetSize, Node packetAlignment, string debugName = null)
        {
            return node.Then(new GroupCommitWritePipe(execution, pipe, reserveId, packetSize, packetAlignment, debugName));
        }
    }
}