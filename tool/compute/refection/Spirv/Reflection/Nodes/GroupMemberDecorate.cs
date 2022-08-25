using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupMemberDecorate : Node
    {
        public GroupMemberDecorate()
        {
        }

        public GroupMemberDecorate(Node decorationGroup, IList<Operands.PairNodeLiteralInteger> targets, string debugName = null)
        {
            this.DecorationGroup = decorationGroup;
            if (targets != null) { foreach (var node in targets) this.Targets.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupMemberDecorate;

        public Node DecorationGroup { get; set; }

        public IList<Operands.PairNodeLiteralInteger> Targets { get; private set; } = new List<Operands.PairNodeLiteralInteger>();

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return DecorationGroup;
        }

        public GroupMemberDecorate WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupMemberDecorate)op, treeBuilder);
        }

        public GroupMemberDecorate SetUp(Action<GroupMemberDecorate> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupMemberDecorate op, SpirvInstructionTreeBuilder treeBuilder)
        {
            DecorationGroup = treeBuilder.GetNode(op.DecorationGroup);
            foreach (var item in op.Targets) { Targets.Add(treeBuilder.Parse(item)); }
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupMemberDecorate object.</summary>
        /// <returns>A string that represents the GroupMemberDecorate object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupMemberDecorate({DecorationGroup}, {Targets}, {DebugName})";
        }
    }
}