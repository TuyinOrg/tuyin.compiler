using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Transpose : Node
    {
        public Transpose()
        {
        }

        public Transpose(SpirvTypeBase resultType, Node matrix, string debugName = null)
        {
            this.ResultType = resultType;
            this.Matrix = matrix;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpTranspose;

        public Node Matrix { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Matrix;
        }

        public Transpose WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTranspose)op, treeBuilder);
        }

        public Transpose SetUp(Action<Transpose> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpTranspose op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Matrix = treeBuilder.GetNode(op.Matrix);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Transpose object.</summary>
        /// <returns>A string that represents the Transpose object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Transpose({ResultType}, {Matrix}, {DebugName})";
        }
    }
}