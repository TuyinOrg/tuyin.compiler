using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupShuffleDownINTEL : Node
    {
        public SubgroupShuffleDownINTEL()
        {
        }

        public SubgroupShuffleDownINTEL(SpirvTypeBase resultType, Node current, Node next, Node delta, string debugName = null)
        {
            this.ResultType = resultType;
            this.Current = current;
            this.Next = next;
            this.Delta = delta;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupShuffleDownINTEL;

        public Node Current { get; set; }

        public Node Next { get; set; }

        public Node Delta { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Current;
                yield return Next;
                yield return Delta;
        }

        public SubgroupShuffleDownINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupShuffleDownINTEL)op, treeBuilder);
        }

        public SubgroupShuffleDownINTEL SetUp(Action<SubgroupShuffleDownINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupShuffleDownINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Current = treeBuilder.GetNode(op.Current);
            Next = treeBuilder.GetNode(op.Next);
            Delta = treeBuilder.GetNode(op.Delta);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupShuffleDownINTEL object.</summary>
        /// <returns>A string that represents the SubgroupShuffleDownINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupShuffleDownINTEL({ResultType}, {Current}, {Next}, {Delta}, {DebugName})";
        }
    }
}