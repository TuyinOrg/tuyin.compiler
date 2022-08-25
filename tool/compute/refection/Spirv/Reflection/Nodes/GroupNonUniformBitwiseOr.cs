using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupNonUniformBitwiseOr : Node
    {
        public GroupNonUniformBitwiseOr()
        {
        }

        public GroupNonUniformBitwiseOr(SpirvTypeBase resultType, uint execution, Spv.GroupOperation operation, Node value, Node clusterSize, string debugName = null)
        {
            this.ResultType = resultType;
            this.Execution = execution;
            this.Operation = operation;
            this.Value = value;
            this.ClusterSize = clusterSize;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupNonUniformBitwiseOr;

        public uint Execution { get; set; }

        public Spv.GroupOperation Operation { get; set; }

        public Node Value { get; set; }

        public Node ClusterSize { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Value;
                yield return ClusterSize;
        }

        public GroupNonUniformBitwiseOr WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupNonUniformBitwiseOr)op, treeBuilder);
        }

        public GroupNonUniformBitwiseOr SetUp(Action<GroupNonUniformBitwiseOr> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupNonUniformBitwiseOr op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Execution = op.Execution;
            Operation = op.Operation;
            Value = treeBuilder.GetNode(op.Value);
            ClusterSize = treeBuilder.GetNode(op.ClusterSize);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupNonUniformBitwiseOr object.</summary>
        /// <returns>A string that represents the GroupNonUniformBitwiseOr object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupNonUniformBitwiseOr({ResultType}, {Execution}, {Operation}, {Value}, {ClusterSize}, {DebugName})";
        }
    }
}