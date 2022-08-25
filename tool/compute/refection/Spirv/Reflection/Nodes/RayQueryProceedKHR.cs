using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class RayQueryProceedKHR : Node
    {
        public RayQueryProceedKHR()
        {
        }

        public RayQueryProceedKHR(SpirvTypeBase resultType, Node rayQuery, string debugName = null)
        {
            this.ResultType = resultType;
            this.RayQuery = rayQuery;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpRayQueryProceedKHR;

        public Node RayQuery { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
            yield return RayQuery;
        }

        public RayQueryProceedKHR WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpRayQueryProceedKHR)op, treeBuilder);
        }

        public RayQueryProceedKHR SetUp(Action<RayQueryProceedKHR> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpRayQueryProceedKHR op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            RayQuery = treeBuilder.GetNode(op.RayQuery);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the RayQueryProceedKHR object.</summary>
        /// <returns>A string that represents the RayQueryProceedKHR object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"RayQueryProceedKHR({ResultType}, {RayQuery}, {DebugName})";
        }
    }
}