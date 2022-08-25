using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL : Node
    {
        public SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL()
        {
        }

        public SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL SetUp(Action<SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceGetDefaultIntraChromaModeBasePenaltyINTEL({ResultType}, {DebugName})";
        }
    }
}