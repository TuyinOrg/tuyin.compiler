using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class RayQueryGetIntersectionPrimitiveIndexKHR : Node
    {
        public RayQueryGetIntersectionPrimitiveIndexKHR()
        {
        }

        public RayQueryGetIntersectionPrimitiveIndexKHR(SpirvTypeBase resultType, Node rayQuery, Node intersection, string debugName = null)
        {
            this.ResultType = resultType;
            this.RayQuery = rayQuery;
            this.Intersection = intersection;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpRayQueryGetIntersectionPrimitiveIndexKHR;

        public Node RayQuery { get; set; }

        public Node Intersection { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return RayQuery;
                yield return Intersection;
        }

        public RayQueryGetIntersectionPrimitiveIndexKHR WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpRayQueryGetIntersectionPrimitiveIndexKHR)op, treeBuilder);
        }

        public RayQueryGetIntersectionPrimitiveIndexKHR SetUp(Action<RayQueryGetIntersectionPrimitiveIndexKHR> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpRayQueryGetIntersectionPrimitiveIndexKHR op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            RayQuery = treeBuilder.GetNode(op.RayQuery);
            Intersection = treeBuilder.GetNode(op.Intersection);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the RayQueryGetIntersectionPrimitiveIndexKHR object.</summary>
        /// <returns>A string that represents the RayQueryGetIntersectionPrimitiveIndexKHR object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"RayQueryGetIntersectionPrimitiveIndexKHR({ResultType}, {RayQuery}, {Intersection}, {DebugName})";
        }
    }
}