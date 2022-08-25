using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeAvcMceResultINTEL : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeAvcMceResultINTEL;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.AvcMceResultINTEL;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeAvcMceResultINTEL)op, treeBuilder);
        }


        public void SetUp(OpTypeAvcMceResultINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}