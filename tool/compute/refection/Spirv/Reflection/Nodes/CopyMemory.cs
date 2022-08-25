using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CopyMemory : ExecutableNode, INodeWithNext
    {
        public CopyMemory()
        {
        }

        public CopyMemory(Node target, Node source, Spv.MemoryAccess memoryAccess, Spv.MemoryAccess memoryAccess2, string debugName = null)
        {
            this.Target = target;
            this.Source = source;
            this.MemoryAccess = memoryAccess;
            this.MemoryAccess2 = memoryAccess2;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCopyMemory;

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

        public Node Target { get; set; }

        public Node Source { get; set; }

        public Spv.MemoryAccess MemoryAccess { get; set; }

        public Spv.MemoryAccess MemoryAccess2 { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Target;
                yield return Source;
        }

        public CopyMemory WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCopyMemory)op, treeBuilder);
        }

        public CopyMemory SetUp(Action<CopyMemory> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCopyMemory op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Target = treeBuilder.GetNode(op.Target);
            Source = treeBuilder.GetNode(op.Source);
            MemoryAccess = op.MemoryAccess;
            MemoryAccess2 = op.MemoryAccess2;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CopyMemory object.</summary>
        /// <returns>A string that represents the CopyMemory object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CopyMemory({Target}, {Source}, {MemoryAccess}, {MemoryAccess2}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static CopyMemory ThenCopyMemory(this INodeWithNext node, Node target, Node source, Spv.MemoryAccess memoryAccess, Spv.MemoryAccess memoryAccess2, string debugName = null)
        {
            return node.Then(new CopyMemory(target, source, memoryAccess, memoryAccess2, debugName));
        }
    }
}