using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ConvertUToPtr : Node
    {
        public ConvertUToPtr()
        {
        }

        public ConvertUToPtr(SpirvTypeBase resultType, Node integerValue, string debugName = null)
        {
            this.ResultType = resultType;
            this.IntegerValue = integerValue;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpConvertUToPtr;

        public Node IntegerValue { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return IntegerValue;
        }

        public ConvertUToPtr WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpConvertUToPtr)op, treeBuilder);
        }

        public ConvertUToPtr SetUp(Action<ConvertUToPtr> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpConvertUToPtr op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            IntegerValue = treeBuilder.GetNode(op.IntegerValue);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ConvertUToPtr object.</summary>
        /// <returns>A string that represents the ConvertUToPtr object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ConvertUToPtr({ResultType}, {IntegerValue}, {DebugName})";
        }
    }
}