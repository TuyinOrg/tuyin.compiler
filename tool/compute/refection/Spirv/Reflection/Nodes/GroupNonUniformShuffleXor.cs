using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupNonUniformShuffleXor : Node
    {
        public GroupNonUniformShuffleXor()
        {
        }

        public GroupNonUniformShuffleXor(SpirvTypeBase resultType, uint execution, Node value, Node mask, string debugName = null)
        {
            this.ResultType = resultType;
            this.Execution = execution;
            this.Value = value;
            this.Mask = mask;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupNonUniformShuffleXor;

        public uint Execution { get; set; }

        public Node Value { get; set; }

        public Node Mask { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Value;
                yield return Mask;
        }

        public GroupNonUniformShuffleXor WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupNonUniformShuffleXor)op, treeBuilder);
        }

        public GroupNonUniformShuffleXor SetUp(Action<GroupNonUniformShuffleXor> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupNonUniformShuffleXor op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Execution = op.Execution;
            Value = treeBuilder.GetNode(op.Value);
            Mask = treeBuilder.GetNode(op.Mask);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupNonUniformShuffleXor object.</summary>
        /// <returns>A string that represents the GroupNonUniformShuffleXor object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupNonUniformShuffleXor({ResultType}, {Execution}, {Value}, {Mask}, {DebugName})";
        }
    }
}