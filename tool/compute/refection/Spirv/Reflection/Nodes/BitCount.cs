using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class BitCount : Node
    {
        public BitCount()
        {
        }

        public BitCount(SpirvTypeBase resultType, Node @base, string debugName = null)
        {
            this.ResultType = resultType;
            this.Base = @base;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpBitCount;

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

        public BitCount WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpBitCount)op, treeBuilder);
        }

        public BitCount SetUp(Action<BitCount> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpBitCount op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Base = treeBuilder.GetNode(op.Base);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the BitCount object.</summary>
        /// <returns>A string that represents the BitCount object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"BitCount({ResultType}, {Base}, {DebugName})";
        }
    }
}