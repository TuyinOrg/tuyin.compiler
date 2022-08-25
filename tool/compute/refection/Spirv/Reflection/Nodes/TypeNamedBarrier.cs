using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeNamedBarrier : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeNamedBarrier;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.NamedBarrier;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeNamedBarrier)op, treeBuilder);
        }


        public void SetUp(OpTypeNamedBarrier op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}