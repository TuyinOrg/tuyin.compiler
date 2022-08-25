using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupBallotKHR : Node
    {
        public SubgroupBallotKHR()
        {
        }

        public SubgroupBallotKHR(SpirvTypeBase resultType, Node predicate, string debugName = null)
        {
            this.ResultType = resultType;
            this.Predicate = predicate;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupBallotKHR;

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

        public SubgroupBallotKHR WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupBallotKHR)op, treeBuilder);
        }

        public SubgroupBallotKHR SetUp(Action<SubgroupBallotKHR> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupBallotKHR op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Predicate = treeBuilder.GetNode(op.Predicate);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupBallotKHR object.</summary>
        /// <returns>A string that represents the SubgroupBallotKHR object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupBallotKHR({ResultType}, {Predicate}, {DebugName})";
        }
    }
}