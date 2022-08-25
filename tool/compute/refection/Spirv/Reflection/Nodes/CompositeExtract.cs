using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CompositeExtract : Node
    {
        public CompositeExtract()
        {
        }

        public CompositeExtract(SpirvTypeBase resultType, Node composite, IList<uint> indexes, string debugName = null)
        {
            this.ResultType = resultType;
            this.Composite = composite;
            if (indexes != null) { foreach (var node in indexes) this.Indexes.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCompositeExtract;

        public Node Composite { get; set; }

        public IList<uint> Indexes { get; private set; } = new List<uint>();

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Composite;
        }

        public CompositeExtract WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCompositeExtract)op, treeBuilder);
        }

        public CompositeExtract SetUp(Action<CompositeExtract> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCompositeExtract op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Composite = treeBuilder.GetNode(op.Composite);
            foreach (var item in op.Indexes) { Indexes.Add(treeBuilder.Parse(item)); }
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CompositeExtract object.</summary>
        /// <returns>A string that represents the CompositeExtract object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CompositeExtract({ResultType}, {Composite}, {Indexes}, {DebugName})";
        }
    }
}