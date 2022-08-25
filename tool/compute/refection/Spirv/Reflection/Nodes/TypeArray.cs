using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeArray : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeArray;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Array;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeArray)op, treeBuilder);
        }

        partial void SetUp(OpTypeArray instruction, SpirvInstructionTreeBuilder treeBuilder);

    }
}