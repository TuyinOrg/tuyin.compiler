using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SpecConstantTrue : Node
    {
        public SpecConstantTrue()
        {
        }

        public SpecConstantTrue(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSpecConstantTrue;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public SpecConstantTrue WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSpecConstantTrue)op, treeBuilder);
        }

        public SpecConstantTrue SetUp(Action<SpecConstantTrue> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSpecConstantTrue op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SpecConstantTrue object.</summary>
        /// <returns>A string that represents the SpecConstantTrue object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SpecConstantTrue({ResultType}, {DebugName})";
        }
    }
}