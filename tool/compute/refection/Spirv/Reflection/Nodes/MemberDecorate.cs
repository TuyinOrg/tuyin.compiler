using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Types;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Nodes
{
    internal partial class MemberDecorate : Node
    {
        public MemberDecorate()
        {
        }

        public MemberDecorate(SpirvTypeBase structureType, uint member, Spv.Decoration decoration, string debugName = null)
        {
            this.StructureType = structureType;
            this.Member = member;
            this.Decoration = decoration;
            DebugName = debugName;
        }

        public override Op OpCode => Op.OpMemberDecorate;

        public SpirvTypeBase StructureType { get; set; }

        public uint Member { get; set; }

        public Spv.Decoration Decoration { get; set; }

        public MemberDecorate WithDecoration(Spv.Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public override void SetUp(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            base.SetUp(op, treeBuilder);
            SetUp((OpMemberDecorate)op, treeBuilder);
        }

        public MemberDecorate SetUp(Action<MemberDecorate> setup)
        {
            setup(this);
            return this;
        }

        private void SetUp(OpMemberDecorate op, SpirvInstructionTreeBuilder treeBuilder)
        {
            StructureType = treeBuilder.ResolveType(op.StructureType);
            Member = op.Member;
            Decoration = op.Decoration;
            SetUpDecorations(op, treeBuilder);
        }

        /// <summary>Returns a string that represents the MemberDecorate object.</summary>
        /// <returns>A string that represents the MemberDecorate object.</returns>
        /// <filterpriority>2</filterpriority>
        public override string ToString()
        {
            return $"MemberDecorate({StructureType}, {Member}, {Decoration}, {DebugName})";
        }
    }
}