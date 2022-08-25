using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageSparseSampleExplicitLod : Node
    {
        public ImageSparseSampleExplicitLod()
        {
        }

        public ImageSparseSampleExplicitLod(SpirvTypeBase resultType, Node sampledImage, Node coordinate, Spv.ImageOperands imageOperands, string debugName = null)
        {
            this.ResultType = resultType;
            this.SampledImage = sampledImage;
            this.Coordinate = coordinate;
            this.ImageOperands = imageOperands;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageSparseSampleExplicitLod;

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

        public ImageSparseSampleExplicitLod WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageSparseSampleExplicitLod)op, treeBuilder);
        }

        public ImageSparseSampleExplicitLod SetUp(Action<ImageSparseSampleExplicitLod> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageSparseSampleExplicitLod op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SampledImage = treeBuilder.GetNode(op.SampledImage);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            ImageOperands = op.ImageOperands;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageSparseSampleExplicitLod object.</summary>
        /// <returns>A string that represents the ImageSparseSampleExplicitLod object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageSparseSampleExplicitLod({ResultType}, {SampledImage}, {Coordinate}, {ImageOperands}, {DebugName})";
        }
    }
}