using FreeType;

namespace libtui.content
{
    internal static partial class Loader
    {
        private readonly static Library sLibrary = new Library();

        public static VulkanFont LoadFont(Window window, VulkanContext ctx, string path)
        {
            return VulkanFont.FromFace(ctx, sLibrary, new Face(sLibrary, path));
        }
    }
}
