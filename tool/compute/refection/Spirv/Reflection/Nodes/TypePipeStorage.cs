using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypePipeStorage : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypePipeStorage;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.PipeStorage;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypePipeStorage)op, treeBuilder);
        }


        public void SetUp(OpTypePipeStorage op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}