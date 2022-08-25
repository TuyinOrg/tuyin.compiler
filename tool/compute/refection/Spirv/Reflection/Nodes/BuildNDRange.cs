using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class BuildNDRange : Node
    {
        public BuildNDRange()
        {
        }

        public BuildNDRange(SpirvTypeBase resultType, Node globalWorkSize, Node localWorkSize, Node globalWorkOffset, string debugName = null)
        {
            this.ResultType = resultType;
            this.GlobalWorkSize = globalWorkSize;
            this.LocalWorkSize = localWorkSize;
            this.GlobalWorkOffset = globalWorkOffset;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpBuildNDRange;

        public Node GlobalWorkSize { get; set; }

        public Node LocalWorkSize { get; set; }

        public Node GlobalWorkOffset { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return GlobalWorkSize;
                yield return LocalWorkSize;
                yield return GlobalWorkOffset;
        }

        public BuildNDRange WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpBuildNDRange)op, treeBuilder);
        }

        public BuildNDRange SetUp(Action<BuildNDRange> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpBuildNDRange op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            GlobalWorkSize = treeBuilder.GetNode(op.GlobalWorkSize);
            LocalWorkSize = treeBuilder.GetNode(op.LocalWorkSize);
            GlobalWorkOffset = treeBuilder.GetNode(op.GlobalWorkOffset);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the BuildNDRange object.</summary>
        /// <returns>A string that represents the BuildNDRange object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"BuildNDRange({ResultType}, {GlobalWorkSize}, {LocalWorkSize}, {GlobalWorkOffset}, {DebugName})";
        }
    }
}