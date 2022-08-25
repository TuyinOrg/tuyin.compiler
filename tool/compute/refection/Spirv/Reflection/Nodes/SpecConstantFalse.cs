using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SpecConstantFalse : Node
    {
        public SpecConstantFalse()
        {
        }

        public SpecConstantFalse(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSpecConstantFalse;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public SpecConstantFalse WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSpecConstantFalse)op, treeBuilder);
        }

        public SpecConstantFalse SetUp(Action<SpecConstantFalse> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSpecConstantFalse op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SpecConstantFalse object.</summary>
        /// <returns>A string that represents the SpecConstantFalse object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SpecConstantFalse({ResultType}, {DebugName})";
        }
    }
}