using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class RayQueryGenerateIntersectionKHR : ExecutableNode, INodeWithNext
    {
        public RayQueryGenerateIntersectionKHR()
        {
        }

        public RayQueryGenerateIntersectionKHR(Node rayQuery, Node hitT, string debugName = null)
        {
            this.RayQuery = rayQuery;
            this.HitT = hitT;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpRayQueryGenerateIntersectionKHR;

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

        public Node HitT { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return RayQuery;
                yield return HitT;
        }

        public RayQueryGenerateIntersectionKHR WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpRayQueryGenerateIntersectionKHR)op, treeBuilder);
        }

        public RayQueryGenerateIntersectionKHR SetUp(Action<RayQueryGenerateIntersectionKHR> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpRayQueryGenerateIntersectionKHR op, SpirvInstructionTreeBuilder treeBuilder)
        {
            RayQuery = treeBuilder.GetNode(op.RayQuery);
            HitT = treeBuilder.GetNode(op.HitT);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the RayQueryGenerateIntersectionKHR object.</summary>
        /// <returns>A string that represents the RayQueryGenerateIntersectionKHR object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"RayQueryGenerateIntersectionKHR({RayQuery}, {HitT}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static RayQueryGenerateIntersectionKHR ThenRayQueryGenerateIntersectionKHR(this INodeWithNext node, Node rayQuery, Node hitT, string debugName = null)
        {
            return node.Then(new RayQueryGenerateIntersectionKHR(rayQuery, hitT, debugName));
        }
    }
}