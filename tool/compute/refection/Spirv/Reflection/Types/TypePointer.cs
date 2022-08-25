using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypePointer : SpirvTypeBase
    {
        public TypePointer()
        {
            
        }
        public TypePointer(StorageClass storageClass, SpirvTypeBase type)
        {
            StorageClass = storageClass;
            Type = type;
        }

        public override string ToString()
        {
            if (DebugName != null)
                return DebugName;
            if (Type != null)
                return $"{Type}*";
            return base.ToString();
        }
    }
}