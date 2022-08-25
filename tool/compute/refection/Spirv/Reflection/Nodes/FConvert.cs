using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class FConvert : Node
    {
        public FConvert()
        {
        }

        public FConvert(SpirvTypeBase resultType, Node floatValue, string debugName = null)
        {
            this.ResultType = resultType;
            this.FloatValue = floatValue;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpFConvert;

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

        public FConvert WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpFConvert)op, treeBuilder);
        }

        public FConvert SetUp(Action<FConvert> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpFConvert op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            FloatValue = treeBuilder.GetNode(op.FloatValue);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the FConvert object.</summary>
        /// <returns>A string that represents the FConvert object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"FConvert({ResultType}, {FloatValue}, {DebugName})";
        }
    }
}