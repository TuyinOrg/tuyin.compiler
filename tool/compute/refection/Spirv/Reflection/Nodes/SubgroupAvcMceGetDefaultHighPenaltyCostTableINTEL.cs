using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL : Node
    {
        public SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL()
        {
        }

        public SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL SetUp(Action<SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceGetDefaultHighPenaltyCostTableINTEL({ResultType}, {DebugName})";
        }
    }
}