using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL : Node
    {
        public SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL()
        {
        }

        public SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL SetUp(Action<SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceGetDefaultLowPenaltyCostTableINTEL({ResultType}, {DebugName})";
        }
    }
}