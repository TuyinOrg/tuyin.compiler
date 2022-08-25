using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcFmeInitializeINTEL : Node
    {
        public SubgroupAvcFmeInitializeINTEL()
        {
        }

        public SubgroupAvcFmeInitializeINTEL(SpirvTypeBase resultType, Node srcCoord, Node motionVectors, Node majorShapes, Node minorShapes, Node direction, Node pixelResolution, Node sadAdjustment, string debugName = null)
        {
            this.ResultType = resultType;
            this.SrcCoord = srcCoord;
            this.MotionVectors = motionVectors;
            this.MajorShapes = majorShapes;
            this.MinorShapes = minorShapes;
            this.Direction = direction;
            this.PixelResolution = pixelResolution;
            this.SadAdjustment = sadAdjustment;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcFmeInitializeINTEL;

        public Node SrcCoord { get; set; }

        public Node MotionVectors { get; set; }

        public Node MajorShapes { get; set; }

        public Node MinorShapes { get; set; }

        public Node Direction { get; set; }

        public Node PixelResolution { get; set; }

        public Node SadAdjustment { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return SrcCoord;
                yield return MotionVectors;
                yield return MajorShapes;
                yield return MinorShapes;
                yield return Direction;
                yield return PixelResolution;
                yield return SadAdjustment;
        }

        public SubgroupAvcFmeInitializeINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcFmeInitializeINTEL)op, treeBuilder);
        }

        public SubgroupAvcFmeInitializeINTEL SetUp(Action<SubgroupAvcFmeInitializeINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcFmeInitializeINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            SrcCoord = treeBuilder.GetNode(op.SrcCoord);
            MotionVectors = treeBuilder.GetNode(op.MotionVectors);
            MajorShapes = treeBuilder.GetNode(op.MajorShapes);
            MinorShapes = treeBuilder.GetNode(op.MinorShapes);
            Direction = treeBuilder.GetNode(op.Direction);
            PixelResolution = treeBuilder.GetNode(op.PixelResolution);
            SadAdjustment = treeBuilder.GetNode(op.SadAdjustment);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcFmeInitializeINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcFmeInitializeINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcFmeInitializeINTEL({ResultType}, {SrcCoord}, {MotionVectors}, {MajorShapes}, {MinorShapes}, {Direction}, {PixelResolution}, {SadAdjustment}, {DebugName})";
        }
    }
}