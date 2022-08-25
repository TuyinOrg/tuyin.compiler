using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeAvcSicPayloadINTEL : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeAvcSicPayloadINTEL;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.AvcSicPayloadINTEL;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeAvcSicPayloadINTEL)op, treeBuilder);
        }


        public void SetUp(OpTypeAvcSicPayloadINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}