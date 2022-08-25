using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class Phi : Node
    {
        public Phi()
        {
        }

        public Phi(SpirvTypeBase resultType, IList<Operands.PairNodeNode> variableParent, string debugName = null)
        {
            this.ResultType = resultType;
            if (variableParent != null) { foreach (var node in variableParent) this.VariableParent.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpPhi;

        public IList<Operands.PairNodeNode> VariableParent { get; private set; } = new List<Operands.PairNodeNode>();

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public Phi WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpPhi)op, treeBuilder);
        }

        public Phi SetUp(Action<Phi> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpPhi op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            foreach (var item in op.VariableParent) { VariableParent.Add(treeBuilder.Parse(item)); }
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the Phi object.</summary>
        /// <returns>A string that represents the Phi object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"Phi({ResultType}, {VariableParent}, {DebugName})";
        }
    }
}