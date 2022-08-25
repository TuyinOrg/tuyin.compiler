using Toe.SPIRV.Instructions;
using Toe.SPIRV.Spv;

namespace Toe.SPIRV.Reflection.Types
{
    internal partial class TypeRuntimeArray : SpirvTypeBase
    {
        public TypeRuntimeArray()
        {
        }

        /// <summary>
        /// TypeRuntimeArray is last field in a structure and it's size is defined in runtime. Let's take 1 as a size value for the array.
        /// </summary>
        public override uint SizeInBytes => Alignment * 1;

        public override uint Alignment => SpirvUtils.RoundUp(ElementType.Alignment, 16);
    }
}