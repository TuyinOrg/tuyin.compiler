using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageSampleProjDrefExplicitLod : Node
    {
        public ImageSampleProjDrefExplicitLod()
        {
        }

        public ImageSampleProjDrefExplicitLod(SpirvTypeBase resultType, Node sampledImage, Node coordinate, Node d_ref, Spv.ImageOperands imageOperands, string debugName = null)
        {
            this.ResultType = resultType;
            this.SampledImage = sampledImage;
            this.Coordinate = coordinate;
            this.D_ref = d_ref;
            this.ImageOperands = imageOperands;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageSampleProjDrefExplicitLod;

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

        public ImageSampleProjDrefExplicitLod WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageSampleProjDrefExplicitLod)op, treeBuilder);
        }

        public ImageSampleProjDrefExplicitLod SetUp(Action<ImageSampleProjDrefExplicitLod> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageSampleProjDrefExplicitLod op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SampledImage = treeBuilder.GetNode(op.SampledImage);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            D_ref = treeBuilder.GetNode(op.D_ref);
            ImageOperands = op.ImageOperands;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageSampleProjDrefExplicitLod object.</summary>
        /// <returns>A string that represents the ImageSampleProjDrefExplicitLod object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageSampleProjDrefExplicitLod({ResultType}, {SampledImage}, {Coordinate}, {D_ref}, {ImageOperands}, {DebugName})";
        }
    }
}