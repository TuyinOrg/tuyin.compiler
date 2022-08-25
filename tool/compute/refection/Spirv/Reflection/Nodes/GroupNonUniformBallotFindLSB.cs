using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupNonUniformBallotFindLSB : Node
    {
        public GroupNonUniformBallotFindLSB()
        {
        }

        public GroupNonUniformBallotFindLSB(SpirvTypeBase resultType, uint execution, Node value, string debugName = null)
        {
            this.ResultType = resultType;
            this.Execution = execution;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupNonUniformBallotFindLSB;

        public uint Execution { get; set; }

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

        public GroupNonUniformBallotFindLSB WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupNonUniformBallotFindLSB)op, treeBuilder);
        }

        public GroupNonUniformBallotFindLSB SetUp(Action<GroupNonUniformBallotFindLSB> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupNonUniformBallotFindLSB op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Execution = op.Execution;
            Value = treeBuilder.GetNode(op.Value);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupNonUniformBallotFindLSB object.</summary>
        /// <returns>A string that represents the GroupNonUniformBallotFindLSB object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupNonUniformBallotFindLSB({ResultType}, {Execution}, {Value}, {DebugName})";
        }
    }
}