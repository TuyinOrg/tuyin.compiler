using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class ImageSparseTexelsResident : Node
    {
        public ImageSparseTexelsResident()
        {
        }

        public ImageSparseTexelsResident(SpirvTypeBase resultType, Node residentCode, string debugName = null)
        {
            this.ResultType = resultType;
            this.ResidentCode = residentCode;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpImageSparseTexelsResident;

        public Node ResidentCode { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return ResidentCode;
        }

        public ImageSparseTexelsResident WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpImageSparseTexelsResident)op, treeBuilder);
        }

        public ImageSparseTexelsResident SetUp(Action<ImageSparseTexelsResident> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpImageSparseTexelsResident op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            ResidentCode = treeBuilder.GetNode(op.ResidentCode);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the ImageSparseTexelsResident object.</summary>
        /// <returns>A string that represents the ImageSparseTexelsResident object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"ImageSparseTexelsResident({ResultType}, {ResidentCode}, {DebugName})";
        }
    }
}