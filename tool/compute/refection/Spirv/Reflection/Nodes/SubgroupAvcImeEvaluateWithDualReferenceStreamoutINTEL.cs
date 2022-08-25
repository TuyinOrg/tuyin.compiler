using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL : Node
    {
        public SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL()
        {
        }

        public SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL(SpirvTypeBase resultType, Node srcImage, Node fwdRefImage, Node bwdRefImage, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.SrcImage = srcImage;
            this.FwdRefImage = fwdRefImage;
            this.BwdRefImage = bwdRefImage;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL;

        public Node SrcImage { get; set; }

        public Node FwdRefImage { get; set; }

        public Node BwdRefImage { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SrcImage;
                yield return FwdRefImage;
                yield return BwdRefImage;
                yield return Payload;
        }

        public SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL SetUp(Action<SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SrcImage = treeBuilder.GetNode(op.SrcImage);
            FwdRefImage = treeBuilder.GetNode(op.FwdRefImage);
            BwdRefImage = treeBuilder.GetNode(op.BwdRefImage);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeEvaluateWithDualReferenceStreamoutINTEL({ResultType}, {SrcImage}, {FwdRefImage}, {BwdRefImage}, {Payload}, {DebugName})";
        }
    }
}