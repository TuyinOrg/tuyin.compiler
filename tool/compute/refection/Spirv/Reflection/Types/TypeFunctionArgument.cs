namespace Toe.SPIRV.Reflection.Types
{
    internal class TypeFunctionArgument
    {
        private readonly SpirvTypeBase _type;

        public TypeFunctionArgument(SpirvTypeBase type)
        {
            _type = type;
        }

        public SpirvTypeBase Type
        {
            get => _type;
        }

        public override string ToString()
        {
            return _type.ToString();
        }
    }
}