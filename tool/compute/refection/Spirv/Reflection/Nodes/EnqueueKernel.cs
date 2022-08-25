using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class EnqueueKernel : Node
    {
        public EnqueueKernel()
        {
        }

        public EnqueueKernel(SpirvTypeBase resultType, Node queue, Node flags, Node nDRange, Node numEvents, Node waitEvents, Node retEvent, Node invoke, Node param, Node paramSize, Node paramAlign, IEnumerable<Node> localSize, string debugName = null)
        {
            this.ResultType = resultType;
            this.Queue = queue;
            this.Flags = flags;
            this.NDRange = nDRange;
            this.NumEvents = numEvents;
            this.WaitEvents = waitEvents;
            this.RetEvent = retEvent;
            this.Invoke = invoke;
            this.Param = param;
            this.ParamSize = paramSize;
            this.ParamAlign = paramAlign;
            if (localSize != null) { foreach (var node in localSize) this.LocalSize.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpEnqueueKernel;

        public Node Queue { get; set; }

        public Node Flags { get; set; }

        public Node NDRange { get; set; }

        public Node NumEvents { get; set; }

        public Node WaitEvents { get; set; }

        public Node RetEvent { get; set; }

        public Node Invoke { get; set; }

        public Node Param { get; set; }

        public Node ParamSize { get; set; }

        public Node ParamAlign { get; set; }

        public IList<Node> LocalSize { get; private set; } = new PrintableList<Node>();

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Queue;
                yield return Flags;
                yield return NDRange;
                yield return NumEvents;
                yield return WaitEvents;
                yield return RetEvent;
                yield return Invoke;
                yield return Param;
                yield return ParamSize;
                yield return ParamAlign;
                for (var index = 0; index < LocalSize.Count; index++)
                {
                    yield return LocalSize[index];
                }
        }

        public EnqueueKernel WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpEnqueueKernel)op, treeBuilder);
        }

        public EnqueueKernel SetUp(Action<EnqueueKernel> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpEnqueueKernel op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Queue = treeBuilder.GetNode(op.Queue);
            Flags = treeBuilder.GetNode(op.Flags);
            NDRange = treeBuilder.GetNode(op.NDRange);
            NumEvents = treeBuilder.GetNode(op.NumEvents);
            WaitEvents = treeBuilder.GetNode(op.WaitEvents);
            RetEvent = treeBuilder.GetNode(op.RetEvent);
            Invoke = treeBuilder.GetNode(op.Invoke);
            Param = treeBuilder.GetNode(op.Param);
            ParamSize = treeBuilder.GetNode(op.ParamSize);
            ParamAlign = treeBuilder.GetNode(op.ParamAlign);
            LocalSize = treeBuilder.GetNodes(op.LocalSize);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the EnqueueKernel object.</summary>
        /// <returns>A string that represents the EnqueueKernel object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"EnqueueKernel({ResultType}, {Queue}, {Flags}, {NDRange}, {NumEvents}, {WaitEvents}, {RetEvent}, {Invoke}, {Param}, {ParamSize}, {ParamAlign}, {LocalSize}, {DebugName})";
        }
    }
}