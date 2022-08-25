using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class AtomicStore : ExecutableNode, INodeWithNext
    {
        public AtomicStore()
        {
        }

        public AtomicStore(Node pointer, uint memory, uint semantics, Node value, string debugName = null)
        {
            this.Pointer = pointer;
            this.Memory = memory;
            this.Semantics = semantics;
            this.Value = value;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpAtomicStore;

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

        public Node Value { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pointer;
                yield return Value;
        }

        public AtomicStore WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpAtomicStore)op, treeBuilder);
        }

        public AtomicStore SetUp(Action<AtomicStore> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpAtomicStore op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Pointer = treeBuilder.GetNode(op.Pointer);
            Memory = op.Memory;
            Semantics = op.Semantics;
            Value = treeBuilder.GetNode(op.Value);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the AtomicStore object.</summary>
        /// <returns>A string that represents the AtomicStore object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"AtomicStore({Pointer}, {Memory}, {Semantics}, {Value}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static AtomicStore ThenAtomicStore(this INodeWithNext node, Node pointer, uint memory, uint semantics, Node value, string debugName = null)
        {
            return node.Then(new AtomicStore(pointer, memory, semantics, value, debugName));
        }
    }
}