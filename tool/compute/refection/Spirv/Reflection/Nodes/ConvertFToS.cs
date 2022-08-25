using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ConvertFToS : Node
    {
        public ConvertFToS()
        {
        }

        public ConvertFToS(SpirvTypeBase resultType, Node floatValue, string debugName = null)
        {
            this.ResultType = resultType;
            this.FloatValue = floatValue;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpConvertFToS;

        public Node FloatValue { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return FloatValue;
        }

        public ConvertFToS WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpConvertFToS)op, treeBuilder);
        }

        public ConvertFToS SetUp(Action<ConvertFToS> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpConvertFToS op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            FloatValue = treeBuilder.GetNode(op.FloatValue);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ConvertFToS object.</summary>
        /// <returns>A string that represents the ConvertFToS object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ConvertFToS({ResultType}, {FloatValue}, {DebugName})";
        }
    }
}