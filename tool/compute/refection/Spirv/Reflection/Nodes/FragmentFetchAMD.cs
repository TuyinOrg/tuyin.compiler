using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class FragmentFetchAMD : Node
    {
        public FragmentFetchAMD()
        {
        }

        public FragmentFetchAMD(SpirvTypeBase resultType, Node image, Node coordinate, Node fragmentIndex, string debugName = null)
        {
            this.ResultType = resultType;
            this.Image = image;
            this.Coordinate = coordinate;
            this.FragmentIndex = fragmentIndex;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpFragmentFetchAMD;

        public Node Image { get; set; }

        public Node Coordinate { get; set; }

        public Node FragmentIndex { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Image;
                yield return Coordinate;
                yield return FragmentIndex;
        }

        public FragmentFetchAMD WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpFragmentFetchAMD)op, treeBuilder);
        }

        public FragmentFetchAMD SetUp(Action<FragmentFetchAMD> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpFragmentFetchAMD op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Image = treeBuilder.GetNode(op.Image);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            FragmentIndex = treeBuilder.GetNode(op.FragmentIndex);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the FragmentFetchAMD object.</summary>
        /// <returns>A string that represents the FragmentFetchAMD object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"FragmentFetchAMD({ResultType}, {Image}, {Coordinate}, {FragmentIndex}, {DebugName})";
        }
    }
}