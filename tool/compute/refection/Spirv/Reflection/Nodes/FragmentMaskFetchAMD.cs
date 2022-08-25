using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class FragmentMaskFetchAMD : Node
    {
        public FragmentMaskFetchAMD()
        {
        }

        public FragmentMaskFetchAMD(SpirvTypeBase resultType, Node image, Node coordinate, string debugName = null)
        {
            this.ResultType = resultType;
            this.Image = image;
            this.Coordinate = coordinate;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpFragmentMaskFetchAMD;

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

        public FragmentMaskFetchAMD WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpFragmentMaskFetchAMD)op, treeBuilder);
        }

        public FragmentMaskFetchAMD SetUp(Action<FragmentMaskFetchAMD> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpFragmentMaskFetchAMD op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Image = treeBuilder.GetNode(op.Image);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the FragmentMaskFetchAMD object.</summary>
        /// <returns>A string that represents the FragmentMaskFetchAMD object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"FragmentMaskFetchAMD({ResultType}, {Image}, {Coordinate}, {DebugName})";
        }
    }
}