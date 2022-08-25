using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class MemberDecorateString : Node
    {
        public MemberDecorateString()
        {
        }

        public MemberDecorateString(Node structType, uint member, Spv.Decoration decoration, string debugName = null)
        {
            this.StructType = structType;
            this.Member = member;
            this.Decoration = decoration;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpMemberDecorateString;

        public Node StructType { get; set; }

        public uint Member { get; set; }

        public Spv.Decoration Decoration { get; set; }

        public override IEnumerable<Node> GetInputNodes()
        {
                yield return StructType;
        }

        public MemberDecorateString WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpMemberDecorateString)op, treeBuilder);
        }

        public MemberDecorateString SetUp(Action<MemberDecorateString> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpMemberDecorateString op, SpirvInstructionTreeBuilder treeBuilder)
        {
            StructType = treeBuilder.GetNode(op.StructType);
            Member = op.Member;
            Decoration = op.Decoration;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the MemberDecorateString object.</summary>
        /// <returns>A string that represents the MemberDecorateString object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"MemberDecorateString({StructType}, {Member}, {Decoration}, {DebugName})";
        }
    }
}