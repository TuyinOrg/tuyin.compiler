using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CompositeInsert : Node
    {
        public CompositeInsert()
        {
        }

        public CompositeInsert(SpirvTypeBase resultType, Node @object, Node composite, IList<uint> indexes, string debugName = null)
        {
            this.ResultType = resultType;
            this.Object = @object;
            this.Composite = composite;
            if (indexes != null) { foreach (var node in indexes) this.Indexes.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCompositeInsert;

        public Node Object { get; set; }

        public Node Composite { get; set; }

        public IList<uint> Indexes { get; private set; } = new List<uint>();

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Object;
                yield return Composite;
        }

        public CompositeInsert WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCompositeInsert)op, treeBuilder);
        }

        public CompositeInsert SetUp(Action<CompositeInsert> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCompositeInsert op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Object = treeBuilder.GetNode(op.Object);
            Composite = treeBuilder.GetNode(op.Composite);
            foreach (var item in op.Indexes) { Indexes.Add(treeBuilder.Parse(item)); }
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CompositeInsert object.</summary>
        /// <returns>A string that represents the CompositeInsert object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CompositeInsert({ResultType}, {Object}, {Composite}, {Indexes}, {DebugName})";
        }
    }
}