using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL : Node
    {
        public SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL()
        {
        }

        public SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL SetUp(Action<SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceGetDefaultNonDcLumaIntraPenaltyINTEL({ResultType}, {DebugName})";
        }
    }
}