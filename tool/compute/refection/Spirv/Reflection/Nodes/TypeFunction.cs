using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeFunction : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeFunction;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Function;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeFunction)op, treeBuilder);
        }

        partial void SetUp(OpTypeFunction instruction, SpirvInstructionTreeBuilder treeBuilder);

    }
}