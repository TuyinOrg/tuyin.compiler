using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class EndStreamPrimitive : ExecutableNode, INodeWithNext
    {
        public EndStreamPrimitive()
        {
        }

        public EndStreamPrimitive(Node stream, string debugName = null)
        {
            this.Stream = stream;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpEndStreamPrimitive;

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

        public Node Stream { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Stream;
        }

        public EndStreamPrimitive WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpEndStreamPrimitive)op, treeBuilder);
        }

        public EndStreamPrimitive SetUp(Action<EndStreamPrimitive> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpEndStreamPrimitive op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Stream = treeBuilder.GetNode(op.Stream);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the EndStreamPrimitive object.</summary>
        /// <returns>A string that represents the EndStreamPrimitive object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"EndStreamPrimitive({Stream}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static EndStreamPrimitive ThenEndStreamPrimitive(this INodeWithNext node, Node stream, string debugName = null)
        {
            return node.Then(new EndStreamPrimitive(stream, debugName));
        }
    }
}