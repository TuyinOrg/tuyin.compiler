using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupNonUniformBallot : Node
    {
        public GroupNonUniformBallot()
        {
        }

        public GroupNonUniformBallot(SpirvTypeBase resultType, uint execution, Node predicate, string debugName = null)
        {
            this.ResultType = resultType;
            this.Execution = execution;
            this.Predicate = predicate;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupNonUniformBallot;

        public uint Execution { get; set; }

        public Node Predicate { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Predicate;
        }

        public GroupNonUniformBallot WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupNonUniformBallot)op, treeBuilder);
        }

        public GroupNonUniformBallot SetUp(Action<GroupNonUniformBallot> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupNonUniformBallot op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Execution = op.Execution;
            Predicate = treeBuilder.GetNode(op.Predicate);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupNonUniformBallot object.</summary>
        /// <returns>A string that represents the GroupNonUniformBallot object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupNonUniformBallot({ResultType}, {Execution}, {Predicate}, {DebugName})";
        }
    }
}