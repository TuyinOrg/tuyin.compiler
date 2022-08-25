using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class VmeImageINTEL : Node
    {
        public VmeImageINTEL()
        {
        }

        public VmeImageINTEL(SpirvTypeBase resultType, Node imageType, Node sampler, string debugName = null)
        {
            this.ResultType = resultType;
            this.ImageType = imageType;
            this.Sampler = sampler;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpVmeImageINTEL;

        public Node ImageType { get; set; }

        public Node Sampler { get; set; }

        public SpirvTypeBase ResultType { get; set; }

        public override SpirvTypeBase GetResultType()
        {
            return ResultType;
        }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return ImageType;
                yield return Sampler;
        }

        public VmeImageINTEL WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpVmeImageINTEL)op, treeBuilder);
        }

        public VmeImageINTEL SetUp(Action<VmeImageINTEL> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpVmeImageINTEL op, SpirvInstructionTreeBuilder treeBuilder)
        {
            ResultType = treeBuilder.ResolveType(op.IdResultType);
            ImageType = treeBuilder.GetNode(op.ImageType);
            Sampler = treeBuilder.GetNode(op.Sampler);
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the VmeImageINTEL object.</summary>
        /// <returns>A string that represents the VmeImageINTEL object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"VmeImageINTEL({ResultType}, {ImageType}, {Sampler}, {DebugName})";
        }
    }
}