using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeVmeImageINTEL : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeVmeImageINTEL;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.VmeImageINTEL;

        public Node ImageType { get; set; }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeVmeImageINTEL)op, treeBuilder);
        }


        public void SetUp(OpTypeVmeImageINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ImageType = treeBuilder.GetNode(op.ImageType);
            SetUpDecorations(op, treeBuilder);
        }

    }
}