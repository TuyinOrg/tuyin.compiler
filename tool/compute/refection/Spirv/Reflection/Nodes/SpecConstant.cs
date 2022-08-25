using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SpecConstant : Node
    {
        public SpecConstant()
        {
        }

        public SpecConstant(SpirvTypeBase resultType, Operands.NumberLiteral value, string debugName = null)
        {
            this.ResultType = resultType;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSpecConstant;

        public Operands.NumberLiteral Value { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public SpecConstant WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSpecConstant)op, treeBuilder);
        }

        public SpecConstant SetUp(Action<SpecConstant> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSpecConstant op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Value = op.Value;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SpecConstant object.</summary>
        /// <returns>A string that represents the SpecConstant object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SpecConstant({ResultType}, {Value}, {DebugName})";
        }
    }
}