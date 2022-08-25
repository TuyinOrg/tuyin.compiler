using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeVector : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeVector;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Vector;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeVector)op, treeBuilder);
        }

        partial void SetUp(OpTypeVector instruction, SpirvInstructionTreeBuilder treeBuilder);

    }
}