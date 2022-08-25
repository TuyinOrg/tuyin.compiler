using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeAvcRefPayloadINTEL : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeAvcRefPayloadINTEL;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.AvcRefPayloadINTEL;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeAvcRefPayloadINTEL)op, treeBuilder);
        }


        public void SetUp(OpTypeAvcRefPayloadINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}