using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class DemoteToHelperInvocationEXT : ExecutableNode, INodeWithNext
    {
        public DemoteToHelperInvocationEXT()
        {
        }

        public DemoteToHelperInvocationEXT(string debugName = null)
        {
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpDemoteToHelperInvocationEXT;

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

        public DemoteToHelperInvocationEXT WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpDemoteToHelperInvocationEXT)op, treeBuilder);
        }

        public DemoteToHelperInvocationEXT SetUp(Action<DemoteToHelperInvocationEXT> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpDemoteToHelperInvocationEXT op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the DemoteToHelperInvocationEXT object.</summary>
        /// <returns>A string that represents the DemoteToHelperInvocationEXT object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"DemoteToHelperInvocationEXT({DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static DemoteToHelperInvocationEXT ThenDemoteToHelperInvocationEXT(this INodeWithNext node, string debugName = null)
        {
            return node.Then(new DemoteToHelperInvocationEXT(debugName));
        }
    }
}