using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupImageMediaBlockWriteINTEL : ExecutableNode, INodeWithNext
    {
        public SubgroupImageMediaBlockWriteINTEL()
        {
        }

        public SubgroupImageMediaBlockWriteINTEL(Node image, Node coordinate, Node width, Node height, Node data, string debugName = null)
        {
            this.Image = image;
            this.Coordinate = coordinate;
            this.Width = width;
            this.Height = height;
            this.Data = data;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupImageMediaBlockWriteINTEL;

        /// <summary>
        /// Next operation in sequence
        /// </summary>
        public ExecutableNode Next { get; set; }

        public override ExecutableNode GetNext()
        {
            return Next;
        }

        public T Then<T>(T node) where T: ExecutableNode
        {
            Next = node;
            return node;
        }

        public Node Image { get; set; }

        public Node Coordinate { get; set; }

        public Node Width { get; set; }

        public Node Height { get; set; }

        public Node Data { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Image;
                yield return Coordinate;
                yield return Width;
                yield return Height;
                yield return Data;
        }

        public SubgroupImageMediaBlockWriteINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupImageMediaBlockWriteINTEL)op, treeBuilder);
        }

        public SubgroupImageMediaBlockWriteINTEL SetUp(Action<SubgroupImageMediaBlockWriteINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupImageMediaBlockWriteINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Image = treeBuilder.GetNode(op.Image);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            Width = treeBuilder.GetNode(op.Width);
            Height = treeBuilder.GetNode(op.Height);
            Data = treeBuilder.GetNode(op.Data);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupImageMediaBlockWriteINTEL object.</summary>
        /// <returns>A string that represents the SubgroupImageMediaBlockWriteINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupImageMediaBlockWriteINTEL({Image}, {Coordinate}, {Width}, {Height}, {Data}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static SubgroupImageMediaBlockWriteINTEL ThenSubgroupImageMediaBlockWriteINTEL(this INodeWithNext node, Node image, Node coordinate, Node width, Node height, Node data, string debugName = null)
        {
            return node.Then(new SubgroupImageMediaBlockWriteINTEL(image, coordinate, width, height, data, debugName));
        }
    }
}