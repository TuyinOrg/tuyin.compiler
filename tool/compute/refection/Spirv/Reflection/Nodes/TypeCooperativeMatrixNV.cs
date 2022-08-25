using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeCooperativeMatrixNV : SpirvTypeBase
    {
        public override Op OpCode => Op.OpTypeCooperativeMatrixNV;

        public override SpirvTypeCategory TypeCategory => SpirvTypeCategory.CooperativeMatrixNV;

        public Node ComponentType { get; set; }
        public uint Execution { get; set; }
        public Node Rows { get; set; }
        public Node Columns { get; set; }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTypeCooperativeMatrixNV)op, treeBuilder);
        }


        public void SetUp(OpTypeCooperativeMatrixNV op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ComponentType = treeBuilder.GetNode(op.ComponentType);
            Execution = op.Execution;
            Rows = treeBuilder.GetNode(op.Rows);
            Columns = treeBuilder.GetNode(op.Columns);
            SetUpDecorations(op, treeBuilder);
        }

    }
}