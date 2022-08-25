using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupNonUniformQuadBroadcast : Node
    {
        public GroupNonUniformQuadBroadcast()
        {
        }

        public GroupNonUniformQuadBroadcast(SpirvTypeBase resultType, uint execution, Node value, Node index, string debugName = null)
        {
            this.ResultType = resultType;
            this.Execution = execution;
            this.Value = value;
            this.Index = index;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupNonUniformQuadBroadcast;

        public uint Execution { get; set; }

        public Node Value { get; set; }

        public Node Index { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Value;
                yield return Index;
        }

        public GroupNonUniformQuadBroadcast WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupNonUniformQuadBroadcast)op, treeBuilder);
        }

        public GroupNonUniformQuadBroadcast SetUp(Action<GroupNonUniformQuadBroadcast> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupNonUniformQuadBroadcast op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Execution = op.Execution;
            Value = treeBuilder.GetNode(op.Value);
            Index = treeBuilder.GetNode(op.Index);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupNonUniformQuadBroadcast object.</summary>
        /// <returns>A string that represents the GroupNonUniformQuadBroadcast object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupNonUniformQuadBroadcast({ResultType}, {Execution}, {Value}, {Index}, {DebugName})";
        }
    }
}