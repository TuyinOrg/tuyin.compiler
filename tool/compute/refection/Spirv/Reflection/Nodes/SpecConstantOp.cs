using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SpecConstantOp : Node
    {
        public SpecConstantOp()
        {
        }

        public SpecConstantOp(SpirvTypeBase resultType, NestedNode opcode, string debugName = null)
        {
            this.ResultType = resultType;
            this.Opcode = opcode;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSpecConstantOp;

        public NestedNode Opcode { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public SpecConstantOp WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSpecConstantOp)op, treeBuilder);
        }

        public SpecConstantOp SetUp(Action<SpecConstantOp> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSpecConstantOp op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Opcode = treeBuilder.Parse(op.Opcode);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SpecConstantOp object.</summary>
        /// <returns>A string that represents the SpecConstantOp object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SpecConstantOp({ResultType}, {Opcode}, {DebugName})";
        }
    }
}