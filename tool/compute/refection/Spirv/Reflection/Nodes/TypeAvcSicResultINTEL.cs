using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeAvcSicResultINTEL : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeAvcSicResultINTEL;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.AvcSicResultINTEL;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeAvcSicResultINTEL)op, treeBuilder);
        }


        public void SetUp(OpTypeAvcSicResultINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}