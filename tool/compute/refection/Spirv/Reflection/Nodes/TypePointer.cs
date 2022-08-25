using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypePointer : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypePointer;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.Pointer;

        public Spv.StorageClass StorageClass { get; set; }
        public SpirvTypeBase Type { get; set; }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypePointer)op, treeBuilder);
        }


        public void SetUp(OpTypePointer op, SpirvInstructionTreeBuilder treeBuilder)
        {
            StorageClass = op.StorageClass;
            Type = treeBuilder.ResolveType(op.Type);
            SetUpDecorations(op, treeBuilder);
        }

    }
}