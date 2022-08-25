using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageSparseRead : Node
    {
        public ImageSparseRead()
        {
        }

        public ImageSparseRead(SpirvTypeBase resultType, Node image, Node coordinate, Spv.ImageOperands imageOperands, string debugName = null)
        {
            this.ResultType = resultType;
            this.Image = image;
            this.Coordinate = coordinate;
            this.ImageOperands = imageOperands;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageSparseRead;

        public Node Image { get; set; }

        public Node Coordinate { get; set; }

        public Spv.ImageOperands ImageOperands { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Image;
                yield return Coordinate;
        }

        public ImageSparseRead WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageSparseRead)op, treeBuilder);
        }

        public ImageSparseRead SetUp(Action<ImageSparseRead> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageSparseRead op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Image = treeBuilder.GetNode(op.Image);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            ImageOperands = op.ImageOperands;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageSparseRead object.</summary>
        /// <returns>A string that represents the ImageSparseRead object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageSparseRead({ResultType}, {Image}, {Coordinate}, {ImageOperands}, {DebugName})";
        }
    }
}