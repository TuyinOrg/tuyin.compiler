using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeEvent : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeEvent;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Event;


        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeEvent)op, treeBuilder);
        }


        public void SetUp(OpTypeEvent op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SetUpDecorations(op, treeBuilder);
        }

    }
}