using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class IsValidEvent : Node
    {
        public IsValidEvent()
        {
        }

        public IsValidEvent(SpirvTypeBase resultType, Node @event, string debugName = null)
        {
            this.ResultType = resultType;
            this.Event = @event;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpIsValidEvent;

        public Node Event { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Event;
        }

        public IsValidEvent WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpIsValidEvent)op, treeBuilder);
        }

        public IsValidEvent SetUp(Action<IsValidEvent> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpIsValidEvent op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Event = treeBuilder.GetNode(op.Event);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the IsValidEvent object.</summary>
        /// <returns>A string that represents the IsValidEvent object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"IsValidEvent({ResultType}, {Event}, {DebugName})";
        }
    }
}