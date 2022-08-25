using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class AtomicFlagClear : ExecutableNode, INodeWithNext
    {
        public AtomicFlagClear()
        {
        }

        public AtomicFlagClear(Node pointer, uint memory, uint semantics, string debugName = null)
        {
            this.Pointer = pointer;
            this.Memory = memory;
            this.Semantics = semantics;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpAtomicFlagClear;

        /// <summary>
        /// Next operation in sequence
        /// </summary>
        public ExecutableNode Next { get; set; }

        public override ExecutableNode GetNext()
        {
            return Next;
        }

        public T Then<T>(T node) where T: ExecutableNode
        {
            Next = node;
            return node;
        }

        public Node Pointer { get; set; }

        public uint Memory { get; set; }

        public uint Semantics { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pointer;
        }

        public AtomicFlagClear WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpAtomicFlagClear)op, treeBuilder);
        }

        public AtomicFlagClear SetUp(Action<AtomicFlagClear> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpAtomicFlagClear op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Pointer = treeBuilder.GetNode(op.Pointer);
            Memory = op.Memory;
            Semantics = op.Semantics;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the AtomicFlagClear object.</summary>
        /// <returns>A string that represents the AtomicFlagClear object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"AtomicFlagClear({Pointer}, {Memory}, {Semantics}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static AtomicFlagClear ThenAtomicFlagClear(this INodeWithNext node, Node pointer, uint memory, uint semantics, string debugName = null)
        {
            return node.Then(new AtomicFlagClear(pointer, memory, semantics, debugName));
        }
    }
}