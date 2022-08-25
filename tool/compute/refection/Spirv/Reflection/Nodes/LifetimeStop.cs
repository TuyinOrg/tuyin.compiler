using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class LifetimeStop : ExecutableNode, INodeWithNext
    {
        public LifetimeStop()
        {
        }

        public LifetimeStop(Node pointer, uint size, string debugName = null)
        {
            this.Pointer = pointer;
            this.Size = size;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpLifetimeStop;

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

        public uint Size { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pointer;
        }

        public LifetimeStop WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpLifetimeStop)op, treeBuilder);
        }

        public LifetimeStop SetUp(Action<LifetimeStop> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpLifetimeStop op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Pointer = treeBuilder.GetNode(op.Pointer);
            Size = op.Size;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the LifetimeStop object.</summary>
        /// <returns>A string that represents the LifetimeStop object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"LifetimeStop({Pointer}, {Size}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static LifetimeStop ThenLifetimeStop(this INodeWithNext node, Node pointer, uint size, string debugName = null)
        {
            return node.Then(new LifetimeStop(pointer, size, debugName));
        }
    }
}