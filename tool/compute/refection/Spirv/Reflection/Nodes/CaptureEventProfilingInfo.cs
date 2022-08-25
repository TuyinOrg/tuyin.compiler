using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CaptureEventProfilingInfo : ExecutableNode, INodeWithNext
    {
        public CaptureEventProfilingInfo()
        {
        }

        public CaptureEventProfilingInfo(Node @event, Node profilingInfo, Node value, string debugName = null)
        {
            this.Event = @event;
            this.ProfilingInfo = profilingInfo;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCaptureEventProfilingInfo;

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

        public Node ProfilingInfo { get; set; }

        public Node Value { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Event;
                yield return ProfilingInfo;
                yield return Value;
        }

        public CaptureEventProfilingInfo WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCaptureEventProfilingInfo)op, treeBuilder);
        }

        public CaptureEventProfilingInfo SetUp(Action<CaptureEventProfilingInfo> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCaptureEventProfilingInfo op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Event = treeBuilder.GetNode(op.Event);
            ProfilingInfo = treeBuilder.GetNode(op.ProfilingInfo);
            Value = treeBuilder.GetNode(op.Value);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CaptureEventProfilingInfo object.</summary>
        /// <returns>A string that represents the CaptureEventProfilingInfo object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CaptureEventProfilingInfo({Event}, {ProfilingInfo}, {Value}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static CaptureEventProfilingInfo ThenCaptureEventProfilingInfo(this INodeWithNext node, Node @event, Node profilingInfo, Node value, string debugName = null)
        {
            return node.Then(new CaptureEventProfilingInfo(@event, profilingInfo, value, debugName));
        }
    }
}