using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class BitReverse : Node
    {
        public BitReverse()
        {
        }

        public BitReverse(SpirvTypeBase resultType, Node @base, string debugName = null)
        {
            this.ResultType = resultType;
            this.Base = @base;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpBitReverse;

        public Node Base { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Base;
        }

        public BitReverse WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpBitReverse)op, treeBuilder);
        }

        public BitReverse SetUp(Action<BitReverse> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpBitReverse op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Base = treeBuilder.GetNode(op.Base);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the BitReverse object.</summary>
        /// <returns>A string that represents the BitReverse object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"BitReverse({ResultType}, {Base}, {DebugName})";
        }
    }
}