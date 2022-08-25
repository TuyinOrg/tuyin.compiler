using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeFloat : SpirvTypeBase
    {
        private readonly uint _width;

        internal TypeFloat(uint width)
        {
            _width = width;
            FloatType = GetType(_width);
        }

        public override uint SizeInBytes => _width / 8;
        public uint Width => _width;

        public FloatType FloatType { get; }

        public static FloatType GetType(uint width)
        {
            switch (width)
            {
                case 16:
                    return FloatType.Half;
                case 32:
                    return FloatType.Float;
                case 64:
                    return FloatType.Double;
                default:
                    return FloatType.Unknown;
            }
        }

        public override string ToString()
        {
            switch (_width)
            {
                case 32:
                    return "float";
                case 64:
                    return "double";
                default:
                    return "float" + _width;
            }
        }
    }
}