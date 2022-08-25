using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using JetBrains.Annotations;

namespace GLFW
{
	/// <summary>
	///     Provides access to relevant native functions of the current operating system.
	///     <para>
	///         By using the native access functions you assert that you know what you're doing and how to fix problems
	///         caused by using them.
	///     </para>
	///     <para>If you don't, you shouldn't be using them.</para>
	/// </summary>
	[SuppressUnmanagedCodeSecurity]
    internal static class Native
    {
        #region External

        /// <summary>
        ///     Returns the contents of the selection as a string.
        /// </summary>
        /// <returns>The selected string, or <c>null</c> if error occurs or no string is selected.</returns>
        [CanBeNull]
        public static string GetX11SelectionString()
        {
            var ptr = GetX11SelectionStringInternal();
            return ptr == IntPtr.Zero ? null : Util.PtrToStringUTF8(ptr);
        }

        /// <summary>
        ///     Sets the clipboard string of an X11 window.
        /// </summary>
        /// <param name="str">The string to set.</param>
        public static void SetX11SelectionString([NotNull] string str)
        {
            SetX11SelectionString(Encoding.UTF8.GetBytes(str));
        }


        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt32 GetCocoaMonitorDelegate0(Monitor monitor);
        private static readonly GetCocoaMonitorDelegate0 GetCocoaMonitor0 = Glfw.ExternLibrary.GetStaticProc<GetCocoaMonitorDelegate0>("glfwGetCocoaMonitor");
        public static UInt32 GetCocoaMonitor(Monitor monitor) { return GetCocoaMonitor0(monitor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetX11DisplayDelegate0();
        private static readonly GetX11DisplayDelegate0 GetX11Display0 = Glfw.ExternLibrary.GetStaticProc<GetX11DisplayDelegate0>("glfwGetX11Display");
        public static IntPtr GetX11Display() { return GetX11Display0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetWaylandDisplayDelegate0();
        private static readonly GetWaylandDisplayDelegate0 GetWaylandDisplay0 = Glfw.ExternLibrary.GetStaticProc<GetWaylandDisplayDelegate0>("glfwGetWaylandDisplay");
        public static IntPtr GetWaylandDisplay() { return GetWaylandDisplay0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetWaylandMonitorDelegate0(Monitor monitor);
        private static readonly GetWaylandMonitorDelegate0 GetWaylandMonitor0 = Glfw.ExternLibrary.GetStaticProc<GetWaylandMonitorDelegate0>("glfwGetWaylandMonitor");
        public static IntPtr GetWaylandMonitor(Monitor monitor) { return GetWaylandMonitor0(monitor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetWaylandWindowDelegate0(Window window);
        private static readonly GetWaylandWindowDelegate0 GetWaylandWindow0 = Glfw.ExternLibrary.GetStaticProc<GetWaylandWindowDelegate0>("glfwGetWaylandWindow");
        public static IntPtr GetWaylandWindow(Window window) { return GetWaylandWindow0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetGLXWindowDelegate0(Window window);
        private static readonly GetGLXWindowDelegate0 GetGLXWindow0 = Glfw.ExternLibrary.GetStaticProc<GetGLXWindowDelegate0>("glfwGetGLXWindow");
        public static IntPtr GetGLXWindow(Window window) { return GetGLXWindow0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetX11WindowDelegate0(Window window);
        private static readonly GetX11WindowDelegate0 GetX11Window0 = Glfw.ExternLibrary.GetStaticProc<GetX11WindowDelegate0>("glfwGetX11Window");
        public static IntPtr GetX11Window(Window window) { return GetX11Window0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetX11MonitorDelegate0(Monitor monitor);
        private static readonly GetX11MonitorDelegate0 GetX11Monitor0 = Glfw.ExternLibrary.GetStaticProc<GetX11MonitorDelegate0>("glfwGetX11Monitor");
        public static IntPtr GetX11Monitor(Monitor monitor) { return GetX11Monitor0(monitor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetX11AdapterDelegate0(Monitor monitor);
        private static readonly GetX11AdapterDelegate0 GetX11Adapter0 = Glfw.ExternLibrary.GetStaticProc<GetX11AdapterDelegate0>("glfwGetX11Adapter");
        public static IntPtr GetX11Adapter(Monitor monitor) { return GetX11Adapter0(monitor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetCocoaWindowDelegate0(Window window);
        private static readonly GetCocoaWindowDelegate0 GetCocoaWindow0 = Glfw.ExternLibrary.GetStaticProc<GetCocoaWindowDelegate0>("glfwGetCocoaWindow");
        public static IntPtr GetCocoaWindow(Window window) { return GetCocoaWindow0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate NSOpenGLContext GetNSGLContextDelegate0(Window window);
        private static readonly GetNSGLContextDelegate0 GetNSGLContext0 = Glfw.ExternLibrary.GetStaticProc<GetNSGLContextDelegate0>("glfwGetNSGLContext");
        public static NSOpenGLContext GetNSGLContext(Window window) { return GetNSGLContext0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate OSMesaContext GetOSMesaContextDelegate0(Window window);
        private static readonly GetOSMesaContextDelegate0 GetOSMesaContext0 = Glfw.ExternLibrary.GetStaticProc<GetOSMesaContextDelegate0>("glfwGetOSMesaContext");
        public static OSMesaContext GetOSMesaContext(Window window) { return GetOSMesaContext0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate GLXContext GetGLXContextDelegate0(Window window);
        private static readonly GetGLXContextDelegate0 GetGLXContext0 = Glfw.ExternLibrary.GetStaticProc<GetGLXContextDelegate0>("glfwGetGLXContext");
        public static GLXContext GetGLXContext(Window window) { return GetGLXContext0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate EGLContext GetEglContextDelegate0(Window window);
        private static readonly GetEglContextDelegate0 GetEglContext0 = Glfw.ExternLibrary.GetStaticProc<GetEglContextDelegate0>("glfwGetEGLContext");
        public static EGLContext GetEglContext(Window window) { return GetEglContext0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate EGLDisplay GetEglDisplayDelegate0();
        private static readonly GetEglDisplayDelegate0 GetEglDisplay0 = Glfw.ExternLibrary.GetStaticProc<GetEglDisplayDelegate0>("glfwGetEGLDisplay");
        public static EGLDisplay GetEglDisplay() { return GetEglDisplay0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate EGLSurface GetEglSurfaceDelegate0(Window window);
        private static readonly GetEglSurfaceDelegate0 GetEglSurface0 = Glfw.ExternLibrary.GetStaticProc<GetEglSurfaceDelegate0>("glfwGetEGLSurface");
        public static EGLSurface GetEglSurface(Window window) { return GetEglSurface0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate HGLRC GetWglContextDelegate0(Window window);
        private static readonly GetWglContextDelegate0 GetWglContext0 = Glfw.ExternLibrary.GetStaticProc<GetWglContextDelegate0>("glfwGetWGLContext");
        public static HGLRC GetWglContext(Window window) { return GetWglContext0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetWin32WindowDelegate0(Window window);
        private static readonly GetWin32WindowDelegate0 GetWin32Window0 = Glfw.ExternLibrary.GetStaticProc<GetWin32WindowDelegate0>("glfwGetWin32Window");
        public static IntPtr GetWin32Window(Window window) { return GetWin32Window0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean GetOSMesaColorBufferDelegate0(Window window, out Int32 width, out Int32 height, out Int32 format, out IntPtr buffer);
        private static readonly GetOSMesaColorBufferDelegate0 GetOSMesaColorBuffer0 = Glfw.ExternLibrary.GetStaticProc<GetOSMesaColorBufferDelegate0>("glfwGetOSMesaColorBuffer");
        public static Boolean GetOSMesaColorBuffer(Window window, out Int32 width, out Int32 height, out Int32 format, out IntPtr buffer) { return GetOSMesaColorBuffer0(window, out width, out height, out format, out buffer); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean GetOSMesaDepthBufferDelegate0(Window window, out Int32 width, out Int32 height, out Int32 bytesPerValue, out IntPtr buffer);
        private static readonly GetOSMesaDepthBufferDelegate0 GetOSMesaDepthBuffer0 = Glfw.ExternLibrary.GetStaticProc<GetOSMesaDepthBufferDelegate0>("glfwGetOSMesaDepthBuffer");
        public static Boolean GetOSMesaDepthBuffer(Window window, out Int32 width, out Int32 height, out Int32 bytesPerValue, out IntPtr buffer) { return GetOSMesaDepthBuffer0(window, out width, out height, out bytesPerValue, out buffer); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetX11SelectionStringDelegate0(Byte[] str);
        private static readonly SetX11SelectionStringDelegate0 SetX11SelectionString0 = Glfw.ExternLibrary.GetStaticProc<SetX11SelectionStringDelegate0>("glfwSetX11SelectionString");
        private static void SetX11SelectionString(Byte[] str) { SetX11SelectionString0(str); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetX11SelectionStringInternalDelegate0();
        private static readonly GetX11SelectionStringInternalDelegate0 GetX11SelectionStringInternal0 = Glfw.ExternLibrary.GetStaticProc<GetX11SelectionStringInternalDelegate0>("glfwGetX11SelectionString");
        private static IntPtr GetX11SelectionStringInternal() { return GetX11SelectionStringInternal0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetWin32AdapterInternalDelegate0(Monitor monitor);
        private static readonly GetWin32AdapterInternalDelegate0 GetWin32AdapterInternal0 = Glfw.ExternLibrary.GetStaticProc<GetWin32AdapterInternalDelegate0>("glfwGetWin32Adapter");
        private static IntPtr GetWin32AdapterInternal(Monitor monitor) { return GetWin32AdapterInternal0(monitor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetWin32MonitorInternalDelegate0(Monitor monitor);
        private static readonly GetWin32MonitorInternalDelegate0 GetWin32MonitorInternal0 = Glfw.ExternLibrary.GetStaticProc<GetWin32MonitorInternalDelegate0>("glfwGetWin32Monitor");
        private static IntPtr GetWin32MonitorInternal(Monitor monitor) { return GetWin32MonitorInternal0(monitor); }


        #endregion

        #region Methods

        /// <summary>
        ///     Gets the win32 adapter.
        /// </summary>
        /// <param name="monitor">A monitor instance.</param>
        /// <returns>dapter device name (for example \\.\DISPLAY1) of the specified monitor, or <c>null</c> if an error occurred.</returns>
        public static string GetWin32Adapter(Monitor monitor)
        {
            return Util.PtrToStringUTF8(GetWin32AdapterInternal(monitor));
        }

        /// <summary>
        ///     Returns the display device name of the specified monitor
        /// </summary>
        /// <param name="monitor">A monitor instance.</param>
        /// <returns>
        ///     The display device name (for example \\.\DISPLAY1\Monitor0) of the specified monitor, or <c>null</c> if an
        ///     error occurred.
        /// </returns>
        public static string GetWin32Monitor(Monitor monitor)
        {
            return Util.PtrToStringUTF8(GetWin32MonitorInternal(monitor));
        }

        #endregion
    }
}