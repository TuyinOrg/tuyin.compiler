using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class EntryPoint : Node
    {
        public EntryPoint()
        {
        }

        public EntryPoint(Spv.ExecutionModel executionModel, Function value, string name, IEnumerable<Node> @interface, string debugName = null)
        {
            this.ExecutionModel = executionModel;
            this.Value = value;
            this.Name = name;
            if (@interface != null) { foreach (var node in @interface) this.Interface.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpEntryPoint;

        public Spv.ExecutionModel ExecutionModel { get; set; }

        public Function Value { get; set; }

        public string Name { get; set; }

        public IList<Node> Interface { get; private set; } = new PrintableList<Node>();

        public override IEnumerable<Node> GetInputNodes()
        {
                for (var index = 0; index < Interface.Count; index++)
                {
                    yield return Interface[index];
                }
        }

        public EntryPoint WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpEntryPoint)op, treeBuilder);
        }

        public EntryPoint SetUp(Action<EntryPoint> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpEntryPoint op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ExecutionModel = op.ExecutionModel;
            Value = (Function)treeBuilder.GetNode(op.Value);
            Name = op.Name;
            Interface = treeBuilder.GetNodes(op.Interface);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the EntryPoint object.</summary>
        /// <returns>A string that represents the EntryPoint object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"EntryPoint({ExecutionModel}, {Value}, {Name}, {Interface}, {DebugName})";
        }
    }
}