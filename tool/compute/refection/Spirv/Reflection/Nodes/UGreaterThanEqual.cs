using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class UGreaterThanEqual : Node
    {
        public UGreaterThanEqual()
        {
        }

        public UGreaterThanEqual(SpirvTypeBase resultType, Node operand1, Node operand2, string debugName = null)
        {
            this.ResultType = resultType;
            this.Operand1 = operand1;
            this.Operand2 = operand2;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpUGreaterThanEqual;

        public Node Operand1 { get; set; }

        public Node Operand2 { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Operand1;
                yield return Operand2;
        }

        public UGreaterThanEqual WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpUGreaterThanEqual)op, treeBuilder);
        }

        public UGreaterThanEqual SetUp(Action<UGreaterThanEqual> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpUGreaterThanEqual op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Operand1 = treeBuilder.GetNode(op.Operand1);
            Operand2 = treeBuilder.GetNode(op.Operand2);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the UGreaterThanEqual object.</summary>
        /// <returns>A string that represents the UGreaterThanEqual object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"UGreaterThanEqual({ResultType}, {Operand1}, {Operand2}, {DebugName})";
        }
    }
}