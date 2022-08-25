using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeQueue : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeQueue;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Queue;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeQueue)op, treeBuilder);
        }


        public void SetUp(OpTypeQueue op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}