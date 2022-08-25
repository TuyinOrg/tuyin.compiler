using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Nodes;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeStruct : SpirvTypeBase
    {
        private readonly List<TypeStructureField> _fields = new List<TypeStructureField>();
        private uint? _sizeInBytes;
        private uint? _alignment;

        internal TypeStruct()
        {
        }

        public TypeStruct(string name, params TypeStructureField[] fields)
        {
            DebugName = name;
            _fields = fields.ToList();
            //if (_fields.Any(_ => _.ByteOffset == null)) UpdateFieldOffsets();
            //EvaluateSizeAndAlignment();
        }

        public IEnumerable<SpirvTypeBase> MemberTypes
        {
            get
            {
                foreach (var field in _fields)
                {
                    yield return field.Type;
                }
            }
        }

        public override uint Alignment
        {
            get
            {
                if (!_alignment.HasValue)
                    throw new InvalidOperationException("Please execute EvaluateLayout() to calculate property value");
                return _alignment.Value;
            }
        }
        //public void EvaluateLayout()
        //{
        //    UpdateFieldOffsets();
        //    EvaluateSizeAndAlignment();
        //}

        public override uint SizeInBytes
        {
            get
            {
                if (!_alignment.HasValue)
                    throw new InvalidOperationException("Please execute EvaluateLayout() to calculate property value");
                return _sizeInBytes.Value;
            }
        }

        public IReadOnlyList<TypeStructureField> Fields => _fields;

        public override string ToString()
        {
            return DebugName ?? base.ToString();
        }

        private void UpdateFieldOffsets()
        {
            uint offset = 0;
            foreach (var field in _fields)
            {
                var alignment = field.Type.Alignment;
                var pos = SpirvUtils.RoundUp(offset, alignment);
                field.ByteOffset = pos;
                offset = pos + field.Type.SizeInBytes;
            }
        }

        private void EvaluateSizeAndAlignment()
        {
            if (Fields.Count == 0)
            {
                _sizeInBytes = 0;
                _alignment = 0;
                return;
            }

            uint size = 0;
            uint maxAlignment = 16;
            foreach (var field in _fields)
            {
                var alignment = field.Type.Alignment;
                if (maxAlignment < alignment)
                    maxAlignment = alignment;
                var end = field.ByteOffset.Value + field.Type.SizeInBytes;
                if (end > size)
                    size = end;
            }

            _alignment = maxAlignment;
            _sizeInBytes = SpirvUtils.RoundUp(size, maxAlignment);
        }

        partial void SetUp(OpTypeStruct instruction, SpirvInstructionTreeBuilder treeBuilder)
        {
            _fields.Clear();
            for (var index = 0; index < instruction.MemberTypes.Count; index++)
            {
                _fields.Add(new TypeStructureField() {Type = treeBuilder.ResolveType(instruction.MemberTypes[index])});
            }
            SetUpDecorations(instruction, treeBuilder);
            if (_fields.Any(_ => _.ByteOffset == null)) UpdateFieldOffsets();
            EvaluateSizeAndAlignment();
        }

        public override void SetUpDecorations(Instruction op, SpirvInstructionTreeBuilder treeBuilder)
        {
            DebugName = treeBuilder.GetDebugName(op);
            foreach (var instruction in treeBuilder.GetDecorations(op))
            {
                switch (instruction.OpCode)
                {
                    case Op.OpDecorate:
                        SetUpDecoration(((OpDecorate)instruction).Decoration, treeBuilder);
                        break;
                    case Op.OpMemberDecorate:
                        var opMemberDecorate = (OpMemberDecorate)instruction;
                        SetUpMemberDecoration(opMemberDecorate.Member, opMemberDecorate.Decoration, treeBuilder);
                        break;
                    case Op.OpMemberName:
                        var opMemberName = ((OpMemberName)instruction);
                        Fields[(int) opMemberName.Member].Name = opMemberName.Name;
                        break;
                    default:
                        throw new NotImplementedException("Decoration instruction " + instruction.OpCode + " not yet implemented by " + this.GetType().Name + " class.");
                }
            }
        }

        protected override void AddDecoration(Decoration decoration)
        {
            switch (decoration.Value)
            {
                case Decoration.Enumerant.Block:
                    Block = true;
                    return;
            }
            base.AddDecoration(decoration);
        }

        public TypeStruct WithDecoration(Decoration decoration)
        {
            AddDecoration(decoration);
            return this;
        }

        public bool Block { get; set; }

        private void SetUpMemberDecoration(uint member, Decoration decoration, SpirvInstructionTreeBuilder treeBuilder)
        {
            var field = Fields[(int) member];
            field.SetUpDecoration(decoration, treeBuilder);
        }

        public override IEnumerable<Node> BuildDecorations()
        {
            if (Block) yield return new Decorate(this, Decoration.Block());

            foreach (var decoration in base.BuildDecorations())
            {
                yield return decoration;
            }

            for (var index = 0; index < _fields.Count; index++)
            {
                var field = _fields[index];
                foreach (var decoration in field.BuildDecorations(this, (uint)index))
                {
                    yield return decoration;
                }
            }
        }
    }
}