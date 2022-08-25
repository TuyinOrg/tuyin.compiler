using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeVoid : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeVoid;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Void;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeVoid)op, treeBuilder);
        }

        partial void SetUp(OpTypeVoid instruction, SpirvInstructionTreeBuilder treeBuilder);

    }
}