using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal static class ExtensionMethods
    {
        public static TypePointer MakePointer(this SpirvTypeBase type, StorageClass storageClass)
        {
            return new TypePointer(storageClass, type);
        }
    }
}