namespace compute.vulkan.nvx
{
    /// <summary>
    /// Provides NVIDIA specific extension methods for the <see cref="Device"/> class.
    /// </summary>
    static class DeviceExtensions
    {
        /// <exception cref="VulkanException">Vulkan returns an error code.</exception>
        public static IndirectCommandsLayoutNvx CreateIndirectCommandsLayoutNvx(this Device device,
            IndirectCommandsLayoutCreateInfoNvx createInfo, AllocationCallbacks? allocator = null)
        {
            return new IndirectCommandsLayoutNvx(device, ref createInfo, ref allocator);
        }

        /// <exception cref="VulkanException">Vulkan returns an error code.</exception>
        public static ObjectTableNvx CreateObjectTableNvx(this Device device,
            ObjectTableCreateInfoNvx createInfo, AllocationCallbacks? allocator = null)
        {
            return new ObjectTableNvx(device, ref createInfo, ref allocator);
        }
    }
}
