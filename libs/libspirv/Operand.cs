using System;
using System.IO;

namespace libspirv
{
    public interface Operand : IEquatable<Operand>
    {
        OperandType Type { get; }

        ushort WordCount { get; }

        void WriteOperand(Stream stream);
    }
}
