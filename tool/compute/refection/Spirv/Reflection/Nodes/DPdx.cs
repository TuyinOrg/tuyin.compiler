using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class DPdx : Node
    {
        public DPdx()
        {
        }

        public DPdx(SpirvTypeBase resultType, Node p, string debugName = null)
        {
            this.ResultType = resultType;
            this.P = p;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpDPdx;

        public Node P { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return P;
        }

        public DPdx WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpDPdx)op, treeBuilder);
        }

        public DPdx SetUp(Action<DPdx> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpDPdx op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            P = treeBuilder.GetNode(op.P);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the DPdx object.</summary>
        /// <returns>A string that represents the DPdx object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"DPdx({ResultType}, {P}, {DebugName})";
        }
    }
}