using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupImageBlockReadINTEL : Node
    {
        public SubgroupImageBlockReadINTEL()
        {
        }

        public SubgroupImageBlockReadINTEL(SpirvTypeBase resultType, Node image, Node coordinate, string debugName = null)
        {
            this.ResultType = resultType;
            this.Image = image;
            this.Coordinate = coordinate;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupImageBlockReadINTEL;

        public Node Image { get; set; }

        public Node Coordinate { get; set; }

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

        public SubgroupImageBlockReadINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupImageBlockReadINTEL)op, treeBuilder);
        }

        public SubgroupImageBlockReadINTEL SetUp(Action<SubgroupImageBlockReadINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupImageBlockReadINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Image = treeBuilder.GetNode(op.Image);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupImageBlockReadINTEL object.</summary>
        /// <returns>A string that represents the SubgroupImageBlockReadINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupImageBlockReadINTEL({ResultType}, {Image}, {Coordinate}, {DebugName})";
        }
    }
}