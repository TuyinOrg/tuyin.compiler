using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class TraceNV : ExecutableNode, INodeWithNext
    {
        public TraceNV()
        {
        }

        public TraceNV(Node accel, Node rayFlags, Node cullMask, Node sBTOffset, Node sBTStride, Node missIndex, Node rayOrigin, Node rayTmin, Node rayDirection, Node rayTmax, Node payloadId, string debugName = null)
        {
            this.Accel = accel;
            this.RayFlags = rayFlags;
            this.CullMask = cullMask;
            this.SBTOffset = sBTOffset;
            this.SBTStride = sBTStride;
            this.MissIndex = missIndex;
            this.RayOrigin = rayOrigin;
            this.RayTmin = rayTmin;
            this.RayDirection = rayDirection;
            this.RayTmax = rayTmax;
            this.PayloadId = payloadId;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpTraceNV;

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

        public Node Accel { get; set; }

        public Node RayFlags { get; set; }

        public Node CullMask { get; set; }

        public Node SBTOffset { get; set; }

        public Node SBTStride { get; set; }

        public Node MissIndex { get; set; }

        public Node RayOrigin { get; set; }

        public Node RayTmin { get; set; }

        public Node RayDirection { get; set; }

        public Node RayTmax { get; set; }

        public Node PayloadId { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Accel;
                yield return RayFlags;
                yield return CullMask;
                yield return SBTOffset;
                yield return SBTStride;
                yield return MissIndex;
                yield return RayOrigin;
                yield return RayTmin;
                yield return RayDirection;
                yield return RayTmax;
                yield return PayloadId;
        }

        public TraceNV WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpTraceNV)op, treeBuilder);
        }

        public TraceNV SetUp(Action<TraceNV> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpTraceNV op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Accel = treeBuilder.GetNode(op.Accel);
            RayFlags = treeBuilder.GetNode(op.RayFlags);
            CullMask = treeBuilder.GetNode(op.CullMask);
            SBTOffset = treeBuilder.GetNode(op.SBTOffset);
            SBTStride = treeBuilder.GetNode(op.SBTStride);
            MissIndex = treeBuilder.GetNode(op.MissIndex);
            RayOrigin = treeBuilder.GetNode(op.RayOrigin);
            RayTmin = treeBuilder.GetNode(op.RayTmin);
            RayDirection = treeBuilder.GetNode(op.RayDirection);
            RayTmax = treeBuilder.GetNode(op.RayTmax);
            PayloadId = treeBuilder.GetNode(op.PayloadId);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the TraceNV object.</summary>
        /// <returns>A string that represents the TraceNV object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"TraceNV({Accel}, {RayFlags}, {CullMask}, {SBTOffset}, {SBTStride}, {MissIndex}, {RayOrigin}, {RayTmin}, {RayDirection}, {RayTmax}, {PayloadId}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static TraceNV ThenTraceNV(this INodeWithNext node, Node accel, Node rayFlags, Node cullMask, Node sBTOffset, Node sBTStride, Node missIndex, Node rayOrigin, Node rayTmin, Node rayDirection, Node rayTmax, Node payloadId, string debugName = null)
        {
            return node.Then(new TraceNV(accel, rayFlags, cullMask, sBTOffset, sBTStride, missIndex, rayOrigin, rayTmin, rayDirection, rayTmax, payloadId, debugName));
        }
    }
}