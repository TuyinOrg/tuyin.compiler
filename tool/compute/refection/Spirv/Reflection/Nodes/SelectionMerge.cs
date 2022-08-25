using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class SelectionMerge : ExecutableNode, INodeWithNext
    {
        public SelectionMerge()
        {
        }

        public SelectionMerge(Label mergeBlock, Spv.SelectionControl selectionControl, string debugName = null)
        {
            this.MergeBlock = mergeBlock;
            this.SelectionControl = selectionControl;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpSelectionMerge;

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

        public Spv.SelectionControl SelectionControl { get; set; }

        public SelectionMerge WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpSelectionMerge)op, treeBuilder);
        }

        public SelectionMerge SetUp(Action<SelectionMerge> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpSelectionMerge op, SpirvInstructionTreeBuilder treeBuilder)
        {
            MergeBlock = (Label)treeBuilder.GetNode(op.MergeBlock);
            SelectionControl = op.SelectionControl;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the SelectionMerge object.</summary>
        /// <returns>A string that represents the SelectionMerge object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"SelectionMerge({MergeBlock}, {SelectionControl}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static SelectionMerge ThenSelectionMerge(this INodeWithNext node, Label mergeBlock, Spv.SelectionControl selectionControl, string debugName = null)
        {
            return node.Then(new SelectionMerge(mergeBlock, selectionControl, debugName));
        }
    }
}