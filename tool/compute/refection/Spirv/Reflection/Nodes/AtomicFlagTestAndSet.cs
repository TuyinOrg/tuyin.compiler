using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class AtomicFlagTestAndSet : Node
    {
        public AtomicFlagTestAndSet()
        {
        }

        public AtomicFlagTestAndSet(SpirvTypeBase resultType, Node pointer, uint memory, uint semantics, string debugName = null)
        {
            this.ResultType = resultType;
            this.Pointer = pointer;
            this.Memory = memory;
            this.Semantics = semantics;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpAtomicFlagTestAndSet;

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

        public AtomicFlagTestAndSet WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpAtomicFlagTestAndSet)op, treeBuilder);
        }

        public AtomicFlagTestAndSet SetUp(Action<AtomicFlagTestAndSet> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpAtomicFlagTestAndSet op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Pointer = treeBuilder.GetNode(op.Pointer);
            Memory = op.Memory;
            Semantics = op.Semantics;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the AtomicFlagTestAndSet object.</summary>
        /// <returns>A string that represents the AtomicFlagTestAndSet object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"AtomicFlagTestAndSet({ResultType}, {Pointer}, {Memory}, {Semantics}, {DebugName})";
        }
    }
}