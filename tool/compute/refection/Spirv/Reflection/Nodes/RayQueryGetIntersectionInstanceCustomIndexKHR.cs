using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class RayQueryGetIntersectionInstanceCustomIndexKHR : Node
    {
        public RayQueryGetIntersectionInstanceCustomIndexKHR()
        {
        }

        public RayQueryGetIntersectionInstanceCustomIndexKHR(SpirvTypeBase resultType, Node rayQuery, Node intersection, string debugName = null)
        {
            this.ResultType = resultType;
            this.RayQuery = rayQuery;
            this.Intersection = intersection;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpRayQueryGetIntersectionInstanceCustomIndexKHR;

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

        public RayQueryGetIntersectionInstanceCustomIndexKHR WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpRayQueryGetIntersectionInstanceCustomIndexKHR)op, treeBuilder);
        }

        public RayQueryGetIntersectionInstanceCustomIndexKHR SetUp(Action<RayQueryGetIntersectionInstanceCustomIndexKHR> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpRayQueryGetIntersectionInstanceCustomIndexKHR op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            RayQuery = treeBuilder.GetNode(op.RayQuery);
            Intersection = treeBuilder.GetNode(op.Intersection);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the RayQueryGetIntersectionInstanceCustomIndexKHR object.</summary>
        /// <returns>A string that represents the RayQueryGetIntersectionInstanceCustomIndexKHR object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"RayQueryGetIntersectionInstanceCustomIndexKHR({ResultType}, {RayQuery}, {Intersection}, {DebugName})";
        }
    }
}