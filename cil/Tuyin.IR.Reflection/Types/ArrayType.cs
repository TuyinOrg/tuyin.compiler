namespace Tuyin.IR.Reflection.Types
{
    public class ArrayType : Type
    {
        public override string Name => $"{ElementType.Name}[]";

        public override uint BitsSize => 32;

        public Type ElementType { get; }

        public ArrayType(Type elementType)
        {
            ElementType = elementType;
        }
    }

}
