using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageSparseSampleProjExplicitLod : Node
    {
        public ImageSparseSampleProjExplicitLod()
        {
        }

        public ImageSparseSampleProjExplicitLod(SpirvTypeBase resultType, Node sampledImage, Node coordinate, Spv.ImageOperands imageOperands, string debugName = null)
        {
            this.ResultType = resultType;
            this.SampledImage = sampledImage;
            this.Coordinate = coordinate;
            this.ImageOperands = imageOperands;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageSparseSampleProjExplicitLod;

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

        public ImageSparseSampleProjExplicitLod WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageSparseSampleProjExplicitLod)op, treeBuilder);
        }

        public ImageSparseSampleProjExplicitLod SetUp(Action<ImageSparseSampleProjExplicitLod> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageSparseSampleProjExplicitLod op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SampledImage = treeBuilder.GetNode(op.SampledImage);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            ImageOperands = op.ImageOperands;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageSparseSampleProjExplicitLod object.</summary>
        /// <returns>A string that represents the ImageSparseSampleProjExplicitLod object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageSparseSampleProjExplicitLod({ResultType}, {SampledImage}, {Coordinate}, {ImageOperands}, {DebugName})";
        }
    }
}