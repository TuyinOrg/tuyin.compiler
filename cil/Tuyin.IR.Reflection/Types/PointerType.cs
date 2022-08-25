namespace Tuyin.IR.Reflection.Types
{
    public class PointerType : Type
    {
        public override string Name => $"{ElementType.Name}*";

        public override uint BitsSize => 32;

        public Type ElementType { get; }

        public PointerType(Type elementType)
        {
            ElementType = elementType;
        }
    }

}
