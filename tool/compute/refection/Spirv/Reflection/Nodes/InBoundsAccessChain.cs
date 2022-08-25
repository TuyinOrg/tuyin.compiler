using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class InBoundsAccessChain : Node
    {
        public InBoundsAccessChain()
        {
        }

        public InBoundsAccessChain(SpirvTypeBase resultType, Node @base, IEnumerable<Node> indexes, string debugName = null)
        {
            this.ResultType = resultType;
            this.Base = @base;
            if (indexes != null) { foreach (var node in indexes) this.Indexes.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpInBoundsAccessChain;

        public Node Base { get; set; }

        public IList<Node> Indexes { get; private set; } = new PrintableList<Node>();

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Base;
                for (var index = 0; index < Indexes.Count; index++)
                {
                    yield return Indexes[index];
                }
        }

        public InBoundsAccessChain WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpInBoundsAccessChain)op, treeBuilder);
        }

        public InBoundsAccessChain SetUp(Action<InBoundsAccessChain> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpInBoundsAccessChain op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Base = treeBuilder.GetNode(op.Base);
            Indexes = treeBuilder.GetNodes(op.Indexes);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the InBoundsAccessChain object.</summary>
        /// <returns>A string that represents the InBoundsAccessChain object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"InBoundsAccessChain({ResultType}, {Base}, {Indexes}, {DebugName})";
        }
    }
}