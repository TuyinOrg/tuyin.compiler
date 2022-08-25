using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAnyKHR : Node
    {
        public SubgroupAnyKHR()
        {
        }

        public SubgroupAnyKHR(SpirvTypeBase resultType, Node predicate, string debugName = null)
        {
            this.ResultType = resultType;
            this.Predicate = predicate;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAnyKHR;

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

        public SubgroupAnyKHR WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAnyKHR)op, treeBuilder);
        }

        public SubgroupAnyKHR SetUp(Action<SubgroupAnyKHR> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAnyKHR op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Predicate = treeBuilder.GetNode(op.Predicate);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAnyKHR object.</summary>
        /// <returns>A string that represents the SubgroupAnyKHR object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAnyKHR({ResultType}, {Predicate}, {DebugName})";
        }
    }
}