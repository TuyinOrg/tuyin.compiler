using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeImage : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeImage;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Image;

        public SpirvTypeBase SampledType { get; set; }
        public Spv.Dim Dim { get; set; }
        public uint Depth { get; set; }
        public uint Arrayed { get; set; }
        public uint MS { get; set; }
        public uint Sampled { get; set; }
        public Spv.ImageFormat ImageFormat { get; set; }
        public Spv.AccessQualifier AccessQualifier { get; set; }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeImage)op, treeBuilder);
        }


        public void SetUp(OpTypeImage op, SpirvInstructionTreeBuilder treeBuilder)
        {
            SampledType = treeBuilder.ResolveType(op.SampledType);
            Dim = op.Dim;
            Depth = op.Depth;
            Arrayed = op.Arrayed;
            MS = op.MS;
            Sampled = op.Sampled;
            ImageFormat = op.ImageFormat;
            AccessQualifier = op.AccessQualifier;
            SetUpDecorations(op, treeBuilder);
        }

    }
}