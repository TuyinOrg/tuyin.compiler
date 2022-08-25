using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeAvcImeResultDualReferenceStreamoutINTEL : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeAvcImeResultDualReferenceStreamoutINTEL;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.AvcImeResultDualReferenceStreamoutINTEL;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeAvcImeResultDualReferenceStreamoutINTEL)op, treeBuilder);
        }


        public void SetUp(OpTypeAvcImeResultDualReferenceStreamoutINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}