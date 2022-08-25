using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupBroadcast : Node
    {
        public GroupBroadcast()
        {
        }

        public GroupBroadcast(SpirvTypeBase resultType, uint execution, Node value, Node localId, string debugName = null)
        {
            this.ResultType = resultType;
            this.Execution = execution;
            this.Value = value;
            this.LocalId = localId;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupBroadcast;

        public uint Execution { get; set; }

        public Node Value { get; set; }

        public Node LocalId { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Value;
                yield return LocalId;
        }

        public GroupBroadcast WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupBroadcast)op, treeBuilder);
        }

        public GroupBroadcast SetUp(Action<GroupBroadcast> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupBroadcast op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Execution = op.Execution;
            Value = treeBuilder.GetNode(op.Value);
            LocalId = treeBuilder.GetNode(op.LocalId);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupBroadcast object.</summary>
        /// <returns>A string that represents the GroupBroadcast object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupBroadcast({ResultType}, {Execution}, {Value}, {LocalId}, {DebugName})";
        }
    }
}