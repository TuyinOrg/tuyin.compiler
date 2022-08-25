using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ExecuteCallableNV : ExecutableNode, INodeWithNext
    {
        public ExecuteCallableNV()
        {
        }

        public ExecuteCallableNV(Node sBTIndex, Node callableDataId, string debugName = null)
        {
            this.SBTIndex = sBTIndex;
            this.CallableDataId = callableDataId;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpExecuteCallableNV;

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

        public Node SBTIndex { get; set; }

        public Node CallableDataId { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SBTIndex;
                yield return CallableDataId;
        }

        public ExecuteCallableNV WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpExecuteCallableNV)op, treeBuilder);
        }

        public ExecuteCallableNV SetUp(Action<ExecuteCallableNV> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpExecuteCallableNV op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SBTIndex = treeBuilder.GetNode(op.SBTIndex);
            CallableDataId = treeBuilder.GetNode(op.CallableDataId);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ExecuteCallableNV object.</summary>
        /// <returns>A string that represents the ExecuteCallableNV object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ExecuteCallableNV({SBTIndex}, {CallableDataId}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static ExecuteCallableNV ThenExecuteCallableNV(this INodeWithNext node, Node sBTIndex, Node callableDataId, string debugName = null)
        {
            return node.Then(new ExecuteCallableNV(sBTIndex, callableDataId, debugName));
        }
    }
}