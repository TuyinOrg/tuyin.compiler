using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeFloat : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeFloat;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Float;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeFloat)op, treeBuilder);
        }

        partial void SetUp(OpTypeFloat instruction, SpirvInstructionTreeBuilder treeBuilder);

    }
}