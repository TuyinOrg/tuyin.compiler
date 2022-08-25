using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ConstantFalse : Node
    {
        public ConstantFalse()
        {
        }

        public ConstantFalse(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpConstantFalse;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public ConstantFalse WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpConstantFalse)op, treeBuilder);
        }

        public ConstantFalse SetUp(Action<ConstantFalse> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpConstantFalse op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ConstantFalse object.</summary>
        /// <returns>A string that represents the ConstantFalse object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ConstantFalse({ResultType}, {DebugName})";
        }
    }
}