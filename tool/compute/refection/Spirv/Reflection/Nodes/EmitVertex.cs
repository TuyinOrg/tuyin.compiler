using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class EmitVertex : ExecutableNode, INodeWithNext
    {
        public EmitVertex()
        {
        }

        public EmitVertex(string debugName = null)
        {
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpEmitVertex;

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

        public EmitVertex WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpEmitVertex)op, treeBuilder);
        }

        public EmitVertex SetUp(Action<EmitVertex> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpEmitVertex op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the EmitVertex object.</summary>
        /// <returns>A string that represents the EmitVertex object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"EmitVertex({DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static EmitVertex ThenEmitVertex(this INodeWithNext node, string debugName = null)
        {
            return node.Then(new EmitVertex(debugName));
        }
    }
}