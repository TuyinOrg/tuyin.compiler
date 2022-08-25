using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ControlBarrier : ExecutableNode, INodeWithNext
    {
        public ControlBarrier()
        {
        }

        public ControlBarrier(uint execution, uint memory, uint semantics, string debugName = null)
        {
            this.Execution = execution;
            this.Memory = memory;
            this.Semantics = semantics;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpControlBarrier;

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

        public uint Execution { get; set; }

        public uint Memory { get; set; }

        public uint Semantics { get; set; }

        public ControlBarrier WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpControlBarrier)op, treeBuilder);
        }

        public ControlBarrier SetUp(Action<ControlBarrier> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpControlBarrier op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Execution = op.Execution;
            Memory = op.Memory;
            Semantics = op.Semantics;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ControlBarrier object.</summary>
        /// <returns>A string that represents the ControlBarrier object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ControlBarrier({Execution}, {Memory}, {Semantics}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static ControlBarrier ThenControlBarrier(this INodeWithNext node, uint execution, uint memory, uint semantics, string debugName = null)
        {
            return node.Then(new ControlBarrier(execution, memory, semantics, debugName));
        }
    }
}