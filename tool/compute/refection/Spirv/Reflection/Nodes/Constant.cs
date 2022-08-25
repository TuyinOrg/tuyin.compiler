using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Constant : Node
    {
        public Constant()
        {
        }

        public Constant(SpirvTypeBase resultType, Operands.NumberLiteral value, string debugName = null)
        {
            this.ResultType = resultType;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpConstant;

        public Operands.NumberLiteral Value { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public Constant WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpConstant)op, treeBuilder);
        }

        public Constant SetUp(Action<Constant> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpConstant op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Value = op.Value;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Constant object.</summary>
        /// <returns>A string that represents the Constant object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Constant({ResultType}, {Value}, {DebugName})";
        }
    }
}