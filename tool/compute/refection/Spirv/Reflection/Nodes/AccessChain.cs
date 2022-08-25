using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class AccessChain : Node
    {
        public AccessChain()
        {
        }

        public AccessChain(SpirvTypeBase resultType, Node @base, IEnumerable<Node> indexes, string debugName = null)
        {
            this.ResultType = resultType;
            this.Base = @base;
            if (indexes != null) { foreach (var node in indexes) this.Indexes.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpAccessChain;

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

        public AccessChain WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpAccessChain)op, treeBuilder);
        }

        public AccessChain SetUp(Action<AccessChain> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpAccessChain op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Base = treeBuilder.GetNode(op.Base);
            Indexes = treeBuilder.GetNodes(op.Indexes);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the AccessChain object.</summary>
        /// <returns>A string that represents the AccessChain object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"AccessChain({ResultType}, {Base}, {Indexes}, {DebugName})";
        }
    }
}