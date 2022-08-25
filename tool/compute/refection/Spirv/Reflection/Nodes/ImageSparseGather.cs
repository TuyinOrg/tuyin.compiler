using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageSparseGather : Node
    {
        public ImageSparseGather()
        {
        }

        public ImageSparseGather(SpirvTypeBase resultType, Node sampledImage, Node coordinate, Node component, Spv.ImageOperands imageOperands, string debugName = null)
        {
            this.ResultType = resultType;
            this.SampledImage = sampledImage;
            this.Coordinate = coordinate;
            this.Component = component;
            this.ImageOperands = imageOperands;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageSparseGather;

        public Node SampledImage { get; set; }

        public Node Coordinate { get; set; }

        public Node Component { get; set; }

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
                yield return Component;
        }

        public ImageSparseGather WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageSparseGather)op, treeBuilder);
        }

        public ImageSparseGather SetUp(Action<ImageSparseGather> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageSparseGather op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SampledImage = treeBuilder.GetNode(op.SampledImage);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            Component = treeBuilder.GetNode(op.Component);
            ImageOperands = op.ImageOperands;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageSparseGather object.</summary>
        /// <returns>A string that represents the ImageSparseGather object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageSparseGather({ResultType}, {SampledImage}, {Coordinate}, {Component}, {ImageOperands}, {DebugName})";
        }
    }
}