using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeAvcImeDualReferenceStreaminINTEL : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeAvcImeDualReferenceStreaminINTEL;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.AvcImeDualReferenceStreaminINTEL;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeAvcImeDualReferenceStreaminINTEL)op, treeBuilder);
        }


        public void SetUp(OpTypeAvcImeDualReferenceStreaminINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}