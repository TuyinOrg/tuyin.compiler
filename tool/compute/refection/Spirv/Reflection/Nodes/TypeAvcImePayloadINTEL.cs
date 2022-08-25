using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeAvcImePayloadINTEL : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeAvcImePayloadINTEL;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.AvcImePayloadINTEL;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeAvcImePayloadINTEL)op, treeBuilder);
        }


        public void SetUp(OpTypeAvcImePayloadINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}