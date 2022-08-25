using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeInt : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeInt;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Int;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeInt)op, treeBuilder);
        }

        partial void SetUp(OpTypeInt instruction, SpirvInstructionTreeBuilder treeBuilder);

    }
}