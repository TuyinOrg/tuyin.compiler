using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Switch : ExecutableNode
    {
        public Switch()
        {
        }

        public Switch(Node selector, Node @default, IList<Operands.PairLiteralIntegerNode> target, string debugName = null)
        {
            this.Selector = selector;
            this.Default = @default;
            if (target != null) { foreach (var node in target) this.Target.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSwitch;

        public Node Selector { get; set; }

        public Node Default { get; set; }

        public IList<Operands.PairLiteralIntegerNode> Target { get; private set; } = new List<Operands.PairLiteralIntegerNode>();

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Selector;
        }

        public Switch WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSwitch)op, treeBuilder);
        }

        public Switch SetUp(Action<Switch> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSwitch op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Selector = treeBuilder.GetNode(op.Selector);
            Default = treeBuilder.GetNode(op.Default);
            foreach (var item in op.Target) { Target.Add(treeBuilder.Parse(item)); }
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Switch object.</summary>
        /// <returns>A string that represents the Switch object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Switch({Selector}, {Default}, {Target}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static Switch ThenSwitch(this INodeWithNext node, Node selector, Node @default, IList<Operands.PairLiteralIntegerNode> target, string debugName = null)
        {
            return node.Then(new Switch(selector, @default, target, debugName));
        }
    }
}