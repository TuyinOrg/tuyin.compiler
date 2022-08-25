using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ExecutionModeId : ExecutableNode, INodeWithNext
    {
        public ExecutionModeId()
        {
        }

        public ExecutionModeId(Node entryPoint, Spv.ExecutionMode mode, string debugName = null)
        {
            this.EntryPoint = entryPoint;
            this.Mode = mode;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpExecutionModeId;

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

        public Node EntryPoint { get; set; }

        public Spv.ExecutionMode Mode { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return EntryPoint;
        }

        public ExecutionModeId WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpExecutionModeId)op, treeBuilder);
        }

        public ExecutionModeId SetUp(Action<ExecutionModeId> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpExecutionModeId op, SpirvInstructionTreeBuilder treeBuilder)
        {
            EntryPoint = treeBuilder.GetNode(op.EntryPoint);
            Mode = op.Mode;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ExecutionModeId object.</summary>
        /// <returns>A string that represents the ExecutionModeId object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ExecutionModeId({EntryPoint}, {Mode}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static ExecutionModeId ThenExecutionModeId(this INodeWithNext node, Node entryPoint, Spv.ExecutionMode mode, string debugName = null)
        {
            return node.Then(new ExecutionModeId(entryPoint, mode, debugName));
        }
    }
}