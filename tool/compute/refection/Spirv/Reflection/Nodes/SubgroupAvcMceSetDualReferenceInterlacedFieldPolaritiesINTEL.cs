using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL : Node
    {
        public SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL()
        {
        }

        public SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL(SpirvTypeBase resultType, Node forwardReferenceFieldPolarity, Node backwardReferenceFieldPolarity, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.ForwardReferenceFieldPolarity = forwardReferenceFieldPolarity;
            this.BackwardReferenceFieldPolarity = backwardReferenceFieldPolarity;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL;

        public Node ForwardReferenceFieldPolarity { get; set; }

        public Node BackwardReferenceFieldPolarity { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return ForwardReferenceFieldPolarity;
                yield return BackwardReferenceFieldPolarity;
                yield return Payload;
        }

        public SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL SetUp(Action<SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            ForwardReferenceFieldPolarity = treeBuilder.GetNode(op.ForwardReferenceFieldPolarity);
            BackwardReferenceFieldPolarity = treeBuilder.GetNode(op.BackwardReferenceFieldPolarity);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceSetDualReferenceInterlacedFieldPolaritiesINTEL({ResultType}, {ForwardReferenceFieldPolarity}, {BackwardReferenceFieldPolarity}, {Payload}, {DebugName})";
        }
    }
}