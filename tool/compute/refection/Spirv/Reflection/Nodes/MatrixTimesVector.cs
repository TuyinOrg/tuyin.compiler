using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class MatrixTimesVector : Node
    {
        public MatrixTimesVector()
        {
        }

        public MatrixTimesVector(SpirvTypeBase resultType, Node matrix, Node vector, string debugName = null)
        {
            this.ResultType = resultType;
            this.Matrix = matrix;
            this.Vector = vector;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpMatrixTimesVector;

        public Node Matrix { get; set; }

        public Node Vector { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Matrix;
                yield return Vector;
        }

        public MatrixTimesVector WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpMatrixTimesVector)op, treeBuilder);
        }

        public MatrixTimesVector SetUp(Action<MatrixTimesVector> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpMatrixTimesVector op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Matrix = treeBuilder.GetNode(op.Matrix);
            Vector = treeBuilder.GetNode(op.Vector);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the MatrixTimesVector object.</summary>
        /// <returns>A string that represents the MatrixTimesVector object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"MatrixTimesVector({ResultType}, {Matrix}, {Vector}, {DebugName})";
        }
    }
}