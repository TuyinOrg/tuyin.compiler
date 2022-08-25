using System;

namespace Toe.SPIRV.Reflection.Types
{
    internal abstract partial class TypeMatrix : SpirvTypeBase, IEquatable<TypeMatrix>
    {
        public TypeMatrix()
        {
        }

        public abstract TypeVector ColumnType { get; }

        public abstract uint ColumnStride { get; }

        public abstract MatrixOrientation MatrixOrientation { get; }

        public abstract uint ColumnCount { get; }

        public override uint Alignment => ColumnType.ComponentType.SizeInBytes * 4;

        public override uint SizeInBytes => ColumnStride * ColumnCount;

        public static bool operator ==(TypeMatrix left, TypeMatrix right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(TypeMatrix left, TypeMatrix right)
        {
            return !Equals(left, right);
        }

        public override bool Equals(object obj)
        {
            return ReferenceEquals(this, obj) || obj is TypeMatrix other && Equals(other);
        }

        public override int GetHashCode()
        {
            var hashCode = ColumnType.GetHashCode();
            hashCode = (hashCode * 397) ^ ColumnCount.GetHashCode();
            hashCode = (hashCode * 397) ^ ColumnStride.GetHashCode();
            hashCode = (hashCode * 397) ^ MatrixOrientation.GetHashCode();
            return hashCode;
        }

        public override string ToString()
        {
            var componentCount = ColumnType.ComponentCount;
            if (componentCount == ColumnCount)
                return "mat" + componentCount;
            return "mat" + ColumnCount + "x" + componentCount;
        }

        public bool Equals(TypeMatrix other)
        {
            return ColumnType == other.ColumnType
                   && ColumnCount == other.ColumnCount
                   && ColumnStride == other.ColumnStride
                   && MatrixOrientation == other.MatrixOrientation;
        }
    }
}