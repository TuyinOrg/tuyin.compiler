using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal class TypeMatrixDeclaration : TypeMatrix
    {
        private readonly TypeVector _columnType;

        public TypeMatrixDeclaration(TypeVector columnType, uint columnCount)
        {
            _columnType = columnType;
            ColumnCount = columnCount;
        }

        public override Op OpCode => Op.OpTypeMatrix;

        public override TypeVector ColumnType => _columnType;
        public override uint ColumnStride => _columnType.Alignment;
        public override MatrixOrientation MatrixOrientation => MatrixOrientation.ColMajor;
        public override uint ColumnCount { get; }
    }
}