using JetBrains.Annotations;
using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace GLFW
{
    /// <summary>
    ///     Implements the Vulkan specific functions of GLFW.
    ///     <para>See http://www.glfw.org/docs/latest/vulkan_guide.html for detailed documentation.</para>
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static class Vulkan
    {
        #region Properties

        /// <summary>
        ///     Gets whether the Vulkan loader has been found. This check is performed by <see cref="Glfw.Init" />.
        /// </summary>
        /// <value>
        ///     <c>true</c> if Vulkan is supported; otherwise <c>false</c>.
        /// </value>
        public static bool IsSupported => VulkanSupported();

        #endregion

        #region External

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 CreateWindowSurfaceDelegate0(IntPtr vulkan, IntPtr window, IntPtr allocator, out UInt64 surface);
        private static readonly CreateWindowSurfaceDelegate0 CreateWindowSurface0 = Glfw.ExternLibrary.GetStaticProc<CreateWindowSurfaceDelegate0>("glfwCreateWindowSurface");
        public static Int32 CreateWindowSurface(IntPtr vulkan, IntPtr window, IntPtr allocator, out UInt64 surface) { return CreateWindowSurface0(vulkan, window, allocator, out surface); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean GetPhysicalDevicePresentationSupportDelegate0(IntPtr instance, IntPtr device, UInt32 family);
        private static readonly GetPhysicalDevicePresentationSupportDelegate0 GetPhysicalDevicePresentationSupport0 = Glfw.ExternLibrary.GetStaticProc<GetPhysicalDevicePresentationSupportDelegate0>("glfwGetPhysicalDevicePresentationSupport");
        public static Boolean GetPhysicalDevicePresentationSupport(IntPtr instance, IntPtr device, UInt32 family) { return GetPhysicalDevicePresentationSupport0(instance, device, family); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetInstanceProcAddressDelegate0(IntPtr vulkan, Byte[] procName);
        private static readonly GetInstanceProcAddressDelegate0 GetInstanceProcAddress0 = Glfw.ExternLibrary.GetStaticProc<GetInstanceProcAddressDelegate0>("glfwGetInstanceProcAddress");
        private static IntPtr GetInstanceProcAddress(IntPtr vulkan, Byte[] procName) { return GetInstanceProcAddress0(vulkan, procName); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetRequiredInstanceExtensionsDelegate0(out UInt32 count);
        private static readonly GetRequiredInstanceExtensionsDelegate0 GetRequiredInstanceExtensions0 = Glfw.ExternLibrary.GetStaticProc<GetRequiredInstanceExtensionsDelegate0>("glfwGetRequiredInstanceExtensions");
        private static IntPtr GetRequiredInstanceExtensions(out UInt32 count) { return GetRequiredInstanceExtensions0(out count); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean VulkanSupportedDelegate0();
        private static readonly VulkanSupportedDelegate0 VulkanSupported0 = Glfw.ExternLibrary.GetStaticProc<VulkanSupportedDelegate0>("glfwVulkanSupported");
        private static Boolean VulkanSupported() { return VulkanSupported0(); }

        #endregion

        #region Methods

        /// <summary>
        ///     This function returns the address of the specified Vulkan core or extension function for the specified instance. If
        ///     instance is set to <see cref="IntPtr.Zero" /> it can return any function exported from the Vulkan loader.
        ///     <para>
        ///         If Vulkan is not available on the machine, this function returns <see cref="IntPtr.Zero" /> and generates an
        ///         error. Use <see cref="IsSupported" /> to check whether Vulkan is available.
        ///     </para>
        /// </summary>
        /// <param name="vulkan">The vulkan instance.</param>
        /// <param name="procName">Name of the function.</param>
        /// <returns>The address of the function, or <see cref="IntPtr.Zero" /> if an error occurred.</returns>
        public static IntPtr GetInstanceProcAddress(IntPtr vulkan, [NotNull] string procName)
        {
            return GetInstanceProcAddress(vulkan, Encoding.ASCII.GetBytes(procName));
        }

        /// <summary>
        ///     This function returns an array of names of Vulkan instance extensions required by GLFW for creating Vulkan surfaces
        ///     for GLFW windows. If successful, the list will always contains VK_KHR_surface, so if you don't require any
        ///     additional extensions you can pass this list directly to the VkInstanceCreateInfo struct.
        ///     <para>
        ///         If Vulkan is not available on the machine, this function returns generates an error, use
        ///         <see cref="IsSupported" /> to first check if supported.
        ///     </para>
        ///     <para>
        ///         If Vulkan is available but no set of extensions allowing window surface creation was found, this function
        ///         returns an empty array. You may still use Vulkan for off-screen rendering and compute work.
        ///     </para>
        /// </summary>
        /// <returns>An array of extension names.</returns>
        [NotNull]
        public static string[] GetRequiredInstanceExtensions()
        {
            var ptr = GetRequiredInstanceExtensions(out var count);
            var extensions = new string[count];
            if (count > 0 && ptr != IntPtr.Zero)
            {
                var offset = 0;
                for (var i = 0; i < count; i++, offset += IntPtr.Size)
                {
                    var p = Marshal.ReadIntPtr(ptr, offset);
                    extensions[i] = Marshal.PtrToStringAnsi(p);
                }
            }

            return extensions.Where(s => !string.IsNullOrWhiteSpace(s)).ToArray();
        }

        #endregion
    }
}