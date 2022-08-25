using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class IsNan : Node
    {
        public IsNan()
        {
        }

        public IsNan(SpirvTypeBase resultType, Node x, string debugName = null)
        {
            this.ResultType = resultType;
            this.x = x;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpIsNan;

        public Node x { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return x;
        }

        public IsNan WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpIsNan)op, treeBuilder);
        }

        public IsNan SetUp(Action<IsNan> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpIsNan op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            x = treeBuilder.GetNode(op.x);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the IsNan object.</summary>
        /// <returns>A string that represents the IsNan object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"IsNan({ResultType}, {x}, {DebugName})";
        }
    }
}