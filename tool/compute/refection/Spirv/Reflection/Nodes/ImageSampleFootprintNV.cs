using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageSampleFootprintNV : Node
    {
        public ImageSampleFootprintNV()
        {
        }

        public ImageSampleFootprintNV(SpirvTypeBase resultType, Node sampledImage, Node coordinate, Node granularity, Node coarse, Spv.ImageOperands imageOperands, string debugName = null)
        {
            this.ResultType = resultType;
            this.SampledImage = sampledImage;
            this.Coordinate = coordinate;
            this.Granularity = granularity;
            this.Coarse = coarse;
            this.ImageOperands = imageOperands;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageSampleFootprintNV;

        public Node SampledImage { get; set; }

        public Node Coordinate { get; set; }

        public Node Granularity { get; set; }

        public Node Coarse { get; set; }

        public Spv.ImageOperands ImageOperands { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SampledImage;
                yield return Coordinate;
                yield return Granularity;
                yield return Coarse;
        }

        public ImageSampleFootprintNV WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageSampleFootprintNV)op, treeBuilder);
        }

        public ImageSampleFootprintNV SetUp(Action<ImageSampleFootprintNV> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageSampleFootprintNV op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SampledImage = treeBuilder.GetNode(op.SampledImage);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            Granularity = treeBuilder.GetNode(op.Granularity);
            Coarse = treeBuilder.GetNode(op.Coarse);
            ImageOperands = op.ImageOperands;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageSampleFootprintNV object.</summary>
        /// <returns>A string that represents the ImageSampleFootprintNV object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageSampleFootprintNV({ResultType}, {SampledImage}, {Coordinate}, {Granularity}, {Coarse}, {ImageOperands}, {DebugName})";
        }
    }
}