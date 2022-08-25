using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcSicConfigureIpeLumaChromaINTEL : Node
    {
        public SubgroupAvcSicConfigureIpeLumaChromaINTEL()
        {
        }

        public SubgroupAvcSicConfigureIpeLumaChromaINTEL(SpirvTypeBase resultType, Node lumaIntraPartitionMask, Node intraNeighbourAvailabilty, Node leftEdgeLumaPixels, Node upperLeftCornerLumaPixel, Node upperEdgeLumaPixels, Node upperRightEdgeLumaPixels, Node leftEdgeChromaPixels, Node upperLeftCornerChromaPixel, Node upperEdgeChromaPixels, Node sadAdjustment, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.LumaIntraPartitionMask = lumaIntraPartitionMask;
            this.IntraNeighbourAvailabilty = intraNeighbourAvailabilty;
            this.LeftEdgeLumaPixels = leftEdgeLumaPixels;
            this.UpperLeftCornerLumaPixel = upperLeftCornerLumaPixel;
            this.UpperEdgeLumaPixels = upperEdgeLumaPixels;
            this.UpperRightEdgeLumaPixels = upperRightEdgeLumaPixels;
            this.LeftEdgeChromaPixels = leftEdgeChromaPixels;
            this.UpperLeftCornerChromaPixel = upperLeftCornerChromaPixel;
            this.UpperEdgeChromaPixels = upperEdgeChromaPixels;
            this.SadAdjustment = sadAdjustment;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcSicConfigureIpeLumaChromaINTEL;

        public Node LumaIntraPartitionMask { get; set; }

        public Node IntraNeighbourAvailabilty { get; set; }

        public Node LeftEdgeLumaPixels { get; set; }

        public Node UpperLeftCornerLumaPixel { get; set; }

        public Node UpperEdgeLumaPixels { get; set; }

        public Node UpperRightEdgeLumaPixels { get; set; }

        public Node LeftEdgeChromaPixels { get; set; }

        public Node UpperLeftCornerChromaPixel { get; set; }

        public Node UpperEdgeChromaPixels { get; set; }

        public Node SadAdjustment { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return LumaIntraPartitionMask;
                yield return IntraNeighbourAvailabilty;
                yield return LeftEdgeLumaPixels;
                yield return UpperLeftCornerLumaPixel;
                yield return UpperEdgeLumaPixels;
                yield return UpperRightEdgeLumaPixels;
                yield return LeftEdgeChromaPixels;
                yield return UpperLeftCornerChromaPixel;
                yield return UpperEdgeChromaPixels;
                yield return SadAdjustment;
                yield return Payload;
        }

        public SubgroupAvcSicConfigureIpeLumaChromaINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcSicConfigureIpeLumaChromaINTEL)op, treeBuilder);
        }

        public SubgroupAvcSicConfigureIpeLumaChromaINTEL SetUp(Action<SubgroupAvcSicConfigureIpeLumaChromaINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcSicConfigureIpeLumaChromaINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            LumaIntraPartitionMask = treeBuilder.GetNode(op.LumaIntraPartitionMask);
            IntraNeighbourAvailabilty = treeBuilder.GetNode(op.IntraNeighbourAvailabilty);
            LeftEdgeLumaPixels = treeBuilder.GetNode(op.LeftEdgeLumaPixels);
            UpperLeftCornerLumaPixel = treeBuilder.GetNode(op.UpperLeftCornerLumaPixel);
            UpperEdgeLumaPixels = treeBuilder.GetNode(op.UpperEdgeLumaPixels);
            UpperRightEdgeLumaPixels = treeBuilder.GetNode(op.UpperRightEdgeLumaPixels);
            LeftEdgeChromaPixels = treeBuilder.GetNode(op.LeftEdgeChromaPixels);
            UpperLeftCornerChromaPixel = treeBuilder.GetNode(op.UpperLeftCornerChromaPixel);
            UpperEdgeChromaPixels = treeBuilder.GetNode(op.UpperEdgeChromaPixels);
            SadAdjustment = treeBuilder.GetNode(op.SadAdjustment);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcSicConfigureIpeLumaChromaINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcSicConfigureIpeLumaChromaINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcSicConfigureIpeLumaChromaINTEL({ResultType}, {LumaIntraPartitionMask}, {IntraNeighbourAvailabilty}, {LeftEdgeLumaPixels}, {UpperLeftCornerLumaPixel}, {UpperEdgeLumaPixels}, {UpperRightEdgeLumaPixels}, {LeftEdgeChromaPixels}, {UpperLeftCornerChromaPixel}, {UpperEdgeChromaPixels}, {SadAdjustment}, {Payload}, {DebugName})";
        }
    }
}