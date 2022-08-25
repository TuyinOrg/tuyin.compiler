using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcSicSetBlockBasedRawSkipSadINTEL : Node
    {
        public SubgroupAvcSicSetBlockBasedRawSkipSadINTEL()
        {
        }

        public SubgroupAvcSicSetBlockBasedRawSkipSadINTEL(SpirvTypeBase resultType, Node blockBasedSkipType, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.BlockBasedSkipType = blockBasedSkipType;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcSicSetBlockBasedRawSkipSadINTEL;

        public Node BlockBasedSkipType { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return BlockBasedSkipType;
                yield return Payload;
        }

        public SubgroupAvcSicSetBlockBasedRawSkipSadINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcSicSetBlockBasedRawSkipSadINTEL)op, treeBuilder);
        }

        public SubgroupAvcSicSetBlockBasedRawSkipSadINTEL SetUp(Action<SubgroupAvcSicSetBlockBasedRawSkipSadINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcSicSetBlockBasedRawSkipSadINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            BlockBasedSkipType = treeBuilder.GetNode(op.BlockBasedSkipType);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcSicSetBlockBasedRawSkipSadINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcSicSetBlockBasedRawSkipSadINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcSicSetBlockBasedRawSkipSadINTEL({ResultType}, {BlockBasedSkipType}, {Payload}, {DebugName})";
        }
    }
}