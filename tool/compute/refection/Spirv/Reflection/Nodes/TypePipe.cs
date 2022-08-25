using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypePipe : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypePipe;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Pipe;

        public Spv.AccessQualifier Qualifier { get; set; }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypePipe)op, treeBuilder);
        }


        public void SetUp(OpTypePipe op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Qualifier = op.Qualifier;
            SetUpDecorations(op, treeBuilder);
        }

    }
}