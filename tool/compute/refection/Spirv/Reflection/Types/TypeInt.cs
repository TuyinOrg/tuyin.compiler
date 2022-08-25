using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeInt : SpirvTypeBase
    {
        private readonly uint _width;
        private readonly bool _signed;

        internal TypeInt(uint width, bool signed)
        {
            _width = width;
            _signed = signed;
            IntType = GetType(_width, _signed);
        }

        public override uint SizeInBytes => _width / 8;

        public uint Width => _width;
        public bool Signed => _signed;

        public IntType IntType { get; }

        public static IntType GetType(uint width, bool signed)
        {
            switch (width)
            {
                case 8:
                    return signed ? IntType.SByte : IntType.Byte;
                case 16:
                    return signed ? IntType.Short : IntType.UShort;
                case 32:
                    return signed ? IntType.Int : IntType.UInt;
                default:
                    return IntType.Unknown;
            }
        }

        public override string ToString()
        {
            switch (IntType)
            {
                case IntType.SByte:
                    return "sbyte";
                case IntType.Byte:
                    return "byte";
                case IntType.Short:
                    return "short";
                case IntType.UShort:
                    return "ushort";
                case IntType.Int:
                    return "int";
                case IntType.UInt:
                    return "uint";
                default:
                    return (_signed ? "u" : "") + "int" + _width;
            }
        }
    }
}