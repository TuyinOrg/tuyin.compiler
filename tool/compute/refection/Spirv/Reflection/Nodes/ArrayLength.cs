using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ArrayLength : Node
    {
        public ArrayLength()
        {
        }

        public ArrayLength(SpirvTypeBase resultType, Node structure, uint arraymember, string debugName = null)
        {
            this.ResultType = resultType;
            this.Structure = structure;
            this.Arraymember = arraymember;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpArrayLength;

        public Node Structure { get; set; }

        public uint Arraymember { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
            yield return Structure;
        }

        public ArrayLength WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpArrayLength)op, treeBuilder);
        }

        public ArrayLength SetUp(Action<ArrayLength> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpArrayLength op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Structure = treeBuilder.GetNode(op.Structure);
            Arraymember = op.Arraymember;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ArrayLength object.</summary>
        /// <returns>A string that represents the ArrayLength object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ArrayLength({ResultType}, {Structure}, {Arraymember}, {DebugName})";
        }
    }
}