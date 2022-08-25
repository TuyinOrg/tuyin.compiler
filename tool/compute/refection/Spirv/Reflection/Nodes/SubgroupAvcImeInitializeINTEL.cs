using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeInitializeINTEL : Node
    {
        public SubgroupAvcImeInitializeINTEL()
        {
        }

        public SubgroupAvcImeInitializeINTEL(SpirvTypeBase resultType, Node srcCoord, Node partitionMask, Node sADAdjustment, string debugName = null)
        {
            this.ResultType = resultType;
            this.SrcCoord = srcCoord;
            this.PartitionMask = partitionMask;
            this.SADAdjustment = sADAdjustment;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeInitializeINTEL;

        public Node SrcCoord { get; set; }

        public Node PartitionMask { get; set; }

        public Node SADAdjustment { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SrcCoord;
                yield return PartitionMask;
                yield return SADAdjustment;
        }

        public SubgroupAvcImeInitializeINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeInitializeINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeInitializeINTEL SetUp(Action<SubgroupAvcImeInitializeINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeInitializeINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SrcCoord = treeBuilder.GetNode(op.SrcCoord);
            PartitionMask = treeBuilder.GetNode(op.PartitionMask);
            SADAdjustment = treeBuilder.GetNode(op.SADAdjustment);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeInitializeINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeInitializeINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeInitializeINTEL({ResultType}, {SrcCoord}, {PartitionMask}, {SADAdjustment}, {DebugName})";
        }
    }
}