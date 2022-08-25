using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcImeSetMaxMotionVectorCountINTEL : Node
    {
        public SubgroupAvcImeSetMaxMotionVectorCountINTEL()
        {
        }

        public SubgroupAvcImeSetMaxMotionVectorCountINTEL(SpirvTypeBase resultType, Node maxMotionVectorCount, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.MaxMotionVectorCount = maxMotionVectorCount;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcImeSetMaxMotionVectorCountINTEL;

        public Node MaxMotionVectorCount { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return MaxMotionVectorCount;
                yield return Payload;
        }

        public SubgroupAvcImeSetMaxMotionVectorCountINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcImeSetMaxMotionVectorCountINTEL)op, treeBuilder);
        }

        public SubgroupAvcImeSetMaxMotionVectorCountINTEL SetUp(Action<SubgroupAvcImeSetMaxMotionVectorCountINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcImeSetMaxMotionVectorCountINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            MaxMotionVectorCount = treeBuilder.GetNode(op.MaxMotionVectorCount);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcImeSetMaxMotionVectorCountINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcImeSetMaxMotionVectorCountINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcImeSetMaxMotionVectorCountINTEL({ResultType}, {MaxMotionVectorCount}, {Payload}, {DebugName})";
        }
    }
}