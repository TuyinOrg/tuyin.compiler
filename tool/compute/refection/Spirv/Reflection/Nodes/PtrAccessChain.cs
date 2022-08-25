using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class PtrAccessChain : Node
    {
        public PtrAccessChain()
        {
        }

        public PtrAccessChain(SpirvTypeBase resultType, Node @base, Node element, IEnumerable<Node> indexes, string debugName = null)
        {
            this.ResultType = resultType;
            this.Base = @base;
            this.Element = element;
            if (indexes != null) { foreach (var node in indexes) this.Indexes.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpPtrAccessChain;

        public Node Base { get; set; }

        public Node Element { get; set; }

        public IList<Node> Indexes { get; private set; } = new PrintableList<Node>();

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Base;
                yield return Element;
                for (var index = 0; index < Indexes.Count; index++)
                {
                    yield return Indexes[index];
                }
        }

        public PtrAccessChain WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpPtrAccessChain)op, treeBuilder);
        }

        public PtrAccessChain SetUp(Action<PtrAccessChain> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpPtrAccessChain op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Base = treeBuilder.GetNode(op.Base);
            Element = treeBuilder.GetNode(op.Element);
            Indexes = treeBuilder.GetNodes(op.Indexes);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the PtrAccessChain object.</summary>
        /// <returns>A string that represents the PtrAccessChain object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"PtrAccessChain({ResultType}, {Base}, {Element}, {Indexes}, {DebugName})";
        }
    }
}