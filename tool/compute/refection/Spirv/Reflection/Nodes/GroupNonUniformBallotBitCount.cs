using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupNonUniformBallotBitCount : Node
    {
        public GroupNonUniformBallotBitCount()
        {
        }

        public GroupNonUniformBallotBitCount(SpirvTypeBase resultType, uint execution, Spv.GroupOperation operation, Node value, string debugName = null)
        {
            this.ResultType = resultType;
            this.Execution = execution;
            this.Operation = operation;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupNonUniformBallotBitCount;

        public uint Execution { get; set; }

        public Spv.GroupOperation Operation { get; set; }

        public Node Value { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Value;
        }

        public GroupNonUniformBallotBitCount WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupNonUniformBallotBitCount)op, treeBuilder);
        }

        public GroupNonUniformBallotBitCount SetUp(Action<GroupNonUniformBallotBitCount> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupNonUniformBallotBitCount op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Execution = op.Execution;
            Operation = op.Operation;
            Value = treeBuilder.GetNode(op.Value);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupNonUniformBallotBitCount object.</summary>
        /// <returns>A string that represents the GroupNonUniformBallotBitCount object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupNonUniformBallotBitCount({ResultType}, {Execution}, {Operation}, {Value}, {DebugName})";
        }
    }
}