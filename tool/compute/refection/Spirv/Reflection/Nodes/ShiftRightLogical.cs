using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ShiftRightLogical : Node
    {
        public ShiftRightLogical()
        {
        }

        public ShiftRightLogical(SpirvTypeBase resultType, Node @base, Node shift, string debugName = null)
        {
            this.ResultType = resultType;
            this.Base = @base;
            this.Shift = shift;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpShiftRightLogical;

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

        public ShiftRightLogical WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpShiftRightLogical)op, treeBuilder);
        }

        public ShiftRightLogical SetUp(Action<ShiftRightLogical> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpShiftRightLogical op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Base = treeBuilder.GetNode(op.Base);
            Shift = treeBuilder.GetNode(op.Shift);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ShiftRightLogical object.</summary>
        /// <returns>A string that represents the ShiftRightLogical object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ShiftRightLogical({ResultType}, {Base}, {Shift}, {DebugName})";
        }
    }
}