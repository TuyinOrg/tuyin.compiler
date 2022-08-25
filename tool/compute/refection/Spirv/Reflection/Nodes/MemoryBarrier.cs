using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class MemoryBarrier : ExecutableNode, INodeWithNext
    {
        public MemoryBarrier()
        {
        }

        public MemoryBarrier(uint memory, uint semantics, string debugName = null)
        {
            this.Memory = memory;
            this.Semantics = semantics;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpMemoryBarrier;

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

        public uint Memory { get; set; }

        public uint Semantics { get; set; }

        public MemoryBarrier WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpMemoryBarrier)op, treeBuilder);
        }

        public MemoryBarrier SetUp(Action<MemoryBarrier> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpMemoryBarrier op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Memory = op.Memory;
            Semantics = op.Semantics;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the MemoryBarrier object.</summary>
        /// <returns>A string that represents the MemoryBarrier object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"MemoryBarrier({Memory}, {Semantics}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static MemoryBarrier ThenMemoryBarrier(this INodeWithNext node, uint memory, uint semantics, string debugName = null)
        {
            return node.Then(new MemoryBarrier(memory, semantics, debugName));
        }
    }
}