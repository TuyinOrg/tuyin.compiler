using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class VectorShuffle : Node
    {
        public VectorShuffle()
        {
        }

        public VectorShuffle(SpirvTypeBase resultType, Node vector1, Node vector2, IList<uint> components, string debugName = null)
        {
            this.ResultType = resultType;
            this.Vector1 = vector1;
            this.Vector2 = vector2;
            if (components != null) { foreach (var node in components) this.Components.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpVectorShuffle;

        public Node Vector1 { get; set; }

        public Node Vector2 { get; set; }

        public IList<uint> Components { get; private set; } = new List<uint>();

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

        public VectorShuffle WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpVectorShuffle)op, treeBuilder);
        }

        public VectorShuffle SetUp(Action<VectorShuffle> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpVectorShuffle op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Vector1 = treeBuilder.GetNode(op.Vector1);
            Vector2 = treeBuilder.GetNode(op.Vector2);
            foreach (var item in op.Components) { Components.Add(treeBuilder.Parse(item)); }
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the VectorShuffle object.</summary>
        /// <returns>A string that represents the VectorShuffle object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"VectorShuffle({ResultType}, {Vector1}, {Vector2}, {Components}, {DebugName})";
        }
    }
}