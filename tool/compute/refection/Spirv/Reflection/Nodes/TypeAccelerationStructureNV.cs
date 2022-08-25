using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeAccelerationStructureNV : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeAccelerationStructureNV;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.AccelerationStructureNV;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeAccelerationStructureNV)op, treeBuilder);
        }


        public void SetUp(OpTypeAccelerationStructureNV op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}