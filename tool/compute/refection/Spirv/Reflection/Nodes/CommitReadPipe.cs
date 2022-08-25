using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CommitReadPipe : ExecutableNode, INodeWithNext
    {
        public CommitReadPipe()
        {
        }

        public CommitReadPipe(Node pipe, Node reserveId, Node packetSize, Node packetAlignment, string debugName = null)
        {
            this.Pipe = pipe;
            this.ReserveId = reserveId;
            this.PacketSize = packetSize;
            this.PacketAlignment = packetAlignment;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCommitReadPipe;

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

        public CommitReadPipe WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCommitReadPipe)op, treeBuilder);
        }

        public CommitReadPipe SetUp(Action<CommitReadPipe> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCommitReadPipe op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Pipe = treeBuilder.GetNode(op.Pipe);
            ReserveId = treeBuilder.GetNode(op.ReserveId);
            PacketSize = treeBuilder.GetNode(op.PacketSize);
            PacketAlignment = treeBuilder.GetNode(op.PacketAlignment);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CommitReadPipe object.</summary>
        /// <returns>A string that represents the CommitReadPipe object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CommitReadPipe({Pipe}, {ReserveId}, {PacketSize}, {PacketAlignment}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static CommitReadPipe ThenCommitReadPipe(this INodeWithNext node, Node pipe, Node reserveId, Node packetSize, Node packetAlignment, string debugName = null)
        {
            return node.Then(new CommitReadPipe(pipe, reserveId, packetSize, packetAlignment, debugName));
        }
    }
}