using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL : Node
    {
        public SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL()
        {
        }

        public SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL(SpirvTypeBase resultType, Node referenceFieldPolarity, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.ReferenceFieldPolarity = referenceFieldPolarity;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL;

        public Node ReferenceFieldPolarity { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return ReferenceFieldPolarity;
                yield return Payload;
        }

        public SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL SetUp(Action<SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            ReferenceFieldPolarity = treeBuilder.GetNode(op.ReferenceFieldPolarity);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceSetSingleReferenceInterlacedFieldPolarityINTEL({ResultType}, {ReferenceFieldPolarity}, {Payload}, {DebugName})";
        }
    }
}