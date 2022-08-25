using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ExtInst : Node
    {
        public ExtInst()
        {
        }

        public ExtInst(SpirvTypeBase resultType, Node set, uint instruction, IEnumerable<Node> operands, string debugName = null)
        {
            this.ResultType = resultType;
            this.Set = set;
            this.Instruction = instruction;
            if (operands != null) { foreach (var node in operands) this.Operands.Add(node); }
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpExtInst;

        public Node Set { get; set; }

        public uint Instruction { get; set; }

        public IList<Node> Operands { get; private set; } = new PrintableList<Node>();

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Set;
                for (var index = 0; index < Operands.Count; index++)
                {
                    yield return Operands[index];
                }
        }

        public ExtInst WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpExtInst)op, treeBuilder);
        }

        public ExtInst SetUp(Action<ExtInst> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpExtInst op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Set = treeBuilder.GetNode(op.Set);
            Instruction = op.Instruction;
            Operands = treeBuilder.GetNodes(op.Operands);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ExtInst object.</summary>
        /// <returns>A string that represents the ExtInst object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ExtInst({ResultType}, {Set}, {Instruction}, {Operands}, {DebugName})";
        }
    }
}