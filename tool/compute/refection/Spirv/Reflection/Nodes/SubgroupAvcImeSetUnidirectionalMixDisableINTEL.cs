using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeSetUnidirectionalMixDisableINTEL : Node
    {
        public SubgroupAvcImeSetUnidirectionalMixDisableINTEL()
        {
        }

        public SubgroupAvcImeSetUnidirectionalMixDisableINTEL(SpirvTypeBase resultType, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeSetUnidirectionalMixDisableINTEL;

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

        public SubgroupAvcImeSetUnidirectionalMixDisableINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeSetUnidirectionalMixDisableINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeSetUnidirectionalMixDisableINTEL SetUp(Action<SubgroupAvcImeSetUnidirectionalMixDisableINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeSetUnidirectionalMixDisableINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeSetUnidirectionalMixDisableINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeSetUnidirectionalMixDisableINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeSetUnidirectionalMixDisableINTEL({ResultType}, {Payload}, {DebugName})";
        }
    }
}