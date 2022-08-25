using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class GenericCastToPtrExplicit : Node
    {
        public GenericCastToPtrExplicit()
        {
        }

        public GenericCastToPtrExplicit(SpirvTypeBase resultType, Node pointer, Spv.StorageClass storage, string debugName = null)
        {
            this.ResultType = resultType;
            this.Pointer = pointer;
            this.Storage = storage;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpGenericCastToPtrExplicit;

        public Node Pointer { get; set; }

        public Spv.StorageClass Storage { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return Pointer;
        }

        public GenericCastToPtrExplicit WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpGenericCastToPtrExplicit)op, treeBuilder);
        }

        public GenericCastToPtrExplicit SetUp(Action<GenericCastToPtrExplicit> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpGenericCastToPtrExplicit op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            Pointer = treeBuilder.GetNode(op.Pointer);
            Storage = op.Storage;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the GenericCastToPtrExplicit object.</summary>
        /// <returns>A string that represents the GenericCastToPtrExplicit object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"GenericCastToPtrExplicit({ResultType}, {Pointer}, {Storage}, {DebugName})";
        }
    }
}