using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GetKernelPreferredWorkGroupSizeMultiple : Node
    {
        public GetKernelPreferredWorkGroupSizeMultiple()
        {
        }

        public GetKernelPreferredWorkGroupSizeMultiple(SpirvTypeBase resultType, Node invoke, Node param, Node paramSize, Node paramAlign, string debugName = null)
        {
            this.ResultType = resultType;
            this.Invoke = invoke;
            this.Param = param;
            this.ParamSize = paramSize;
            this.ParamAlign = paramAlign;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGetKernelPreferredWorkGroupSizeMultiple;

        public Node Invoke { get; set; }

        public Node Param { get; set; }

        public Node ParamSize { get; set; }

        public Node ParamAlign { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Invoke;
                yield return Param;
                yield return ParamSize;
                yield return ParamAlign;
        }

        public GetKernelPreferredWorkGroupSizeMultiple WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGetKernelPreferredWorkGroupSizeMultiple)op, treeBuilder);
        }

        public GetKernelPreferredWorkGroupSizeMultiple SetUp(Action<GetKernelPreferredWorkGroupSizeMultiple> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGetKernelPreferredWorkGroupSizeMultiple op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Invoke = treeBuilder.GetNode(op.Invoke);
            Param = treeBuilder.GetNode(op.Param);
            ParamSize = treeBuilder.GetNode(op.ParamSize);
            ParamAlign = treeBuilder.GetNode(op.ParamAlign);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GetKernelPreferredWorkGroupSizeMultiple object.</summary>
        /// <returns>A string that represents the GetKernelPreferredWorkGroupSizeMultiple object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GetKernelPreferredWorkGroupSizeMultiple({ResultType}, {Invoke}, {Param}, {ParamSize}, {ParamAlign}, {DebugName})";
        }
    }
}