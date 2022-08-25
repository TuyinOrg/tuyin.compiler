using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeRayQueryProvisionalKHR : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeRayQueryProvisionalKHR;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.RayQueryProvisionalKHR;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeRayQueryProvisionalKHR)op, treeBuilder);
        }


        public void SetUp(OpTypeRayQueryProvisionalKHR op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}