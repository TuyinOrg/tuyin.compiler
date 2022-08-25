using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CreateUserEvent : Node
    {
        public CreateUserEvent()
        {
        }

        public CreateUserEvent(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCreateUserEvent;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public CreateUserEvent WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCreateUserEvent)op, treeBuilder);
        }

        public CreateUserEvent SetUp(Action<CreateUserEvent> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCreateUserEvent op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CreateUserEvent object.</summary>
        /// <returns>A string that represents the CreateUserEvent object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CreateUserEvent({ResultType}, {DebugName})";
        }
    }
}