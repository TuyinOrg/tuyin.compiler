using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class BitFieldUExtract : Node
    {
        public BitFieldUExtract()
        {
        }

        public BitFieldUExtract(SpirvTypeBase resultType, Node @base, Node offset, Node count, string debugName = null)
        {
            this.ResultType = resultType;
            this.Base = @base;
            this.Offset = offset;
            this.Count = count;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpBitFieldUExtract;

        public Node Base { get; set; }

        public Node Offset { get; set; }

        public Node Count { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Base;
                yield return Offset;
                yield return Count;
        }

        public BitFieldUExtract WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpBitFieldUExtract)op, treeBuilder);
        }

        public BitFieldUExtract SetUp(Action<BitFieldUExtract> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpBitFieldUExtract op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Base = treeBuilder.GetNode(op.Base);
            Offset = treeBuilder.GetNode(op.Offset);
            Count = treeBuilder.GetNode(op.Count);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the BitFieldUExtract object.</summary>
        /// <returns>A string that represents the BitFieldUExtract object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"BitFieldUExtract({ResultType}, {Base}, {Offset}, {Count}, {DebugName})";
        }
    }
}