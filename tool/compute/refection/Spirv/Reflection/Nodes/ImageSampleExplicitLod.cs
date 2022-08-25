using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageSampleExplicitLod : Node
    {
        public ImageSampleExplicitLod()
        {
        }

        public ImageSampleExplicitLod(SpirvTypeBase resultType, Node sampledImage, Node coordinate, Spv.ImageOperands imageOperands, string debugName = null)
        {
            this.ResultType = resultType;
            this.SampledImage = sampledImage;
            this.Coordinate = coordinate;
            this.ImageOperands = imageOperands;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageSampleExplicitLod;

        public Node SampledImage { get; set; }

        public Node Coordinate { get; set; }

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
        }

        public ImageSampleExplicitLod WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageSampleExplicitLod)op, treeBuilder);
        }

        public ImageSampleExplicitLod SetUp(Action<ImageSampleExplicitLod> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageSampleExplicitLod op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SampledImage = treeBuilder.GetNode(op.SampledImage);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            ImageOperands = op.ImageOperands;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageSampleExplicitLod object.</summary>
        /// <returns>A string that represents the ImageSampleExplicitLod object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageSampleExplicitLod({ResultType}, {SampledImage}, {Coordinate}, {ImageOperands}, {DebugName})";
        }
    }
}