using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ReportIntersectionNV : Node
    {
        public ReportIntersectionNV()
        {
        }

        public ReportIntersectionNV(SpirvTypeBase resultType, Node hit, Node hitKind, string debugName = null)
        {
            this.ResultType = resultType;
            this.Hit = hit;
            this.HitKind = hitKind;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpReportIntersectionNV;

        public Node Hit { get; set; }

        public Node HitKind { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Hit;
                yield return HitKind;
        }

        public ReportIntersectionNV WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpReportIntersectionNV)op, treeBuilder);
        }

        public ReportIntersectionNV SetUp(Action<ReportIntersectionNV> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpReportIntersectionNV op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Hit = treeBuilder.GetNode(op.Hit);
            HitKind = treeBuilder.GetNode(op.HitKind);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ReportIntersectionNV object.</summary>
        /// <returns>A string that represents the ReportIntersectionNV object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ReportIntersectionNV({ResultType}, {Hit}, {HitKind}, {DebugName})";
        }
    }
}