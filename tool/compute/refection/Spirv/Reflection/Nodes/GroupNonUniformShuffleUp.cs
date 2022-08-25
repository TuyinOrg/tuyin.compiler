using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupNonUniformShuffleUp : Node
    {
        public GroupNonUniformShuffleUp()
        {
        }

        public GroupNonUniformShuffleUp(SpirvTypeBase resultType, uint execution, Node value, Node delta, string debugName = null)
        {
            this.ResultType = resultType;
            this.Execution = execution;
            this.Value = value;
            this.Delta = delta;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupNonUniformShuffleUp;

        public uint Execution { get; set; }

        public Node Value { get; set; }

        public Node Delta { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Value;
                yield return Delta;
        }

        public GroupNonUniformShuffleUp WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupNonUniformShuffleUp)op, treeBuilder);
        }

        public GroupNonUniformShuffleUp SetUp(Action<GroupNonUniformShuffleUp> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupNonUniformShuffleUp op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Execution = op.Execution;
            Value = treeBuilder.GetNode(op.Value);
            Delta = treeBuilder.GetNode(op.Delta);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupNonUniformShuffleUp object.</summary>
        /// <returns>A string that represents the GroupNonUniformShuffleUp object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupNonUniformShuffleUp({ResultType}, {Execution}, {Value}, {Delta}, {DebugName})";
        }
    }
}