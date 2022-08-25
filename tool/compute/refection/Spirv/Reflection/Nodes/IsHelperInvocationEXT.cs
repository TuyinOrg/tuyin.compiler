using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class IsHelperInvocationEXT : Node
    {
        public IsHelperInvocationEXT()
        {
        }

        public IsHelperInvocationEXT(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpIsHelperInvocationEXT;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public IsHelperInvocationEXT WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpIsHelperInvocationEXT)op, treeBuilder);
        }

        public IsHelperInvocationEXT SetUp(Action<IsHelperInvocationEXT> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpIsHelperInvocationEXT op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the IsHelperInvocationEXT object.</summary>
        /// <returns>A string that represents the IsHelperInvocationEXT object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"IsHelperInvocationEXT({ResultType}, {DebugName})";
        }
    }
}