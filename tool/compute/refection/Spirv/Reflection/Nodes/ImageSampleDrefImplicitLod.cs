using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageSampleDrefImplicitLod : Node
    {
        public ImageSampleDrefImplicitLod()
        {
        }

        public ImageSampleDrefImplicitLod(SpirvTypeBase resultType, Node sampledImage, Node coordinate, Node d_ref, Spv.ImageOperands imageOperands, string debugName = null)
        {
            this.ResultType = resultType;
            this.SampledImage = sampledImage;
            this.Coordinate = coordinate;
            this.D_ref = d_ref;
            this.ImageOperands = imageOperands;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageSampleDrefImplicitLod;

        public Node SampledImage { get; set; }

        public Node Coordinate { get; set; }

        public Node D_ref { get; set; }

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
                yield return D_ref;
        }

        public ImageSampleDrefImplicitLod WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageSampleDrefImplicitLod)op, treeBuilder);
        }

        public ImageSampleDrefImplicitLod SetUp(Action<ImageSampleDrefImplicitLod> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageSampleDrefImplicitLod op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SampledImage = treeBuilder.GetNode(op.SampledImage);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            D_ref = treeBuilder.GetNode(op.D_ref);
            ImageOperands = op.ImageOperands;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageSampleDrefImplicitLod object.</summary>
        /// <returns>A string that represents the ImageSampleDrefImplicitLod object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageSampleDrefImplicitLod({ResultType}, {SampledImage}, {Coordinate}, {D_ref}, {ImageOperands}, {DebugName})";
        }
    }
}