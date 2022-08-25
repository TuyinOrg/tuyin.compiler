using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class FunctionCall : ExecutableNode, INodeWithNext
    {
        public FunctionCall()
        {
        }

        public FunctionCall(SpirvTypeBase resultType, Function function, IEnumerable<Node> arguments, string debugName = null)
        {
            this.ResultType = resultType;
            this.Function = function;
            if (arguments != null) { foreach (var node in arguments) this.Arguments.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpFunctionCall;

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

        public Function Function { get; set; }

        public IList<Node> Arguments { get; private set; } = new PrintableList<Node>();

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Function;
                for (var index = 0; index < Arguments.Count; index++)
                {
                    yield return Arguments[index];
                }
        }

        public FunctionCall WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpFunctionCall)op, treeBuilder);
        }

        public FunctionCall SetUp(Action<FunctionCall> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpFunctionCall op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Function = (Function)treeBuilder.GetNode(op.Function);
            Arguments = treeBuilder.GetNodes(op.Arguments);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the FunctionCall object.</summary>
        /// <returns>A string that represents the FunctionCall object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"FunctionCall({ResultType}, {Function}, {Arguments}, {DebugName})";
        }
    }

    internal static partial class INodeWithNextExtensionMethods
    {
        internal static FunctionCall ThenFunctionCall(this INodeWithNext node, Function function, IEnumerable<Node> arguments, string debugName = null)
        {
            return node.Then(new FunctionCall(function, arguments, debugName));
        }
    }
}