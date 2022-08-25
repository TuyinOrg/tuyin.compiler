using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL : Node
    {
        public SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL()
        {
        }

        public SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL(SpirvTypeBase resultType, string debugName = null)
        {
            this.ResultType = resultType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL;

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL SetUp(Action<SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceGetDefaultMediumPenaltyCostTableINTEL({ResultType}, {DebugName})";
        }
    }
}