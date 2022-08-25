using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Undef : Node
    {
        public Undef()
        {
        }

        public Undef(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpUndef;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public Undef WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpUndef)op, treeBuilder);
        }

        public Undef SetUp(Action<Undef> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpUndef op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Undef object.</summary>
        /// <returns>A string that represents the Undef object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Undef({ResultType}, {DebugName})";
        }
    }
}