using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SatConvertUToS : Node
    {
        public SatConvertUToS()
        {
        }

        public SatConvertUToS(SpirvTypeBase resultType, Node unsignedValue, string debugName = null)
        {
            this.ResultType = resultType;
            this.UnsignedValue = unsignedValue;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSatConvertUToS;

        public Node UnsignedValue { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return UnsignedValue;
        }

        public SatConvertUToS WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSatConvertUToS)op, treeBuilder);
        }

        public SatConvertUToS SetUp(Action<SatConvertUToS> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSatConvertUToS op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            UnsignedValue = treeBuilder.GetNode(op.UnsignedValue);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SatConvertUToS object.</summary>
        /// <returns>A string that represents the SatConvertUToS object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SatConvertUToS({ResultType}, {UnsignedValue}, {DebugName})";
        }
    }
}