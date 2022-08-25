using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class AtomicIAdd : Node
    {
        public AtomicIAdd()
        {
        }

        public AtomicIAdd(SpirvTypeBase resultType, Node pointer, uint memory, uint semantics, Node value, string debugName = null)
        {
            this.ResultType = resultType;
            this.Pointer = pointer;
            this.Memory = memory;
            this.Semantics = semantics;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpAtomicIAdd;

        public Node Pointer { get; set; }

        public uint Memory { get; set; }

        public uint Semantics { get; set; }

        public Node Value { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pointer;
                yield return Value;
        }

        public AtomicIAdd WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpAtomicIAdd)op, treeBuilder);
        }

        public AtomicIAdd SetUp(Action<AtomicIAdd> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpAtomicIAdd op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Pointer = treeBuilder.GetNode(op.Pointer);
            Memory = op.Memory;
            Semantics = op.Semantics;
            Value = treeBuilder.GetNode(op.Value);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the AtomicIAdd object.</summary>
        /// <returns>A string that represents the AtomicIAdd object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"AtomicIAdd({ResultType}, {Pointer}, {Memory}, {Semantics}, {Value}, {DebugName})";
        }
    }
}