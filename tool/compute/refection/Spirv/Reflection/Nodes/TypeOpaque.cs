using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeOpaque : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeOpaque;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Opaque;

        public string Thenameoftheopaquetype { get; set; }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeOpaque)op, treeBuilder);
        }


        public void SetUp(OpTypeOpaque op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Thenameoftheopaquetype = op.Thenameoftheopaquetype;
            SetUpDecorations(op, treeBuilder);
        }

    }
}