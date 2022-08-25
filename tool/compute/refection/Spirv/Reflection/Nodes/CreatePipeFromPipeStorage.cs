using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class CreatePipeFromPipeStorage : Node
    {
        public CreatePipeFromPipeStorage()
        {
        }

        public CreatePipeFromPipeStorage(SpirvTypeBase resultType, Node pipeStorage, string debugName = null)
        {
            this.ResultType = resultType;
            this.PipeStorage = pipeStorage;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpCreatePipeFromPipeStorage;

        public Node PipeStorage { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return PipeStorage;
        }

        public CreatePipeFromPipeStorage WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpCreatePipeFromPipeStorage)op, treeBuilder);
        }

        public CreatePipeFromPipeStorage SetUp(Action<CreatePipeFromPipeStorage> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpCreatePipeFromPipeStorage op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            PipeStorage = treeBuilder.GetNode(op.PipeStorage);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the CreatePipeFromPipeStorage object.</summary>
        /// <returns>A string that represents the CreatePipeFromPipeStorage object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"CreatePipeFromPipeStorage({ResultType}, {PipeStorage}, {DebugName})";
        }
    }
}