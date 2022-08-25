using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeMatrix : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeMatrix;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Matrix;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeMatrix)op, treeBuilder);
        }

        partial void SetUp(OpTypeMatrix instruction, SpirvInstructionTreeBuilder treeBuilder);

    }
}