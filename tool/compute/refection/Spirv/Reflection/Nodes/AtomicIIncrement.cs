using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class AtomicIIncrement : Node
    {
        public AtomicIIncrement()
        {
        }

        public AtomicIIncrement(SpirvTypeBase resultType, Node pointer, uint memory, uint semantics, string debugName = null)
        {
            this.ResultType = resultType;
            this.Pointer = pointer;
            this.Memory = memory;
            this.Semantics = semantics;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpAtomicIIncrement;

        public Node Pointer { get; set; }

        public uint Memory { get; set; }

        public uint Semantics { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pointer;
        }

        public AtomicIIncrement WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpAtomicIIncrement)op, treeBuilder);
        }

        public AtomicIIncrement SetUp(Action<AtomicIIncrement> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpAtomicIIncrement op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Pointer = treeBuilder.GetNode(op.Pointer);
            Memory = op.Memory;
            Semantics = op.Semantics;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the AtomicIIncrement object.</summary>
        /// <returns>A string that represents the AtomicIIncrement object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"AtomicIIncrement({ResultType}, {Pointer}, {Memory}, {Semantics}, {DebugName})";
        }
    }
}