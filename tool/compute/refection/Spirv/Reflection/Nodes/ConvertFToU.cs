using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ConvertFToU : Node
    {
        public ConvertFToU()
        {
        }

        public ConvertFToU(SpirvTypeBase resultType, Node floatValue, string debugName = null)
        {
            this.ResultType = resultType;
            this.FloatValue = floatValue;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpConvertFToU;

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

        public ConvertFToU WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpConvertFToU)op, treeBuilder);
        }

        public ConvertFToU SetUp(Action<ConvertFToU> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpConvertFToU op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            FloatValue = treeBuilder.GetNode(op.FloatValue);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ConvertFToU object.</summary>
        /// <returns>A string that represents the ConvertFToU object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ConvertFToU({ResultType}, {FloatValue}, {DebugName})";
        }
    }
}