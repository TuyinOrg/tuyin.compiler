using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class VectorInsertDynamic : Node
    {
        public VectorInsertDynamic()
        {
        }

        public VectorInsertDynamic(SpirvTypeBase resultType, Node vector, Node component, Node index, string debugName = null)
        {
            this.ResultType = resultType;
            this.Vector = vector;
            this.Component = component;
            this.Index = index;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpVectorInsertDynamic;

        public Node Vector { get; set; }

        public Node Component { get; set; }

        public Node Index { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Vector;
                yield return Component;
                yield return Index;
        }

        public VectorInsertDynamic WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpVectorInsertDynamic)op, treeBuilder);
        }

        public VectorInsertDynamic SetUp(Action<VectorInsertDynamic> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpVectorInsertDynamic op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Vector = treeBuilder.GetNode(op.Vector);
            Component = treeBuilder.GetNode(op.Component);
            Index = treeBuilder.GetNode(op.Index);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the VectorInsertDynamic object.</summary>
        /// <returns>A string that represents the VectorInsertDynamic object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"VectorInsertDynamic({ResultType}, {Vector}, {Component}, {Index}, {DebugName})";
        }
    }
}