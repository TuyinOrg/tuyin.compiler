using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SetUserEventStatus : ExecutableNode, INodeWithNext
    {
        public SetUserEventStatus()
        {
        }

        public SetUserEventStatus(Node @event, Node status, string debugName = null)
        {
            this.Event = @event;
            this.Status = status;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSetUserEventStatus;

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

        public Node Event { get; set; }

        public Node Status { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Event;
                yield return Status;
        }

        public SetUserEventStatus WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSetUserEventStatus)op, treeBuilder);
        }

        public SetUserEventStatus SetUp(Action<SetUserEventStatus> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSetUserEventStatus op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Event = treeBuilder.GetNode(op.Event);
            Status = treeBuilder.GetNode(op.Status);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SetUserEventStatus object.</summary>
        /// <returns>A string that represents the SetUserEventStatus object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SetUserEventStatus({Event}, {Status}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static SetUserEventStatus ThenSetUserEventStatus(this INodeWithNext node, Node @event, Node status, string debugName = null)
        {
            return node.Then(new SetUserEventStatus(@event, status, debugName));
        }
    }
}