using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class BitFieldInsert : Node
    {
        public BitFieldInsert()
        {
        }

        public BitFieldInsert(SpirvTypeBase resultType, Node @base, Node insert, Node offset, Node count, string debugName = null)
        {
            this.ResultType = resultType;
            this.Base = @base;
            this.Insert = insert;
            this.Offset = offset;
            this.Count = count;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpBitFieldInsert;

        public Node Base { get; set; }

        public Node Insert { get; set; }

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
                yield return Insert;
                yield return Offset;
                yield return Count;
        }

        public BitFieldInsert WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpBitFieldInsert)op, treeBuilder);
        }

        public BitFieldInsert SetUp(Action<BitFieldInsert> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpBitFieldInsert op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Base = treeBuilder.GetNode(op.Base);
            Insert = treeBuilder.GetNode(op.Insert);
            Offset = treeBuilder.GetNode(op.Offset);
            Count = treeBuilder.GetNode(op.Count);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the BitFieldInsert object.</summary>
        /// <returns>A string that represents the BitFieldInsert object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"BitFieldInsert({ResultType}, {Base}, {Insert}, {Offset}, {Count}, {DebugName})";
        }
    }
}