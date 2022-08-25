using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ModuleProcessed : ExecutableNode, INodeWithNext
    {
        public ModuleProcessed()
        {
        }

        public ModuleProcessed(string process, string debugName = null)
        {
            this.Process = process;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpModuleProcessed;

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

        public string Process { get; set; }

        public ModuleProcessed WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpModuleProcessed)op, treeBuilder);
        }

        public ModuleProcessed SetUp(Action<ModuleProcessed> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpModuleProcessed op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Process = op.Process;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ModuleProcessed object.</summary>
        /// <returns>A string that represents the ModuleProcessed object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ModuleProcessed({Process}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static ModuleProcessed ThenModuleProcessed(this INodeWithNext node, string process, string debugName = null)
        {
            return node.Then(new ModuleProcessed(process, debugName));
        }
    }
}