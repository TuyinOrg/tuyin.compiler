using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class RayQueryTerminateKHR : ExecutableNode, INodeWithNext
    {
        public RayQueryTerminateKHR()
        {
        }

        public RayQueryTerminateKHR(Node rayQuery, string debugName = null)
        {
            this.RayQuery = rayQuery;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpRayQueryTerminateKHR;

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

        public Node RayQuery { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return RayQuery;
        }

        public RayQueryTerminateKHR WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpRayQueryTerminateKHR)op, treeBuilder);
        }

        public RayQueryTerminateKHR SetUp(Action<RayQueryTerminateKHR> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpRayQueryTerminateKHR op, SpirvInstructionTreeBuilder treeBuilder)
        {
            RayQuery = treeBuilder.GetNode(op.RayQuery);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the RayQueryTerminateKHR object.</summary>
        /// <returns>A string that represents the RayQueryTerminateKHR object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"RayQueryTerminateKHR({RayQuery}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static RayQueryTerminateKHR ThenRayQueryTerminateKHR(this INodeWithNext node, Node rayQuery, string debugName = null)
        {
            return node.Then(new RayQueryTerminateKHR(rayQuery, debugName));
        }
    }
}