using System.IO;
using Vulkan;

namespace libtui.content
{
    internal static partial class Loader
    {
        public static ShaderModule LoadShaderModule(Window window, VulkanContext ctx, string path)
        {
            const int defaultBufferSize = 4096;
            using (Stream stream = window.Open(path))
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms, defaultBufferSize);
                return ctx.Device.CreateShaderModule(new ShaderModuleCreateInfo(ms.ToArray()));
            }
        }
    }
}
