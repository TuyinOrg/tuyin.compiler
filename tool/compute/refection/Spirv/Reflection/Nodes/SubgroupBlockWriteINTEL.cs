using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupBlockWriteINTEL : ExecutableNode, INodeWithNext
    {
        public SubgroupBlockWriteINTEL()
        {
        }

        public SubgroupBlockWriteINTEL(Node ptr, Node data, string debugName = null)
        {
            this.Ptr = ptr;
            this.Data = data;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupBlockWriteINTEL;

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

        public Node Ptr { get; set; }

        public Node Data { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Ptr;
                yield return Data;
        }

        public SubgroupBlockWriteINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupBlockWriteINTEL)op, treeBuilder);
        }

        public SubgroupBlockWriteINTEL SetUp(Action<SubgroupBlockWriteINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupBlockWriteINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Ptr = treeBuilder.GetNode(op.Ptr);
            Data = treeBuilder.GetNode(op.Data);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupBlockWriteINTEL object.</summary>
        /// <returns>A string that represents the SubgroupBlockWriteINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupBlockWriteINTEL({Ptr}, {Data}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static SubgroupBlockWriteINTEL ThenSubgroupBlockWriteINTEL(this INodeWithNext node, Node ptr, Node data, string debugName = null)
        {
            return node.Then(new SubgroupBlockWriteINTEL(ptr, data, debugName));
        }
    }
}