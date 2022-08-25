using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL : Node
    {
        public SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL()
        {
        }

        public SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL(SpirvTypeBase resultType, Node srcImage, Node refImage, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.SrcImage = srcImage;
            this.RefImage = refImage;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL;

        public Node SrcImage { get; set; }

        public Node RefImage { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SrcImage;
                yield return RefImage;
                yield return Payload;
        }

        public SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL SetUp(Action<SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SrcImage = treeBuilder.GetNode(op.SrcImage);
            RefImage = treeBuilder.GetNode(op.RefImage);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeEvaluateWithSingleReferenceStreamoutINTEL({ResultType}, {SrcImage}, {RefImage}, {Payload}, {DebugName})";
        }
    }
}