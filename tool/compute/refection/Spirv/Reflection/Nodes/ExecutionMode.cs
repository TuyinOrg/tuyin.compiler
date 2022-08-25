using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ExecutionMode : Node
    {
        public ExecutionMode()
        {
        }

        public ExecutionMode(Node entryPoint, Spv.ExecutionMode mode, string debugName = null)
        {
            this.EntryPoint = entryPoint;
            this.Mode = mode;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpExecutionMode;

        public Node EntryPoint { get; set; }

        public Spv.ExecutionMode Mode { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return EntryPoint;
        }

        public ExecutionMode WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpExecutionMode)op, treeBuilder);
        }

        public ExecutionMode SetUp(Action<ExecutionMode> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpExecutionMode op, SpirvInstructionTreeBuilder treeBuilder)
        {
            EntryPoint = treeBuilder.GetNode(op.EntryPoint);
            Mode = op.Mode;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ExecutionMode object.</summary>
        /// <returns>A string that represents the ExecutionMode object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ExecutionMode({EntryPoint}, {Mode}, {DebugName})";
        }
    }
}