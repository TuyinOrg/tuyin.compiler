using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeAvcRefResultINTEL : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeAvcRefResultINTEL;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.AvcRefResultINTEL;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeAvcRefResultINTEL)op, treeBuilder);
        }


        public void SetUp(OpTypeAvcRefResultINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}