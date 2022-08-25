using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Not : Node
    {
        public Not()
        {
        }

        public Not(SpirvTypeBase resultType, Node operand, string debugName = null)
        {
            this.ResultType = resultType;
            this.Operand = operand;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpNot;

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

        public Not WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpNot)op, treeBuilder);
        }

        public Not SetUp(Action<Not> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpNot op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Operand = treeBuilder.GetNode(op.Operand);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Not object.</summary>
        /// <returns>A string that represents the Not object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Not({ResultType}, {Operand}, {DebugName})";
        }
    }
}