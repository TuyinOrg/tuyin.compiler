using compute.environment;
using System;

namespace compute.drawing
{
    /// <summary>
    /// 静态资源
    /// 通常被编译成VulkanBuffer
    /// </summary>
    public interface IStaticResource : IResource
    {
        ReadOnlySpan<byte> Compile();
    }
}
