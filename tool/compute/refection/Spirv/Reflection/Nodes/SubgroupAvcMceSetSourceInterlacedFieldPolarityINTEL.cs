using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL : Node
    {
        public SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL()
        {
        }

        public SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL(SpirvTypeBase resultType, Node sourceFieldPolarity, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.SourceFieldPolarity = sourceFieldPolarity;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL;

        public Node SourceFieldPolarity { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SourceFieldPolarity;
                yield return Payload;
        }

        public SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL SetUp(Action<SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SourceFieldPolarity = treeBuilder.GetNode(op.SourceFieldPolarity);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceSetSourceInterlacedFieldPolarityINTEL({ResultType}, {SourceFieldPolarity}, {Payload}, {DebugName})";
        }
    }
}