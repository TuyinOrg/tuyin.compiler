using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class LoopMerge : ExecutableNode, INodeWithNext
    {
        public LoopMerge()
        {
        }

        public LoopMerge(Label mergeBlock, Label continueTarget, Spv.LoopControl loopControl, string debugName = null)
        {
            this.MergeBlock = mergeBlock;
            this.ContinueTarget = continueTarget;
            this.LoopControl = loopControl;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpLoopMerge;

        /// <summary>
        /// Next operation in sequence
        /// </summary>
        public ExecutableNode Next { get; set; }

        public override ExecutableNode GetNext()
        {
            return Next;
        }

        public T Then<T>(T node) where T: ExecutableNode
        {
            Next = node;
            return node;
        }

        public Label MergeBlock { get; set; }

        public Label ContinueTarget { get; set; }

        public Spv.LoopControl LoopControl { get; set; }

        public LoopMerge WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpLoopMerge)op, treeBuilder);
        }

        public LoopMerge SetUp(Action<LoopMerge> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpLoopMerge op, SpirvInstructionTreeBuilder treeBuilder)
        {
            MergeBlock = (Label)treeBuilder.GetNode(op.MergeBlock);
            ContinueTarget = (Label)treeBuilder.GetNode(op.ContinueTarget);
            LoopControl = op.LoopControl;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the LoopMerge object.</summary>
        /// <returns>A string that represents the LoopMerge object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"LoopMerge({MergeBlock}, {ContinueTarget}, {LoopControl}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static LoopMerge ThenLoopMerge(this INodeWithNext node, Label mergeBlock, Label continueTarget, Spv.LoopControl loopControl, string debugName = null)
        {
            return node.Then(new LoopMerge(mergeBlock, continueTarget, loopControl, debugName));
        }
    }
}