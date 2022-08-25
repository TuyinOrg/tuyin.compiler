using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupNonUniformElect : Node
    {
        public GroupNonUniformElect()
        {
        }

        public GroupNonUniformElect(SpirvTypeBase resultType, uint execution, string debugName = null)
        {
            this.ResultType = resultType;
            this.Execution = execution;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupNonUniformElect;

        public uint Execution { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public GroupNonUniformElect WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupNonUniformElect)op, treeBuilder);
        }

        public GroupNonUniformElect SetUp(Action<GroupNonUniformElect> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupNonUniformElect op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Execution = op.Execution;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupNonUniformElect object.</summary>
        /// <returns>A string that represents the GroupNonUniformElect object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupNonUniformElect({ResultType}, {Execution}, {DebugName})";
        }
    }
}