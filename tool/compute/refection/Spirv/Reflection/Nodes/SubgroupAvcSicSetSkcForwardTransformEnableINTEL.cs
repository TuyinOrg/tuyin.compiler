using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SubgroupAvcSicSetSkcForwardTransformEnableINTEL : Node
    {
        public SubgroupAvcSicSetSkcForwardTransformEnableINTEL()
        {
        }

        public SubgroupAvcSicSetSkcForwardTransformEnableINTEL(SpirvTypeBase resultType, Node packedSadCoefficients, Node payload, string debugName = null)
        {
            this.ResultType = resultType;
            this.PackedSadCoefficients = packedSadCoefficients;
            this.Payload = payload;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSubgroupAvcSicSetSkcForwardTransformEnableINTEL;

        public Node PackedSadCoefficients { get; set; }

        public Node Payload { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return PackedSadCoefficients;
                yield return Payload;
        }

        public SubgroupAvcSicSetSkcForwardTransformEnableINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSubgroupAvcSicSetSkcForwardTransformEnableINTEL)op, treeBuilder);
        }

        public SubgroupAvcSicSetSkcForwardTransformEnableINTEL SetUp(Action<SubgroupAvcSicSetSkcForwardTransformEnableINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSubgroupAvcSicSetSkcForwardTransformEnableINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            PackedSadCoefficients = treeBuilder.GetNode(op.PackedSadCoefficients);
            Payload = treeBuilder.GetNode(op.Payload);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SubgroupAvcSicSetSkcForwardTransformEnableINTEL object.</summary>
        /// <returns>A string that represents the SubgroupAvcSicSetSkcForwardTransformEnableINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SubgroupAvcSicSetSkcForwardTransformEnableINTEL({ResultType}, {PackedSadCoefficients}, {Payload}, {DebugName})";
        }
    }
}