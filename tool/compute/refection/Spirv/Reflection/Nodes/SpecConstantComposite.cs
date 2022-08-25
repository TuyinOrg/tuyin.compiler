using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SpecConstantComposite : Node
    {
        public SpecConstantComposite()
        {
        }

        public SpecConstantComposite(SpirvTypeBase resultType, IEnumerable<Node> constituents, string debugName = null)
        {
            this.ResultType = resultType;
            if (constituents != null) { foreach (var node in constituents) this.Constituents.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSpecConstantComposite;

        public IList<Node> Constituents { get; private set; } = new PrintableList<Node>();

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                for (var index = 0; index < Constituents.Count; index++)
                {
                    yield return Constituents[index];
                }
        }

        public SpecConstantComposite WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSpecConstantComposite)op, treeBuilder);
        }

        public SpecConstantComposite SetUp(Action<SpecConstantComposite> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSpecConstantComposite op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Constituents = treeBuilder.GetNodes(op.Constituents);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SpecConstantComposite object.</summary>
        /// <returns>A string that represents the SpecConstantComposite object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SpecConstantComposite({ResultType}, {Constituents}, {DebugName})";
        }
    }
}