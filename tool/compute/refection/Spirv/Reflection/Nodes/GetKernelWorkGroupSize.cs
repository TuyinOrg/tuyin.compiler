using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GetKernelWorkGroupSize : Node
    {
        public GetKernelWorkGroupSize()
        {
        }

        public GetKernelWorkGroupSize(SpirvTypeBase resultType, Node invoke, Node param, Node paramSize, Node paramAlign, string debugName = null)
        {
            this.ResultType = resultType;
            this.Invoke = invoke;
            this.Param = param;
            this.ParamSize = paramSize;
            this.ParamAlign = paramAlign;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGetKernelWorkGroupSize;

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

        public GetKernelWorkGroupSize WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGetKernelWorkGroupSize)op, treeBuilder);
        }

        public GetKernelWorkGroupSize SetUp(Action<GetKernelWorkGroupSize> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGetKernelWorkGroupSize op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Invoke = treeBuilder.GetNode(op.Invoke);
            Param = treeBuilder.GetNode(op.Param);
            ParamSize = treeBuilder.GetNode(op.ParamSize);
            ParamAlign = treeBuilder.GetNode(op.ParamAlign);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GetKernelWorkGroupSize object.</summary>
        /// <returns>A string that represents the GetKernelWorkGroupSize object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GetKernelWorkGroupSize({ResultType}, {Invoke}, {Param}, {ParamSize}, {ParamAlign}, {DebugName})";
        }
    }
}