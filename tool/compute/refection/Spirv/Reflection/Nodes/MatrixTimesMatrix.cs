using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class MatrixTimesMatrix : Node
    {
        public MatrixTimesMatrix()
        {
        }

        public MatrixTimesMatrix(SpirvTypeBase resultType, Node leftMatrix, Node rightMatrix, string debugName = null)
        {
            this.ResultType = resultType;
            this.LeftMatrix = leftMatrix;
            this.RightMatrix = rightMatrix;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpMatrixTimesMatrix;

        public Node LeftMatrix { get; set; }

        public Node RightMatrix { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return LeftMatrix;
                yield return RightMatrix;
        }

        public MatrixTimesMatrix WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpMatrixTimesMatrix)op, treeBuilder);
        }

        public MatrixTimesMatrix SetUp(Action<MatrixTimesMatrix> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpMatrixTimesMatrix op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            LeftMatrix = treeBuilder.GetNode(op.LeftMatrix);
            RightMatrix = treeBuilder.GetNode(op.RightMatrix);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the MatrixTimesMatrix object.</summary>
        /// <returns>A string that represents the MatrixTimesMatrix object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"MatrixTimesMatrix({ResultType}, {LeftMatrix}, {RightMatrix}, {DebugName})";
        }
    }
}