using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class RayQueryGetRayTMinKHR : Node
    {
        public RayQueryGetRayTMinKHR()
        {
        }

        public RayQueryGetRayTMinKHR(SpirvTypeBase resultType, Node rayQuery, string debugName = null)
        {
            this.ResultType = resultType;
            this.RayQuery = rayQuery;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpRayQueryGetRayTMinKHR;

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

        public RayQueryGetRayTMinKHR WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpRayQueryGetRayTMinKHR)op, treeBuilder);
        }

        public RayQueryGetRayTMinKHR SetUp(Action<RayQueryGetRayTMinKHR> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpRayQueryGetRayTMinKHR op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            RayQuery = treeBuilder.GetNode(op.RayQuery);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the RayQueryGetRayTMinKHR object.</summary>
        /// <returns>A string that represents the RayQueryGetRayTMinKHR object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"RayQueryGetRayTMinKHR({ResultType}, {RayQuery}, {DebugName})";
        }
    }
}