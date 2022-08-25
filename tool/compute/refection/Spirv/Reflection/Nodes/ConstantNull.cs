using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ConstantNull : Node
    {
        public ConstantNull()
        {
        }

        public ConstantNull(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpConstantNull;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public ConstantNull WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpConstantNull)op, treeBuilder);
        }

        public ConstantNull SetUp(Action<ConstantNull> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpConstantNull op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ConstantNull object.</summary>
        /// <returns>A string that represents the ConstantNull object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ConstantNull({ResultType}, {DebugName})";
        }
    }
}