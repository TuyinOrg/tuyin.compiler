using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Variable : Node
    {
        public Variable()
        {
        }

        public Variable(SpirvTypeBase resultType, Spv.StorageClass storageClass, Node initializer, string debugName = null)
        {
            this.ResultType = resultType;
            this.StorageClass = storageClass;
            this.Initializer = initializer;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpVariable;

        public Spv.StorageClass StorageClass { get; set; }

        public Node Initializer { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Initializer;
        }

        public Variable WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpVariable)op, treeBuilder);
        }

        public Variable SetUp(Action<Variable> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpVariable op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            StorageClass = op.StorageClass;
            Initializer = treeBuilder.GetNode(op.Initializer);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Variable object.</summary>
        /// <returns>A string that represents the Variable object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Variable({ResultType}, {StorageClass}, {Initializer}, {DebugName})";
        }
    }
}