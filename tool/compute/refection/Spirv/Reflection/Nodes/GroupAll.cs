using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupAll : Node
    {
        public GroupAll()
        {
        }

        public GroupAll(SpirvTypeBase resultType, uint execution, Node predicate, string debugName = null)
        {
            this.ResultType = resultType;
            this.Execution = execution;
            this.Predicate = predicate;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupAll;

        public uint Execution { get; set; }

        public Node Predicate { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Predicate;
        }

        public GroupAll WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupAll)op, treeBuilder);
        }

        public GroupAll SetUp(Action<GroupAll> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupAll op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Execution = op.Execution;
            Predicate = treeBuilder.GetNode(op.Predicate);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupAll object.</summary>
        /// <returns>A string that represents the GroupAll object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupAll({ResultType}, {Execution}, {Predicate}, {DebugName})";
        }
    }
}