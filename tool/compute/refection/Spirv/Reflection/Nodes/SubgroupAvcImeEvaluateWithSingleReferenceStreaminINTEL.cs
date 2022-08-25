using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL : Node
    {
        public SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL()
        {
        }

        public SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL(SpirvTypeBase resultType, Node srcImage, Node refImage, Node payload, Node streaminComponents, string debugName = null)
        {
            this.ResultType = resultType;
            this.SrcImage = srcImage;
            this.RefImage = refImage;
            this.Payload = payload;
            this.StreaminComponents = streaminComponents;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL;

        public Node SrcImage { get; set; }

        public Node RefImage { get; set; }

        public Node Payload { get; set; }

        public Node StreaminComponents { get; set; }

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
                yield return StreaminComponents;
        }

        public SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL SetUp(Action<SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SrcImage = treeBuilder.GetNode(op.SrcImage);
            RefImage = treeBuilder.GetNode(op.RefImage);
            Payload = treeBuilder.GetNode(op.Payload);
            StreaminComponents = treeBuilder.GetNode(op.StreaminComponents);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeEvaluateWithSingleReferenceStreaminINTEL({ResultType}, {SrcImage}, {RefImage}, {Payload}, {StreaminComponents}, {DebugName})";
        }
    }
}