using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupImageMediaBlockReadINTEL : Node
    {
        public SubgroupImageMediaBlockReadINTEL()
        {
        }

        public SubgroupImageMediaBlockReadINTEL(SpirvTypeBase resultType, Node image, Node coordinate, Node width, Node height, string debugName = null)
        {
            this.ResultType = resultType;
            this.Image = image;
            this.Coordinate = coordinate;
            this.Width = width;
            this.Height = height;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupImageMediaBlockReadINTEL;

        public Node Image { get; set; }

        public Node Coordinate { get; set; }

        public Node Width { get; set; }

        public Node Height { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Image;
                yield return Coordinate;
                yield return Width;
                yield return Height;
        }

        public SubgroupImageMediaBlockReadINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupImageMediaBlockReadINTEL)op, treeBuilder);
        }

        public SubgroupImageMediaBlockReadINTEL SetUp(Action<SubgroupImageMediaBlockReadINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupImageMediaBlockReadINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Image = treeBuilder.GetNode(op.Image);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            Width = treeBuilder.GetNode(op.Width);
            Height = treeBuilder.GetNode(op.Height);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupImageMediaBlockReadINTEL object.</summary>
        /// <returns>A string that represents the SubgroupImageMediaBlockReadINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupImageMediaBlockReadINTEL({ResultType}, {Image}, {Coordinate}, {Width}, {Height}, {DebugName})";
        }
    }
}