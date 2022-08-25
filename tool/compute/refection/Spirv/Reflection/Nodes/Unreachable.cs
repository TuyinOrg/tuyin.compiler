using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Unreachable : ExecutableNode
    {
        public Unreachable()
        {
        }

        public Unreachable(string debugName = null)
        {
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpUnreachable;

        public Unreachable WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpUnreachable)op, treeBuilder);
        }

        public Unreachable SetUp(Action<Unreachable> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpUnreachable op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Unreachable object.</summary>
        /// <returns>A string that represents the Unreachable object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Unreachable({DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static Unreachable ThenUnreachable(this INodeWithNext node, string debugName = null)
        {
            return node.Then(new Unreachable(debugName));
        }
    }
}