using System;
using System.Collections.Generic;
using System.Linq;
using Toe.SPIRV.Instructions;
using Toe.SPIRV.Reflection.Nodes;
using Toe.SPIRV.Reflection.Operands;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeArray : SpirvTypeBase, IEquatable<TypeArray>
    {
        private uint? _arrayStride;

        public TypeArray()
        {
        }
        public TypeArray(SpirvTypeBase elementType, uint length)
        {
            ElementType = elementType;
            Length = length;
        }

        public uint ArrayStride
        {
            get => _arrayStride ?? SpirvUtils.RoundUp(ElementType.SizeInBytes, 16);
            set => _arrayStride = value;
        }

        public bool HasExplicitArrayStride
        {
            get { return _arrayStride.HasValue; }
        }

        public NumberLiteral Length { get; set; }

        public SpirvTypeBase ElementType { get; set; }

        public override uint SizeInBytes => Alignment * Length;

        public override uint Alignment => SpirvUtils.RoundUp(ElementType.Alignment, 16);

        public static bool operator ==(TypeArray left, TypeArray right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TypeArray left, TypeArray right)
        {
            return !Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is TypeArray other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = ElementType.GetHashCode();
            hashCode = (hashCode * 397) ^ Length.GetHashCode();
            if (_arrayStride != null)
            hashCode = (hashCode * 397) ^ _arrayStride.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            return $"{ElementType}[{Length}]";
        }

        public bool Equals(TypeArray other)
        {
            return ElementType == other.ElementType
                   && Length == other.Length
                   && ArrayStride == other.ArrayStride
                   && Alignment == other.Alignment;
        }

        partial void SetUp(OpTypeArray instruction, SpirvInstructionTreeBuilder treeBuilder)
        {
            var decorations = treeBuilder.GetDecorations(instruction);

            var length = instruction.Length.Instruction as OpConstant;
            var lengthType = treeBuilder.ResolveType(length.IdResultType);

            _arrayStride = decorations.FindDecoration<Decoration.ArrayStrideImpl>()?.ArrayStride;
            ElementType = treeBuilder.ResolveType(instruction.ElementType);
            if (lengthType == SpirvTypeBase.UInt)
            {
                Length = length.Value.Value.ToUInt32();
            }
            else if (lengthType == SpirvTypeBase.Int)
            {
                Length = (uint)length.Value.Value.ToInt32();
            }
            else
                throw new NotImplementedException();
        }

        public override IEnumerable<Node> BuildDecorations()
        {
            return base.BuildDecorations().Concat(BuildArrayStride());
        }

        private IEnumerable<Node> BuildArrayStride()
        {
            if (_arrayStride != null)
                yield return new Decorate(){Decoration = Decoration.ArrayStride(_arrayStride.Value), Target = this};
        }
    }
}