using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class EnqueueMarker : Node
    {
        public EnqueueMarker()
        {
        }

        public EnqueueMarker(SpirvTypeBase resultType, Node queue, Node numEvents, Node waitEvents, Node retEvent, string debugName = null)
        {
            this.ResultType = resultType;
            this.Queue = queue;
            this.NumEvents = numEvents;
            this.WaitEvents = waitEvents;
            this.RetEvent = retEvent;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpEnqueueMarker;

        public Node Queue { get; set; }

        public Node NumEvents { get; set; }

        public Node WaitEvents { get; set; }

        public Node RetEvent { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Queue;
                yield return NumEvents;
                yield return WaitEvents;
                yield return RetEvent;
        }

        public EnqueueMarker WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpEnqueueMarker)op, treeBuilder);
        }

        public EnqueueMarker SetUp(Action<EnqueueMarker> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpEnqueueMarker op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Queue = treeBuilder.GetNode(op.Queue);
            NumEvents = treeBuilder.GetNode(op.NumEvents);
            WaitEvents = treeBuilder.GetNode(op.WaitEvents);
            RetEvent = treeBuilder.GetNode(op.RetEvent);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the EnqueueMarker object.</summary>
        /// <returns>A string that represents the EnqueueMarker object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"EnqueueMarker({ResultType}, {Queue}, {NumEvents}, {WaitEvents}, {RetEvent}, {DebugName})";
        }
    }
}