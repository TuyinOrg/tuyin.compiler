using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CopyLogical : Node
    {
        public CopyLogical()
        {
        }

        public CopyLogical(SpirvTypeBase resultType, Node operand, string debugName = null)
        {
            this.ResultType = resultType;
            this.Operand = operand;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCopyLogical;

        public Node Operand { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Operand;
        }

        public CopyLogical WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCopyLogical)op, treeBuilder);
        }

        public CopyLogical SetUp(Action<CopyLogical> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCopyLogical op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Operand = treeBuilder.GetNode(op.Operand);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CopyLogical object.</summary>
        /// <returns>A string that represents the CopyLogical object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CopyLogical({ResultType}, {Operand}, {DebugName})";
        }
    }
}