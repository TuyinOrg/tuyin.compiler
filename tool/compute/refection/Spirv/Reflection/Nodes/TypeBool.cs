using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeBool : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeBool;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Bool;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeBool)op, treeBuilder);
        }

        partial void SetUp(OpTypeBool instruction, SpirvInstructionTreeBuilder treeBuilder);

    }
}