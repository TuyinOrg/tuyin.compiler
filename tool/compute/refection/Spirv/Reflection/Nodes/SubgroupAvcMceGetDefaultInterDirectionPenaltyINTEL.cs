using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL : Node
    {
        public SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL()
        {
        }

        public SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL(SpirvTypeBase resultType, Node sliceType, Node qp, string debugName = null)
        {
            this.ResultType = resultType;
            this.SliceType = sliceType;
            this.Qp = qp;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL;

        public Node SliceType { get; set; }

        public Node Qp { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SliceType;
                yield return Qp;
        }

        public SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL SetUp(Action<SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SliceType = treeBuilder.GetNode(op.SliceType);
            Qp = treeBuilder.GetNode(op.Qp);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceGetDefaultInterDirectionPenaltyINTEL({ResultType}, {SliceType}, {Qp}, {DebugName})";
        }
    }
}