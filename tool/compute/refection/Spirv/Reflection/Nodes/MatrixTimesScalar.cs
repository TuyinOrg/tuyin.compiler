using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class MatrixTimesScalar : Node
    {
        public MatrixTimesScalar()
        {
        }

        public MatrixTimesScalar(SpirvTypeBase resultType, Node matrix, Node scalar, string debugName = null)
        {
            this.ResultType = resultType;
            this.Matrix = matrix;
            this.Scalar = scalar;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpMatrixTimesScalar;

        public Node Matrix { get; set; }

        public Node Scalar { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Matrix;
                yield return Scalar;
        }

        public MatrixTimesScalar WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpMatrixTimesScalar)op, treeBuilder);
        }

        public MatrixTimesScalar SetUp(Action<MatrixTimesScalar> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpMatrixTimesScalar op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Matrix = treeBuilder.GetNode(op.Matrix);
            Scalar = treeBuilder.GetNode(op.Scalar);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the MatrixTimesScalar object.</summary>
        /// <returns>A string that represents the MatrixTimesScalar object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"MatrixTimesScalar({ResultType}, {Matrix}, {Scalar}, {DebugName})";
        }
    }
}