using compute.environment.font;

namespace compute.environment.content
{
    internal static partial class Loader
    {
        private readonly static Library sLibrary = new Library();

        public static VulkanFont LoadFont(IAppHost host, VulkanContext ctx, string path)
        {
            return VulkanFont.FromFace(ctx, sLibrary, new Face(sLibrary, path));
        }
    }
}
