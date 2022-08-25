using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupNonUniformShuffle : Node
    {
        public GroupNonUniformShuffle()
        {
        }

        public GroupNonUniformShuffle(SpirvTypeBase resultType, uint execution, Node value, Node id, string debugName = null)
        {
            this.ResultType = resultType;
            this.Execution = execution;
            this.Value = value;
            this.Id = id;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupNonUniformShuffle;

        public uint Execution { get; set; }

        public Node Value { get; set; }

        public Node Id { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Value;
                yield return Id;
        }

        public GroupNonUniformShuffle WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupNonUniformShuffle)op, treeBuilder);
        }

        public GroupNonUniformShuffle SetUp(Action<GroupNonUniformShuffle> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupNonUniformShuffle op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Execution = op.Execution;
            Value = treeBuilder.GetNode(op.Value);
            Id = treeBuilder.GetNode(op.Id);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupNonUniformShuffle object.</summary>
        /// <returns>A string that represents the GroupNonUniformShuffle object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupNonUniformShuffle({ResultType}, {Execution}, {Value}, {Id}, {DebugName})";
        }
    }
}