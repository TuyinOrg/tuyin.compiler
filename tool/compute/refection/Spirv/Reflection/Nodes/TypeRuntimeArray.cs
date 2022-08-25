using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeRuntimeArray : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeRuntimeArray;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.RuntimeArray;

        public SpirvTypeBase ElementType { get; set; }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeRuntimeArray)op, treeBuilder);
        }


        public void SetUp(OpTypeRuntimeArray op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ElementType = treeBuilder.ResolveType(op.ElementType);
            SetUpDecorations(op, treeBuilder);
        }

    }
}