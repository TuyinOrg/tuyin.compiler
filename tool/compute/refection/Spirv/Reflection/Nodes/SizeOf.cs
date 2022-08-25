using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SizeOf : Node
    {
        public SizeOf()
        {
        }

        public SizeOf(SpirvTypeBase resultType, Node pointer, string debugName = null)
        {
            this.ResultType = resultType;
            this.Pointer = pointer;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSizeOf;

        public Node Pointer { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pointer;
        }

        public SizeOf WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSizeOf)op, treeBuilder);
        }

        public SizeOf SetUp(Action<SizeOf> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSizeOf op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Pointer = treeBuilder.GetNode(op.Pointer);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SizeOf object.</summary>
        /// <returns>A string that represents the SizeOf object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SizeOf({ResultType}, {Pointer}, {DebugName})";
        }
    }
}