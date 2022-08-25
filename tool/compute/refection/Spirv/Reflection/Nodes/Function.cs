using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Function : Node, INodeWithNext
    {
        public Function()
        {
        }

        public Function(SpirvTypeBase resultType, Spv.FunctionControl functionControl, SpirvTypeBase functionType, string debugName = null)
        {
            this.ResultType = resultType;
            this.FunctionControl = functionControl;
            this.FunctionType = functionType;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpFunction;

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

        public Spv.FunctionControl FunctionControl { get; set; }

        public SpirvTypeBase FunctionType { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public Function WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpFunction)op, treeBuilder);
        }

        public Function SetUp(Action<Function> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpFunction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            FunctionControl = op.FunctionControl;
            FunctionType = treeBuilder.ResolveType(op.FunctionType);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Function object.</summary>
        /// <returns>A string that represents the Function object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Function({ResultType}, {FunctionControl}, {FunctionType}, {DebugName})";
        }
    }
}