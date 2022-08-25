using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageQuerySize : Node
    {
        public ImageQuerySize()
        {
        }

        public ImageQuerySize(SpirvTypeBase resultType, Node image, string debugName = null)
        {
            this.ResultType = resultType;
            this.Image = image;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageQuerySize;

        public Node Image { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Image;
        }

        public ImageQuerySize WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageQuerySize)op, treeBuilder);
        }

        public ImageQuerySize SetUp(Action<ImageQuerySize> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageQuerySize op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Image = treeBuilder.GetNode(op.Image);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageQuerySize object.</summary>
        /// <returns>A string that represents the ImageQuerySize object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageQuerySize({ResultType}, {Image}, {DebugName})";
        }
    }
}