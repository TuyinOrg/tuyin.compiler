using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class FunctionParameter : Node
    {
        public FunctionParameter()
        {
        }

        public FunctionParameter(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpFunctionParameter;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public FunctionParameter WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpFunctionParameter)op, treeBuilder);
        }

        public FunctionParameter SetUp(Action<FunctionParameter> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpFunctionParameter op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the FunctionParameter object.</summary>
        /// <returns>A string that represents the FunctionParameter object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"FunctionParameter({ResultType}, {DebugName})";
        }
    }
}