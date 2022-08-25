using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class All : Node
    {
        public All()
        {
        }

        public All(SpirvTypeBase resultType, Node vector, string debugName = null)
        {
            this.ResultType = resultType;
            this.Vector = vector;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpAll;

        public Node Vector { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Vector;
        }

        public All WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpAll)op, treeBuilder);
        }

        public All SetUp(Action<All> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpAll op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Vector = treeBuilder.GetNode(op.Vector);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the All object.</summary>
        /// <returns>A string that represents the All object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"All({ResultType}, {Vector}, {DebugName})";
        }
    }
}