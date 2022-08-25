using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcMceGetInterMotionVectorCountINTEL : Node
    {
        public SubgroupAvcMceGetInterMotionVectorCountINTEL()
        {
        }

        public SubgroupAvcMceGetInterMotionVectorCountINTEL(SpirvTypeBase resultType, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcMceGetInterMotionVectorCountINTEL;

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Payload;
        }

        public SubgroupAvcMceGetInterMotionVectorCountINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcMceGetInterMotionVectorCountINTEL)op, treeBuilder);
        }

        public SubgroupAvcMceGetInterMotionVectorCountINTEL SetUp(Action<SubgroupAvcMceGetInterMotionVectorCountINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcMceGetInterMotionVectorCountINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcMceGetInterMotionVectorCountINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcMceGetInterMotionVectorCountINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcMceGetInterMotionVectorCountINTEL({ResultType}, {Payload}, {DebugName})";
        }
    }
}