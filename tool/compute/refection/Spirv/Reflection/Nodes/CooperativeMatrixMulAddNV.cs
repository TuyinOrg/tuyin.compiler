using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CooperativeMatrixMulAddNV : Node
    {
        public CooperativeMatrixMulAddNV()
        {
        }

        public CooperativeMatrixMulAddNV(SpirvTypeBase resultType, Node a, Node b, Node c, string debugName = null)
        {
            this.ResultType = resultType;
            this.A = a;
            this.B = b;
            this.C = c;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCooperativeMatrixMulAddNV;

        public Node A { get; set; }

        public Node B { get; set; }

        public Node C { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return A;
                yield return B;
                yield return C;
        }

        public CooperativeMatrixMulAddNV WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCooperativeMatrixMulAddNV)op, treeBuilder);
        }

        public CooperativeMatrixMulAddNV SetUp(Action<CooperativeMatrixMulAddNV> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCooperativeMatrixMulAddNV op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            A = treeBuilder.GetNode(op.A);
            B = treeBuilder.GetNode(op.B);
            C = treeBuilder.GetNode(op.C);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CooperativeMatrixMulAddNV object.</summary>
        /// <returns>A string that represents the CooperativeMatrixMulAddNV object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CooperativeMatrixMulAddNV({ResultType}, {A}, {B}, {C}, {DebugName})";
        }
    }
}