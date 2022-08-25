using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class AtomicCompareExchangeWeak : Node
    {
        public AtomicCompareExchangeWeak()
        {
        }

        public AtomicCompareExchangeWeak(SpirvTypeBase resultType, Node pointer, uint memory, uint equal, uint unequal, Node value, Node comparator, string debugName = null)
        {
            this.ResultType = resultType;
            this.Pointer = pointer;
            this.Memory = memory;
            this.Equal = equal;
            this.Unequal = unequal;
            this.Value = value;
            this.Comparator = comparator;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpAtomicCompareExchangeWeak;

        public Node Pointer { get; set; }

        public uint Memory { get; set; }

        public uint Equal { get; set; }

        public uint Unequal { get; set; }

        public Node Value { get; set; }

        public Node Comparator { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pointer;
                yield return Value;
                yield return Comparator;
        }

        public AtomicCompareExchangeWeak WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpAtomicCompareExchangeWeak)op, treeBuilder);
        }

        public AtomicCompareExchangeWeak SetUp(Action<AtomicCompareExchangeWeak> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpAtomicCompareExchangeWeak op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Pointer = treeBuilder.GetNode(op.Pointer);
            Memory = op.Memory;
            Equal = op.Equal;
            Unequal = op.Unequal;
            Value = treeBuilder.GetNode(op.Value);
            Comparator = treeBuilder.GetNode(op.Comparator);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the AtomicCompareExchangeWeak object.</summary>
        /// <returns>A string that represents the AtomicCompareExchangeWeak object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"AtomicCompareExchangeWeak({ResultType}, {Pointer}, {Memory}, {Equal}, {Unequal}, {Value}, {Comparator}, {DebugName})";
        }
    }
}