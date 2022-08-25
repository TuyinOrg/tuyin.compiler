using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class MemoryNamedBarrier : ExecutableNode, INodeWithNext
    {
        public MemoryNamedBarrier()
        {
        }

        public MemoryNamedBarrier(Node namedBarrier, uint memory, uint semantics, string debugName = null)
        {
            this.NamedBarrier = namedBarrier;
            this.Memory = memory;
            this.Semantics = semantics;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpMemoryNamedBarrier;

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

        public Node NamedBarrier { get; set; }

        public uint Memory { get; set; }

        public uint Semantics { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return NamedBarrier;
        }

        public MemoryNamedBarrier WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpMemoryNamedBarrier)op, treeBuilder);
        }

        public MemoryNamedBarrier SetUp(Action<MemoryNamedBarrier> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpMemoryNamedBarrier op, SpirvInstructionTreeBuilder treeBuilder)
        {
            NamedBarrier = treeBuilder.GetNode(op.NamedBarrier);
            Memory = op.Memory;
            Semantics = op.Semantics;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the MemoryNamedBarrier object.</summary>
        /// <returns>A string that represents the MemoryNamedBarrier object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"MemoryNamedBarrier({NamedBarrier}, {Memory}, {Semantics}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static MemoryNamedBarrier ThenMemoryNamedBarrier(this INodeWithNext node, Node namedBarrier, uint memory, uint semantics, string debugName = null)
        {
            return node.Then(new MemoryNamedBarrier(namedBarrier, memory, semantics, debugName));
        }
    }
}