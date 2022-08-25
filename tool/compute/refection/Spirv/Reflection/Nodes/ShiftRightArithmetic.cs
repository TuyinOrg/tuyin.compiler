using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ShiftRightArithmetic : Node
    {
        public ShiftRightArithmetic()
        {
        }

        public ShiftRightArithmetic(SpirvTypeBase resultType, Node @base, Node shift, string debugName = null)
        {
            this.ResultType = resultType;
            this.Base = @base;
            this.Shift = shift;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpShiftRightArithmetic;

        public Node Base { get; set; }

        public Node Shift { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Base;
                yield return Shift;
        }

        public ShiftRightArithmetic WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpShiftRightArithmetic)op, treeBuilder);
        }

        public ShiftRightArithmetic SetUp(Action<ShiftRightArithmetic> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpShiftRightArithmetic op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Base = treeBuilder.GetNode(op.Base);
            Shift = treeBuilder.GetNode(op.Shift);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ShiftRightArithmetic object.</summary>
        /// <returns>A string that represents the ShiftRightArithmetic object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ShiftRightArithmetic({ResultType}, {Base}, {Shift}, {DebugName})";
        }
    }
}