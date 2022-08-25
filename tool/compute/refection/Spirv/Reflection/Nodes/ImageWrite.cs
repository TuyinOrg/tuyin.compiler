using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageWrite : ExecutableNode, INodeWithNext
    {
        public ImageWrite()
        {
        }

        public ImageWrite(Node image, Node coordinate, Node texel, Spv.ImageOperands imageOperands, string debugName = null)
        {
            this.Image = image;
            this.Coordinate = coordinate;
            this.Texel = texel;
            this.ImageOperands = imageOperands;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageWrite;

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

        public Node Texel { get; set; }

        public Spv.ImageOperands ImageOperands { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Image;
                yield return Coordinate;
                yield return Texel;
        }

        public ImageWrite WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageWrite)op, treeBuilder);
        }

        public ImageWrite SetUp(Action<ImageWrite> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageWrite op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Image = treeBuilder.GetNode(op.Image);
            Coordinate = treeBuilder.GetNode(op.Coordinate);
            Texel = treeBuilder.GetNode(op.Texel);
            ImageOperands = op.ImageOperands;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageWrite object.</summary>
        /// <returns>A string that represents the ImageWrite object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageWrite({Image}, {Coordinate}, {Texel}, {ImageOperands}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static ImageWrite ThenImageWrite(this INodeWithNext node, Node image, Node coordinate, Node texel, Spv.ImageOperands imageOperands, string debugName = null)
        {
            return node.Then(new ImageWrite(image, coordinate, texel, imageOperands, debugName));
        }
    }
}