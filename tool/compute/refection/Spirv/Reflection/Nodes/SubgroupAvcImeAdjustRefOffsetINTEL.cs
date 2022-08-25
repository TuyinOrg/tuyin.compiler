using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeAdjustRefOffsetINTEL : Node
    {
        public SubgroupAvcImeAdjustRefOffsetINTEL()
        {
        }

        public SubgroupAvcImeAdjustRefOffsetINTEL(SpirvTypeBase resultType, Node refOffset, Node srcCoord, Node refWindowSize, Node imageSize, string debugName = null)
        {
            this.ResultType = resultType;
            this.RefOffset = refOffset;
            this.SrcCoord = srcCoord;
            this.RefWindowSize = refWindowSize;
            this.ImageSize = imageSize;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeAdjustRefOffsetINTEL;

        public Node RefOffset { get; set; }

        public Node SrcCoord { get; set; }

        public Node RefWindowSize { get; set; }

        public Node ImageSize { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return RefOffset;
                yield return SrcCoord;
                yield return RefWindowSize;
                yield return ImageSize;
        }

        public SubgroupAvcImeAdjustRefOffsetINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeAdjustRefOffsetINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeAdjustRefOffsetINTEL SetUp(Action<SubgroupAvcImeAdjustRefOffsetINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeAdjustRefOffsetINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            RefOffset = treeBuilder.GetNode(op.RefOffset);
            SrcCoord = treeBuilder.GetNode(op.SrcCoord);
            RefWindowSize = treeBuilder.GetNode(op.RefWindowSize);
            ImageSize = treeBuilder.GetNode(op.ImageSize);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeAdjustRefOffsetINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeAdjustRefOffsetINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeAdjustRefOffsetINTEL({ResultType}, {RefOffset}, {SrcCoord}, {RefWindowSize}, {ImageSize}, {DebugName})";
        }
    }
}