using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeAvcImeResultSingleReferenceStreamoutINTEL : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeAvcImeResultSingleReferenceStreamoutINTEL;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.AvcImeResultSingleReferenceStreamoutINTEL;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeAvcImeResultSingleReferenceStreamoutINTEL)op, treeBuilder);
        }


        public void SetUp(OpTypeAvcImeResultSingleReferenceStreamoutINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}