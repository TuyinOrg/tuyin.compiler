using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceSetMotionVectorCostFunctionINTEL : Node
    {
        public SubgroupAvcMceSetMotionVectorCostFunctionINTEL()
        {
        }

        public SubgroupAvcMceSetMotionVectorCostFunctionINTEL(SpirvTypeBase resultType, Node packedCostCenterDelta, Node packedCostTable, Node costPrecision, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.PackedCostCenterDelta = packedCostCenterDelta;
            this.PackedCostTable = packedCostTable;
            this.CostPrecision = costPrecision;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceSetMotionVectorCostFunctionINTEL;

        public Node PackedCostCenterDelta { get; set; }

        public Node PackedCostTable { get; set; }

        public Node CostPrecision { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return PackedCostCenterDelta;
                yield return PackedCostTable;
                yield return CostPrecision;
                yield return Payload;
        }

        public SubgroupAvcMceSetMotionVectorCostFunctionINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceSetMotionVectorCostFunctionINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceSetMotionVectorCostFunctionINTEL SetUp(Action<SubgroupAvcMceSetMotionVectorCostFunctionINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceSetMotionVectorCostFunctionINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            PackedCostCenterDelta = treeBuilder.GetNode(op.PackedCostCenterDelta);
            PackedCostTable = treeBuilder.GetNode(op.PackedCostTable);
            CostPrecision = treeBuilder.GetNode(op.CostPrecision);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceSetMotionVectorCostFunctionINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceSetMotionVectorCostFunctionINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceSetMotionVectorCostFunctionINTEL({ResultType}, {PackedCostCenterDelta}, {PackedCostTable}, {CostPrecision}, {Payload}, {DebugName})";
        }
    }
}