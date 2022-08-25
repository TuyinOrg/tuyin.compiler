using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GetDefaultQueue : Node
    {
        public GetDefaultQueue()
        {
        }

        public GetDefaultQueue(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGetDefaultQueue;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public GetDefaultQueue WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGetDefaultQueue)op, treeBuilder);
        }

        public GetDefaultQueue SetUp(Action<GetDefaultQueue> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGetDefaultQueue op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GetDefaultQueue object.</summary>
        /// <returns>A string that represents the GetDefaultQueue object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GetDefaultQueue({ResultType}, {DebugName})";
        }
    }
}