using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class LessOrGreater : Node
    {
        public LessOrGreater()
        {
        }

        public LessOrGreater(SpirvTypeBase resultType, Node x, Node y, string debugName = null)
        {
            this.ResultType = resultType;
            this.x = x;
            this.y = y;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpLessOrGreater;

        public Node x { get; set; }

        public Node y { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return x;
                yield return y;
        }

        public LessOrGreater WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpLessOrGreater)op, treeBuilder);
        }

        public LessOrGreater SetUp(Action<LessOrGreater> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpLessOrGreater op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            x = treeBuilder.GetNode(op.x);
            y = treeBuilder.GetNode(op.y);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the LessOrGreater object.</summary>
        /// <returns>A string that represents the LessOrGreater object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"LessOrGreater({ResultType}, {x}, {y}, {DebugName})";
        }
    }
}