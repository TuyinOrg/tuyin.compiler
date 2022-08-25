using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Dot : Node
    {
        public Dot()
        {
        }

        public Dot(SpirvTypeBase resultType, Node vector1, Node vector2, string debugName = null)
        {
            this.ResultType = resultType;
            this.Vector1 = vector1;
            this.Vector2 = vector2;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpDot;

        public Node Vector1 { get; set; }

        public Node Vector2 { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Vector1;
                yield return Vector2;
        }

        public Dot WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpDot)op, treeBuilder);
        }

        public Dot SetUp(Action<Dot> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpDot op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Vector1 = treeBuilder.GetNode(op.Vector1);
            Vector2 = treeBuilder.GetNode(op.Vector2);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Dot object.</summary>
        /// <returns>A string that represents the Dot object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Dot({ResultType}, {Vector1}, {Vector2}, {DebugName})";
        }
    }
}