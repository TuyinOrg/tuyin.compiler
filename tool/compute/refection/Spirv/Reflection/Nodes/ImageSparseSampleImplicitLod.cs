using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageSparseSampleImplicitLod : Node
    {
        public ImageSparseSampleImplicitLod()
        {
        }

        public ImageSparseSampleImplicitLod(SpirvTypeBase resultType, Node sampledImage, Node coordinate, Spv.ImageOperands imageOperands, string debugName = null)
        {
            this.ResultType = resultType;
            this.SampledImage = sampledImage;
            this.Coordinate = coordinate;
            this.ImageOperands = imageOperands;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageSparseSampleImplicitLod;

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

        public ImageSparseSampleImplicitLod WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageSparseSampleImplicitLod)op, treeBuilder);
        }

        public ImageSparseSampleImplicitLod SetUp(Action<ImageSparseSampleImplicitLod> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageSparseSampleImplicitLod op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SampledImage = treeBuilder.GetNode(op.SampledImage);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            ImageOperands = op.ImageOperands;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageSparseSampleImplicitLod object.</summary>
        /// <returns>A string that represents the ImageSparseSampleImplicitLod object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageSparseSampleImplicitLod({ResultType}, {SampledImage}, {Coordinate}, {ImageOperands}, {DebugName})";
        }
    }
}