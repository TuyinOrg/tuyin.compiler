using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CopyObject : Node
    {
        public CopyObject()
        {
        }

        public CopyObject(SpirvTypeBase resultType, Node operand, string debugName = null)
        {
            this.ResultType = resultType;
            this.Operand = operand;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCopyObject;

        public Node Operand { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Operand;
        }

        public CopyObject WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCopyObject)op, treeBuilder);
        }

        public CopyObject SetUp(Action<CopyObject> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCopyObject op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Operand = treeBuilder.GetNode(op.Operand);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CopyObject object.</summary>
        /// <returns>A string that represents the CopyObject object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CopyObject({ResultType}, {Operand}, {DebugName})";
        }
    }
}