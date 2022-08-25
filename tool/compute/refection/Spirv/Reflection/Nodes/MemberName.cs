using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class MemberName : Node
    {
        public MemberName()
        {
        }

        public MemberName(SpirvTypeBase type, uint member, string name, string debugName = null)
        {
            this.Type = type;
            this.Member = member;
            this.Name = name;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpMemberName;

        public SpirvTypeBase Type { get; set; }

        public uint Member { get; set; }

        public string Name { get; set; }

        public MemberName WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpMemberName)op, treeBuilder);
        }

        public MemberName SetUp(Action<MemberName> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpMemberName op, SpirvInstructionTreeBuilder treeBuilder)
        {
            Type = treeBuilder.ResolveType(op.Type);
            Member = op.Member;
            Name = op.Name;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the MemberName object.</summary>
        /// <returns>A string that represents the MemberName object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"MemberName({Type}, {Member}, {Name}, {DebugName})";
        }
    }
}