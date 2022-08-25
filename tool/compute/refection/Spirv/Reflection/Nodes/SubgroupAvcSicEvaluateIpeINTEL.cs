using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcSicEvaluateIpeINTEL : Node
    {
        public SubgroupAvcSicEvaluateIpeINTEL()
        {
        }

        public SubgroupAvcSicEvaluateIpeINTEL(SpirvTypeBase resultType, Node srcImage, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.SrcImage = srcImage;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcSicEvaluateIpeINTEL;

        public Node SrcImage { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SrcImage;
                yield return Payload;
        }

        public SubgroupAvcSicEvaluateIpeINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcSicEvaluateIpeINTEL)op, treeBuilder);
        }

        public SubgroupAvcSicEvaluateIpeINTEL SetUp(Action<SubgroupAvcSicEvaluateIpeINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcSicEvaluateIpeINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SrcImage = treeBuilder.GetNode(op.SrcImage);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcSicEvaluateIpeINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcSicEvaluateIpeINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcSicEvaluateIpeINTEL({ResultType}, {SrcImage}, {Payload}, {DebugName})";
        }
    }
}