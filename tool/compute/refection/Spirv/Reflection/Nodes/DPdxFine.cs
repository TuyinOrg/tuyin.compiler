using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class DPdxFine : Node
    {
        public DPdxFine()
        {
        }

        public DPdxFine(SpirvTypeBase resultType, Node p, string debugName = null)
        {
            this.ResultType = resultType;
            this.P = p;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpDPdxFine;

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

        public DPdxFine WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpDPdxFine)op, treeBuilder);
        }

        public DPdxFine SetUp(Action<DPdxFine> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpDPdxFine op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            P = treeBuilder.GetNode(op.P);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the DPdxFine object.</summary>
        /// <returns>A string that represents the DPdxFine object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"DPdxFine({ResultType}, {P}, {DebugName})";
        }
    }
}