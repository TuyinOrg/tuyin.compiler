using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Select : Node
    {
        public Select()
        {
        }

        public Select(SpirvTypeBase resultType, Node condition, Node object1, Node object2, string debugName = null)
        {
            this.ResultType = resultType;
            this.Condition = condition;
            this.Object1 = object1;
            this.Object2 = object2;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSelect;

        public Node Condition { get; set; }

        public Node Object1 { get; set; }

        public Node Object2 { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Condition;
                yield return Object1;
                yield return Object2;
        }

        public Select WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSelect)op, treeBuilder);
        }

        public Select SetUp(Action<Select> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSelect op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Condition = treeBuilder.GetNode(op.Condition);
            Object1 = treeBuilder.GetNode(op.Object1);
            Object2 = treeBuilder.GetNode(op.Object2);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Select object.</summary>
        /// <returns>A string that represents the Select object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Select({ResultType}, {Condition}, {Object1}, {Object2}, {DebugName})";
        }
    }
}