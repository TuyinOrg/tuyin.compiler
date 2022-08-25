using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeAvcMcePayloadINTEL : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeAvcMcePayloadINTEL;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.AvcMcePayloadINTEL;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeAvcMcePayloadINTEL)op, treeBuilder);
        }


        public void SetUp(OpTypeAvcMcePayloadINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}