using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeSampledImage : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeSampledImage;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.SampledImage;

        public SpirvTypeBase ImageType { get; set; }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeSampledImage)op, treeBuilder);
        }


        public void SetUp(OpTypeSampledImage op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ImageType = treeBuilder.ResolveType(op.ImageType);
            SetUpDecorations(op, treeBuilder);
        }

    }
}