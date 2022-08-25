using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SatConvertSToU : Node
    {
        public SatConvertSToU()
        {
        }

        public SatConvertSToU(SpirvTypeBase resultType, Node signedValue, string debugName = null)
        {
            this.ResultType = resultType;
            this.SignedValue = signedValue;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSatConvertSToU;

        public Node SignedValue { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SignedValue;
        }

        public SatConvertSToU WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSatConvertSToU)op, treeBuilder);
        }

        public SatConvertSToU SetUp(Action<SatConvertSToU> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSatConvertSToU op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SignedValue = treeBuilder.GetNode(op.SignedValue);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SatConvertSToU object.</summary>
        /// <returns>A string that represents the SatConvertSToU object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SatConvertSToU({ResultType}, {SignedValue}, {DebugName})";
        }
    }
}