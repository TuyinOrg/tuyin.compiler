using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class IsValidReserveId : Node
    {
        public IsValidReserveId()
        {
        }

        public IsValidReserveId(SpirvTypeBase resultType, Node reserveId, string debugName = null)
        {
            this.ResultType = resultType;
            this.ReserveId = reserveId;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpIsValidReserveId;

        public Node ReserveId { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return ReserveId;
        }

        public IsValidReserveId WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpIsValidReserveId)op, treeBuilder);
        }

        public IsValidReserveId SetUp(Action<IsValidReserveId> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpIsValidReserveId op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            ReserveId = treeBuilder.GetNode(op.ReserveId);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the IsValidReserveId object.</summary>
        /// <returns>A string that represents the IsValidReserveId object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"IsValidReserveId({ResultType}, {ReserveId}, {DebugName})";
        }
    }
}