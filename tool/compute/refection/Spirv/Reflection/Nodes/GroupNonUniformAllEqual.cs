using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupNonUniformAllEqual : Node
    {
        public GroupNonUniformAllEqual()
        {
        }

        public GroupNonUniformAllEqual(SpirvTypeBase resultType, uint execution, Node value, string debugName = null)
        {
            this.ResultType = resultType;
            this.Execution = execution;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupNonUniformAllEqual;

        public uint Execution { get; set; }

        public Node Value { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Value;
        }

        public GroupNonUniformAllEqual WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupNonUniformAllEqual)op, treeBuilder);
        }

        public GroupNonUniformAllEqual SetUp(Action<GroupNonUniformAllEqual> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupNonUniformAllEqual op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Execution = op.Execution;
            Value = treeBuilder.GetNode(op.Value);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupNonUniformAllEqual object.</summary>
        /// <returns>A string that represents the GroupNonUniformAllEqual object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupNonUniformAllEqual({ResultType}, {Execution}, {Value}, {DebugName})";
        }
    }
}