using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageQueryLod : Node
    {
        public ImageQueryLod()
        {
        }

        public ImageQueryLod(SpirvTypeBase resultType, Node sampledImage, Node coordinate, string debugName = null)
        {
            this.ResultType = resultType;
            this.SampledImage = sampledImage;
            this.Coordinate = coordinate;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageQueryLod;

        public Node SampledImage { get; set; }

        public Node Coordinate { get; set; }

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

        public ImageQueryLod WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageQueryLod)op, treeBuilder);
        }

        public ImageQueryLod SetUp(Action<ImageQueryLod> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageQueryLod op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SampledImage = treeBuilder.GetNode(op.SampledImage);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageQueryLod object.</summary>
        /// <returns>A string that represents the ImageQueryLod object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageQueryLod({ResultType}, {SampledImage}, {Coordinate}, {DebugName})";
        }
    }
}