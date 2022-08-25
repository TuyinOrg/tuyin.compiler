using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CooperativeMatrixLengthNV : Node
    {
        public CooperativeMatrixLengthNV()
        {
        }

        public CooperativeMatrixLengthNV(SpirvTypeBase resultType, Node type, string debugName = null)
        {
            this.ResultType = resultType;
            this.Type = type;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCooperativeMatrixLengthNV;

        public Node Type { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Type;
        }

        public CooperativeMatrixLengthNV WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCooperativeMatrixLengthNV)op, treeBuilder);
        }

        public CooperativeMatrixLengthNV SetUp(Action<CooperativeMatrixLengthNV> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCooperativeMatrixLengthNV op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Type = treeBuilder.GetNode(op.Type);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CooperativeMatrixLengthNV object.</summary>
        /// <returns>A string that represents the CooperativeMatrixLengthNV object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CooperativeMatrixLengthNV({ResultType}, {Type}, {DebugName})";
        }
    }
}