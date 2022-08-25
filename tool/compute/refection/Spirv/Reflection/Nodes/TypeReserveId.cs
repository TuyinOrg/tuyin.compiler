using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeReserveId : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeReserveId;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.ReserveId;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeReserveId)op, treeBuilder);
        }


        public void SetUp(OpTypeReserveId op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}