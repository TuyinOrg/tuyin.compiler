using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeStruct : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeStruct;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Struct;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeStruct)op, treeBuilder);
        }

        partial void SetUp(OpTypeStruct instruction, SpirvInstructionTreeBuilder treeBuilder);

    }
}