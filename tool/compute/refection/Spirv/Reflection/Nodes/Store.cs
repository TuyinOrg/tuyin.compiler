using System;
using System.Collections.Generic;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Store : ExecutableNode, INodeWithNext
    {
        public Store()
        {
        }

        public Store(Node pointer, Node @object, Spv.MemoryAccess memoryAccess, string debugName = null)
        {
            this.Pointer = pointer;
            this.Object = @object;
            this.MemoryAccess = memoryAccess;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpStore;

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

        public Node Object { get; set; }

        public Spv.MemoryAccess MemoryAccess { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pointer;
                yield return Object;
        }

        public Store WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpStore)op, treeBuilder);
        }

        public Store SetUp(Action<Store> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpStore op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Pointer = treeBuilder.GetNode(op.Pointer);
            Object = treeBuilder.GetNode(op.Object);
            MemoryAccess = op.MemoryAccess;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Store object.</summary>
        /// <returns>A string that represents the Store object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Store({Pointer}, {Object}, {MemoryAccess}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static Store ThenStore(this INodeWithNext node, Node pointer, Node @object, Spv.MemoryAccess memoryAccess, string debugName = null)
        {
            return node.Then(new Store(pointer, @object, memoryAccess, debugName));
        }
    }
}