using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal class TypeMatrixLayout : TypeMatrix
    {
        private readonly TypeMatrix _matrixType;
        private readonly uint? _columnStride;

        public TypeMatrixLayout(TypeMatrix matrixType,
            MatrixOrientation matrixOrientation = MatrixOrientation.ColMajor, uint? columnStride = null)
        {
            _matrixType = matrixType;
            MatrixOrientation = matrixOrientation;
            _columnStride = columnStride;
        }

        public override Op OpCode => Op.OpTypeMatrix;


        public override TypeVector ColumnType => _matrixType.ColumnType;
        public override uint ColumnStride => _columnStride ?? _matrixType.ColumnStride;
        public override uint ColumnCount => _matrixType.ColumnCount;

        public override MatrixOrientation MatrixOrientation { get; }
    }
}