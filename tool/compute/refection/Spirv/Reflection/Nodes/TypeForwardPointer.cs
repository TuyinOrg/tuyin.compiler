using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeForwardPointer : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeForwardPointer;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.ForwardPointer;

        public SpirvTypeBase PointerType { get; set; }
        public Spv.StorageClass StorageClass { get; set; }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeForwardPointer)op, treeBuilder);
        }


        public void SetUp(OpTypeForwardPointer op, SpirvInstructionTreeBuilder treeBuilder)
        {
            PointerType = treeBuilder.ResolveType(op.PointerType);
            StorageClass = op.StorageClass;
            SetUpDecorations(op, treeBuilder);
        }

    }
}