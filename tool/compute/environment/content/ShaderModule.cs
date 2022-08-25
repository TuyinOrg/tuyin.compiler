using compute.vulkan;
using System.IO;

namespace compute.environment.content
{
    internal static partial class Loader
    {
        public static ShaderModule LoadShaderModule(IAppHost host, VulkanContext ctx, string path)
        {
            const int defaultBufferSize = 4096;
            using (Stream stream = host.Open(path))
            using (var ms = new MemoryStream())
            {
                stream.CopyTo(ms, defaultBufferSize);
                return ctx.Device.CreateShaderModule(new ShaderModuleCreateInfo(ms.ToArray()));
            }
        }
    }
}
