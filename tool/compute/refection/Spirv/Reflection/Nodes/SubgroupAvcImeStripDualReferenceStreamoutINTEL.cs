using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeStripDualReferenceStreamoutINTEL : Node
    {
        public SubgroupAvcImeStripDualReferenceStreamoutINTEL()
        {
        }

        public SubgroupAvcImeStripDualReferenceStreamoutINTEL(SpirvTypeBase resultType, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeStripDualReferenceStreamoutINTEL;

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Payload;
        }

        public SubgroupAvcImeStripDualReferenceStreamoutINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeStripDualReferenceStreamoutINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeStripDualReferenceStreamoutINTEL SetUp(Action<SubgroupAvcImeStripDualReferenceStreamoutINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeStripDualReferenceStreamoutINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeStripDualReferenceStreamoutINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeStripDualReferenceStreamoutINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeStripDualReferenceStreamoutINTEL({ResultType}, {Payload}, {DebugName})";
        }
    }
}