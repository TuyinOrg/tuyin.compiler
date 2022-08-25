using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GroupDecorate : Node
    {
        public GroupDecorate()
        {
        }

        public GroupDecorate(Node decorationGroup, IEnumerable<Node> targets, string debugName = null)
        {
            this.DecorationGroup = decorationGroup;
            if (targets != null) { foreach (var node in targets) this.Targets.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGroupDecorate;

        public Node DecorationGroup { get; set; }

        public IList<Node> Targets { get; private set; } = new PrintableList<Node>();

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return DecorationGroup;
                for (var index = 0; index < Targets.Count; index++)
                {
                    yield return Targets[index];
                }
        }

        public GroupDecorate WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGroupDecorate)op, treeBuilder);
        }

        public GroupDecorate SetUp(Action<GroupDecorate> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGroupDecorate op, SpirvInstructionTreeBuilder treeBuilder)
        {
            DecorationGroup = treeBuilder.GetNode(op.DecorationGroup);
            Targets = treeBuilder.GetNodes(op.Targets);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GroupDecorate object.</summary>
        /// <returns>A string that represents the GroupDecorate object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GroupDecorate({DecorationGroup}, {Targets}, {DebugName})";
        }
    }
}