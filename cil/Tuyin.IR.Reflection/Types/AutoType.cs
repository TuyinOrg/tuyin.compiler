namespace Tuyin.IR.Reflection.Types
{
    public class AutoType : Type
    {
        public Type BindingType { get; set; }

        public override string Name => BindingType?.Name ?? "[Auto]Waitting for binding.";

        public override uint BitsSize => BindingType?.BitsSize ?? 0;

        public override bool IsArray => BindingType?.IsArray ?? false;

        public override bool IsAuto => BindingType?.IsAuto ?? true;

        public override bool IsPointer => BindingType?.IsPointer ?? false;

        public override bool IsStruct => BindingType?.IsStruct ?? false;

        public override bool IsMutable => BindingType?.IsMutable ?? false;
    }

}
