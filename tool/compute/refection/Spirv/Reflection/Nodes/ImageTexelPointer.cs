using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageTexelPointer : Node
    {
        public ImageTexelPointer()
        {
        }

        public ImageTexelPointer(SpirvTypeBase resultType, Node image, Node coordinate, Node sample, string debugName = null)
        {
            this.ResultType = resultType;
            this.Image = image;
            this.Coordinate = coordinate;
            this.Sample = sample;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageTexelPointer;

        public Node Image { get; set; }

        public Node Coordinate { get; set; }

        public Node Sample { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Image;
                yield return Coordinate;
                yield return Sample;
        }

        public ImageTexelPointer WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageTexelPointer)op, treeBuilder);
        }

        public ImageTexelPointer SetUp(Action<ImageTexelPointer> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageTexelPointer op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Image = treeBuilder.GetNode(op.Image);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            Sample = treeBuilder.GetNode(op.Sample);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageTexelPointer object.</summary>
        /// <returns>A string that represents the ImageTexelPointer object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageTexelPointer({ResultType}, {Image}, {Coordinate}, {Sample}, {DebugName})";
        }
    }
}