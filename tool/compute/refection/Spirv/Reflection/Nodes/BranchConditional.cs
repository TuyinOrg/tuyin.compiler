using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class BranchConditional : ExecutableNode
    {
        public BranchConditional()
        {
        }

        public BranchConditional(Node condition, Label trueLabel, Label falseLabel, IList<uint> branchweights, string debugName = null)
        {
            this.Condition = condition;
            this.TrueLabel = trueLabel;
            this.FalseLabel = falseLabel;
            if (branchweights != null) { foreach (var node in branchweights) this.Branchweights.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpBranchConditional;

        public Node Condition { get; set; }

        public Label TrueLabel { get; set; }

        public Label FalseLabel { get; set; }

        public IList<uint> Branchweights { get; private set; } = new List<uint>();

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Condition;
        }

        public BranchConditional WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpBranchConditional)op, treeBuilder);
        }

        public BranchConditional SetUp(Action<BranchConditional> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpBranchConditional op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Condition = treeBuilder.GetNode(op.Condition);
            TrueLabel = (Label)treeBuilder.GetNode(op.TrueLabel);
            FalseLabel = (Label)treeBuilder.GetNode(op.FalseLabel);
            foreach (var item in op.Branchweights) { Branchweights.Add(treeBuilder.Parse(item)); }
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the BranchConditional object.</summary>
        /// <returns>A string that represents the BranchConditional object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"BranchConditional({Condition}, {TrueLabel}, {FalseLabel}, {Branchweights}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static BranchConditional ThenBranchConditional(this INodeWithNext node, Node condition, Label trueLabel, Label falseLabel, IList<uint> branchweights, string debugName = null)
        {
            return node.Then(new BranchConditional(condition, trueLabel, falseLabel, branchweights, debugName));
        }
    }
}