using libtui.controls;
using libtui.utils;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace GLFW
{
    internal static partial class Glfw
    {
        class GlfwLibrary : EnviromentLibrary
        {
            public GlfwLibrary() : base("GLFW")
            {
            }

            protected override IEnumerable<string> GetLinuxLibraries()
            {
                yield return $@"glfw";
            }

            protected override IEnumerable<string> GetOSXLibraries()
            {
                yield return $@"runtimes\{GetPlatformIdentity()}\native\lib\libglfw.3";
            }

            protected override IEnumerable<string> GetWindowsLibraries()
            {
 
                yield return $@"runtimes\{GetPlatformIdentity()}\native\lib\glfw3.dll";
            }
        }

        internal static EnviromentLibrary ExternLibrary { get; } = new GlfwLibrary();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetKeyNameInternalDelegate0(Keys key, Int32 scanCode);
        private static readonly GetKeyNameInternalDelegate0 GetKeyNameInternal0 = ExternLibrary.GetStaticProc<GetKeyNameInternalDelegate0>("glfwGetKeyName");
        private static IntPtr GetKeyNameInternal(Keys key, Int32 scanCode) { return GetKeyNameInternal0(key, scanCode); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate SizeCallback SetFramebufferSizeCallbackDelegate0(Window window, SizeCallback sizeCallback);
        private static readonly SetFramebufferSizeCallbackDelegate0 SetFramebufferSizeCallback0 = ExternLibrary.GetStaticProc<SetFramebufferSizeCallbackDelegate0>("glfwSetFramebufferSizeCallback");
        public static SizeCallback SetFramebufferSizeCallback(Window window, SizeCallback sizeCallback) { return SetFramebufferSizeCallback0(window, sizeCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate WindowCallback SetWindowRefreshCallbackDelegate0(Window window, WindowCallback callback);
        private static readonly SetWindowRefreshCallbackDelegate0 SetWindowRefreshCallback0 = ExternLibrary.GetStaticProc<SetWindowRefreshCallbackDelegate0>("glfwSetWindowRefreshCallback");
        public static WindowCallback SetWindowRefreshCallback(Window window, WindowCallback callback) { return SetWindowRefreshCallback0(window, callback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate KeyCallback SetKeyCallbackDelegate0(Window window, KeyCallback keyCallback);
        private static readonly SetKeyCallbackDelegate0 SetKeyCallback0 = ExternLibrary.GetStaticProc<SetKeyCallbackDelegate0>("glfwSetKeyCallback");
        public static KeyCallback SetKeyCallback(Window window, KeyCallback keyCallback) { return SetKeyCallback0(window, keyCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean JoystickPresentDelegate0(Joystick joystick);
        private static readonly JoystickPresentDelegate0 JoystickPresent0 = ExternLibrary.GetStaticProc<JoystickPresentDelegate0>("glfwJoystickPresent");
        public static Boolean JoystickPresent(Joystick joystick) { return JoystickPresent0(joystick); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetJoystickNameInternalDelegate0(Joystick joystick);
        private static readonly GetJoystickNameInternalDelegate0 GetJoystickNameInternal0 = ExternLibrary.GetStaticProc<GetJoystickNameInternalDelegate0>("glfwGetJoystickName");
        private static IntPtr GetJoystickNameInternal(Joystick joystick) { return GetJoystickNameInternal0(joystick); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetJoystickAxesDelegate0(Joystick joystic, out Int32 count);
        private static readonly GetJoystickAxesDelegate0 GetJoystickAxes0 = ExternLibrary.GetStaticProc<GetJoystickAxesDelegate0>("glfwGetJoystickAxes");
        private static IntPtr GetJoystickAxes(Joystick joystic, out Int32 count) { return GetJoystickAxes0(joystic, out count); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetJoystickButtonsDelegate0(Joystick joystick, out Int32 count);
        private static readonly GetJoystickButtonsDelegate0 GetJoystickButtons0 = ExternLibrary.GetStaticProc<GetJoystickButtonsDelegate0>("glfwGetJoystickButtons");
        private static IntPtr GetJoystickButtons(Joystick joystick, out Int32 count) { return GetJoystickButtons0(joystick, out count); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate JoystickCallback SetJoystickCallbackDelegate0(JoystickCallback callback);
        private static readonly SetJoystickCallbackDelegate0 SetJoystickCallback0 = ExternLibrary.GetStaticProc<SetJoystickCallbackDelegate0>("glfwSetJoystickCallback");
        public static JoystickCallback SetJoystickCallback(JoystickCallback callback) { return SetJoystickCallback0(callback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate MonitorCallback SetMonitorCallbackDelegate0(MonitorCallback monitorCallback);
        private static readonly SetMonitorCallbackDelegate0 SetMonitorCallback0 = ExternLibrary.GetStaticProc<SetMonitorCallbackDelegate0>("glfwSetMonitorCallback");
        public static MonitorCallback SetMonitorCallback(MonitorCallback monitorCallback) { return SetMonitorCallback0(monitorCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IconifyCallback SetWindowIconifyCallbackDelegate0(Window window, IconifyCallback callback);
        private static readonly SetWindowIconifyCallbackDelegate0 SetWindowIconifyCallback0 = ExternLibrary.GetStaticProc<SetWindowIconifyCallbackDelegate0>("glfwSetWindowIconifyCallback");
        public static IconifyCallback SetWindowIconifyCallback(Window window, IconifyCallback callback) { return SetWindowIconifyCallback0(window, callback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetInputModeDelegate0(Window window, InputMode mode, Int32 value);
        private static readonly SetInputModeDelegate0 SetInputMode0 = ExternLibrary.GetStaticProc<SetInputModeDelegate0>("glfwSetInputMode");
        public static void SetInputMode(Window window, InputMode mode, Int32 value) { SetInputMode0(window, mode, value); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 GetInputModeDelegate0(Window window, InputMode mode);
        private static readonly GetInputModeDelegate0 GetInputMode0 = ExternLibrary.GetStaticProc<GetInputModeDelegate0>("glfwGetInputMode");
        public static Int32 GetInputMode(Window window, InputMode mode) { return GetInputMode0(window, mode); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void GetMonitorWorkAreaDelegate0(IntPtr monitor, out Int32 x, out Int32 y, out Int32 width, out Int32 height);
        private static readonly GetMonitorWorkAreaDelegate0 GetMonitorWorkArea0 = ExternLibrary.GetStaticProc<GetMonitorWorkAreaDelegate0>("glfwGetMonitorWorkarea");
        public static void GetMonitorWorkArea(IntPtr monitor, out Int32 x, out Int32 y, out Int32 width, out Int32 height) { GetMonitorWorkArea0(monitor, out x, out y, out width, out height); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetProcAddressDelegate0(Byte[] procName);
        private static readonly GetProcAddressDelegate0 GetProcAddress0 = ExternLibrary.GetStaticProc<GetProcAddressDelegate0>("glfwGetProcAddress");
        private static IntPtr GetProcAddress(Byte[] procName) { return GetProcAddress0(procName); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void WindowHintDelegate0(Hint hint, Int32 value);
        private static readonly WindowHintDelegate0 WindowHint0 = ExternLibrary.GetStaticProc<WindowHintDelegate0>("glfwWindowHint");
        public static void WindowHint(Hint hint, Int32 value) { WindowHint0(hint, value); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 GetWindowAttributeDelegate0(Window window, Int32 attribute);
        private static readonly GetWindowAttributeDelegate0 GetWindowAttribute0 = ExternLibrary.GetStaticProc<GetWindowAttributeDelegate0>("glfwGetWindowAttrib");
        private static Int32 GetWindowAttribute(Window window, Int32 attribute) { return GetWindowAttribute0(window, attribute); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate ErrorCode GetErrorPrivateDelegate0(out IntPtr description);
        private static readonly GetErrorPrivateDelegate0 GetErrorPrivate0 = ExternLibrary.GetStaticProc<GetErrorPrivateDelegate0>("glfwGetError");
        private static ErrorCode GetErrorPrivate(out IntPtr description) { return GetErrorPrivate0(out description); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetClipboardStringDelegate0(Window window, Byte[] bytes);
        private static readonly SetClipboardStringDelegate0 SetClipboardString0 = ExternLibrary.GetStaticProc<SetClipboardStringDelegate0>("glfwSetClipboardString");
        private static void SetClipboardString(Window window, Byte[] bytes) { SetClipboardString0(window, bytes); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FileDropCallback SetDropCallbackDelegate0(Window window, FileDropCallback dropCallback);
        private static readonly SetDropCallbackDelegate0 SetDropCallback0 = ExternLibrary.GetStaticProc<SetDropCallbackDelegate0>("glfwSetDropCallback");
        public static FileDropCallback SetDropCallback(Window window, FileDropCallback dropCallback) { return SetDropCallback0(window, dropCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetMonitorNameInternalDelegate0(Monitor monitor);
        private static readonly GetMonitorNameInternalDelegate0 GetMonitorNameInternal0 = ExternLibrary.GetStaticProc<GetMonitorNameInternalDelegate0>("glfwGetMonitorName");
        private static IntPtr GetMonitorNameInternal(Monitor monitor) { return GetMonitorNameInternal0(monitor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Cursor CreateCursorDelegate0(Image image, Int32 xHotspot, Int32 yHotspot);
        private static readonly CreateCursorDelegate0 CreateCursor0 = ExternLibrary.GetStaticProc<CreateCursorDelegate0>("glfwCreateCursor");
        public static Cursor CreateCursor(Image image, Int32 xHotspot, Int32 yHotspot) { return CreateCursor0(image, xHotspot, yHotspot); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void DestroyCursorDelegate0(Cursor cursor);
        private static readonly DestroyCursorDelegate0 DestroyCursor0 = ExternLibrary.GetStaticProc<DestroyCursorDelegate0>("glfwDestroyCursor");
        public static void DestroyCursor(Cursor cursor) { DestroyCursor0(cursor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetCursorDelegate0(Window window, Cursor cursor);
        private static readonly SetCursorDelegate0 SetCursor0 = ExternLibrary.GetStaticProc<SetCursorDelegate0>("glfwSetCursor");
        public static void SetCursor(Window window, Cursor cursor) { SetCursor0(window, cursor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Cursor CreateStandardCursorDelegate0(CursorType type);
        private static readonly CreateStandardCursorDelegate0 CreateStandardCursor0 = ExternLibrary.GetStaticProc<CreateStandardCursorDelegate0>("glfwCreateStandardCursor");
        public static Cursor CreateStandardCursor(CursorType type) { return CreateStandardCursor0(type); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void GetCursorPositionDelegate0(Window window, out Double x, out Double y);
        private static readonly GetCursorPositionDelegate0 GetCursorPosition0 = ExternLibrary.GetStaticProc<GetCursorPositionDelegate0>("glfwGetCursorPos");
        public static void GetCursorPosition(Window window, out Double x, out Double y) { GetCursorPosition0(window, out x, out y); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetCursorPositionDelegate0(Window window, Double x, Double y);
        private static readonly SetCursorPositionDelegate0 SetCursorPosition0 = ExternLibrary.GetStaticProc<SetCursorPositionDelegate0>("glfwSetCursorPos");
        public static void SetCursorPosition(Window window, Double x, Double y) { SetCursorPosition0(window, x, y); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate MouseCallback SetCursorPositionCallbackDelegate0(Window window, MouseCallback mouseCallback);
        private static readonly SetCursorPositionCallbackDelegate0 SetCursorPositionCallback0 = ExternLibrary.GetStaticProc<SetCursorPositionCallbackDelegate0>("glfwSetCursorPosCallback");
        public static MouseCallback SetCursorPositionCallback(Window window, MouseCallback mouseCallback) { return SetCursorPositionCallback0(window, mouseCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate MouseEnterCallback SetCursorEnterCallbackDelegate0(Window window, MouseEnterCallback mouseCallback);
        private static readonly SetCursorEnterCallbackDelegate0 SetCursorEnterCallback0 = ExternLibrary.GetStaticProc<SetCursorEnterCallbackDelegate0>("glfwSetCursorEnterCallback");
        public static MouseEnterCallback SetCursorEnterCallback(Window window, MouseEnterCallback mouseCallback) { return SetCursorEnterCallback0(window, mouseCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate MouseButtonCallback SetMouseButtonCallbackDelegate0(Window window, MouseButtonCallback mouseCallback);
        private static readonly SetMouseButtonCallbackDelegate0 SetMouseButtonCallback0 = ExternLibrary.GetStaticProc<SetMouseButtonCallbackDelegate0>("glfwSetMouseButtonCallback");
        public static MouseButtonCallback SetMouseButtonCallback(Window window, MouseButtonCallback mouseCallback) { return SetMouseButtonCallback0(window, mouseCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate MouseCallback SetScrollCallbackDelegate0(Window window, MouseCallback mouseCallback);
        private static readonly SetScrollCallbackDelegate0 SetScrollCallback0 = ExternLibrary.GetStaticProc<SetScrollCallbackDelegate0>("glfwSetScrollCallback");
        public static MouseCallback SetScrollCallback(Window window, MouseCallback mouseCallback) { return SetScrollCallback0(window, mouseCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate InputState GetMouseButtonDelegate0(Window window, MouseButtons button);
        private static readonly GetMouseButtonDelegate0 GetMouseButton0 = ExternLibrary.GetStaticProc<GetMouseButtonDelegate0>("glfwGetMouseButton");
        public static InputState GetMouseButton(Window window, MouseButtons button) { return GetMouseButton0(window, button); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetWindowUserPointerDelegate0(Window window, IntPtr userPointer);
        private static readonly SetWindowUserPointerDelegate0 SetWindowUserPointer0 = ExternLibrary.GetStaticProc<SetWindowUserPointerDelegate0>("glfwSetWindowUserPointer");
        public static void SetWindowUserPointer(Window window, IntPtr userPointer) { SetWindowUserPointer0(window, userPointer); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetWindowUserPointerDelegate0(Window window);
        private static readonly GetWindowUserPointerDelegate0 GetWindowUserPointer0 = ExternLibrary.GetStaticProc<GetWindowUserPointerDelegate0>("glfwGetWindowUserPointer");
        public static IntPtr GetWindowUserPointer(Window window) { return GetWindowUserPointer0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetWindowSizeLimitsDelegate0(Window window, Int32 minWidth, Int32 minHeight, Int32 maxWidth, Int32 maxHeight);
        private static readonly SetWindowSizeLimitsDelegate0 SetWindowSizeLimits0 = ExternLibrary.GetStaticProc<SetWindowSizeLimitsDelegate0>("glfwSetWindowSizeLimits");
        public static void SetWindowSizeLimits(Window window, Int32 minWidth, Int32 minHeight, Int32 maxWidth, Int32 maxHeight) { SetWindowSizeLimits0(window, minWidth, minHeight, maxWidth, maxHeight); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetWindowAspectRatioDelegate0(Window window, Int32 numerator, Int32 denominator);
        private static readonly SetWindowAspectRatioDelegate0 SetWindowAspectRatio0 = ExternLibrary.GetStaticProc<SetWindowAspectRatioDelegate0>("glfwSetWindowAspectRatio");
        public static void SetWindowAspectRatio(Window window, Int32 numerator, Int32 denominator) { SetWindowAspectRatio0(window, numerator, denominator); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Window GetCurrentContextDelegate0();
        private static readonly GetCurrentContextDelegate0 GetCurrentContext0 = ExternLibrary.GetStaticProc<GetCurrentContextDelegate0>("glfwGetCurrentContext");
        private static Window GetCurrentContext() { return GetCurrentContext0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void GetMonitorPhysicalSizeDelegate0(Monitor monitor, out Int32 width, out Int32 height);
        private static readonly GetMonitorPhysicalSizeDelegate0 GetMonitorPhysicalSize0 = ExternLibrary.GetStaticProc<GetMonitorPhysicalSizeDelegate0>("glfwGetMonitorPhysicalSize");
        public static void GetMonitorPhysicalSize(Monitor monitor, out Int32 width, out Int32 height) { GetMonitorPhysicalSize0(monitor, out width, out height); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void GetMonitorPositionDelegate0(Monitor monitor, out Int32 x, out Int32 y);
        private static readonly GetMonitorPositionDelegate0 GetMonitorPosition0 = ExternLibrary.GetStaticProc<GetMonitorPositionDelegate0>("glfwGetMonitorPos");
        public static void GetMonitorPosition(Monitor monitor, out Int32 x, out Int32 y) { GetMonitorPosition0(monitor, out x, out y); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetMonitorsDelegate0(out Int32 count);
        private static readonly GetMonitorsDelegate0 GetMonitors0 = ExternLibrary.GetStaticProc<GetMonitorsDelegate0>("glfwGetMonitors");
        private static IntPtr GetMonitors(out Int32 count) { return GetMonitors0(out count); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate CharCallback SetCharCallbackDelegate0(Window window, CharCallback charCallback);
        private static readonly SetCharCallbackDelegate0 SetCharCallback0 = ExternLibrary.GetStaticProc<SetCharCallbackDelegate0>("glfwSetCharCallback");
        public static CharCallback SetCharCallback(Window window, CharCallback charCallback) { return SetCharCallback0(window, charCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate CharModsCallback SetCharModsCallbackDelegate0(Window window, CharModsCallback charCallback);
        private static readonly SetCharModsCallbackDelegate0 SetCharModsCallback0 = ExternLibrary.GetStaticProc<SetCharModsCallbackDelegate0>("glfwSetCharModsCallback");
        public static CharModsCallback SetCharModsCallback(Window window, CharModsCallback charCallback) { return SetCharModsCallback0(window, charCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate InputState GetKeyDelegate0(Window window, Keys key);
        private static readonly GetKeyDelegate0 GetKey0 = ExternLibrary.GetStaticProc<GetKeyDelegate0>("glfwGetKey");
        public static InputState GetKey(Window window, Keys key) { return GetKey0(window, key); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void MaximizeWindowDelegate0(Window window);
        private static readonly MaximizeWindowDelegate0 MaximizeWindow0 = ExternLibrary.GetStaticProc<MaximizeWindowDelegate0>("glfwMaximizeWindow");
        public static void MaximizeWindow(Window window) { MaximizeWindow0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void IconifyWindowDelegate0(Window window);
        private static readonly IconifyWindowDelegate0 IconifyWindow0 = ExternLibrary.GetStaticProc<IconifyWindowDelegate0>("glfwIconifyWindow");
        public static void IconifyWindow(Window window) { IconifyWindow0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void RestoreWindowDelegate0(Window window);
        private static readonly RestoreWindowDelegate0 RestoreWindow0 = ExternLibrary.GetStaticProc<RestoreWindowDelegate0>("glfwRestoreWindow");
        public static void RestoreWindow(Window window) { RestoreWindow0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void MakeContextCurrentDelegate0(Window window);
        private static readonly MakeContextCurrentDelegate0 MakeContextCurrent0 = ExternLibrary.GetStaticProc<MakeContextCurrentDelegate0>("glfwMakeContextCurrent");
        public static void MakeContextCurrent(Window window) { MakeContextCurrent0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SwapBuffersDelegate0(Window window);
        private static readonly SwapBuffersDelegate0 SwapBuffers0 = ExternLibrary.GetStaticProc<SwapBuffersDelegate0>("glfwSwapBuffers");
        public static void SwapBuffers(Window window) { SwapBuffers0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SwapIntervalDelegate0(Int32 interval);
        private static readonly SwapIntervalDelegate0 SwapInterval0 = ExternLibrary.GetStaticProc<SwapIntervalDelegate0>("glfwSwapInterval");
        public static void SwapInterval(Int32 interval) { SwapInterval0(interval); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean GetExtensionSupportedDelegate0(Byte[] extension);
        private static readonly GetExtensionSupportedDelegate0 GetExtensionSupported0 = ExternLibrary.GetStaticProc<GetExtensionSupportedDelegate0>("glfwExtensionSupported");
        private static Boolean GetExtensionSupported(Byte[] extension) { return GetExtensionSupported0(extension); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void DefaultWindowHintsDelegate0();
        private static readonly DefaultWindowHintsDelegate0 DefaultWindowHints0 = ExternLibrary.GetStaticProc<DefaultWindowHintsDelegate0>("glfwDefaultWindowHints");
        public static void DefaultWindowHints() { DefaultWindowHints0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean WindowShouldCloseDelegate0(Window window);
        private static readonly WindowShouldCloseDelegate0 WindowShouldClose0 = ExternLibrary.GetStaticProc<WindowShouldCloseDelegate0>("glfwWindowShouldClose");
        public static Boolean WindowShouldClose(Window window) { return WindowShouldClose0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetWindowShouldCloseDelegate0(Window window, Boolean close);
        private static readonly SetWindowShouldCloseDelegate0 SetWindowShouldClose0 = ExternLibrary.GetStaticProc<SetWindowShouldCloseDelegate0>("glfwSetWindowShouldClose");
        public static void SetWindowShouldClose(Window window, Boolean close) { SetWindowShouldClose0(window, close); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetWindowIconDelegate0(Window window, Int32 count, Image[] images);
        private static readonly SetWindowIconDelegate0 SetWindowIcon0 = ExternLibrary.GetStaticProc<SetWindowIconDelegate0>("glfwSetWindowIcon");
        public static void SetWindowIcon(Window window, Int32 count, Image[] images) { SetWindowIcon0(window, count, images); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void WaitEventsDelegate0();
        private static readonly WaitEventsDelegate0 WaitEvents0 = ExternLibrary.GetStaticProc<WaitEventsDelegate0>("glfwWaitEvents");
        public static void WaitEvents() { WaitEvents0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void PollEventsDelegate0();
        private static readonly PollEventsDelegate0 PollEvents0 = ExternLibrary.GetStaticProc<PollEventsDelegate0>("glfwPollEvents");
        public static void PollEvents() { PollEvents0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void PostEmptyEventDelegate0();
        private static readonly PostEmptyEventDelegate0 PostEmptyEvent0 = ExternLibrary.GetStaticProc<PostEmptyEventDelegate0>("glfwPostEmptyEvent");
        public static void PostEmptyEvent() { PostEmptyEvent0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void WaitEventsTimeoutDelegate0(Double timeout);
        private static readonly WaitEventsTimeoutDelegate0 WaitEventsTimeout0 = ExternLibrary.GetStaticProc<WaitEventsTimeoutDelegate0>("glfwWaitEventsTimeout");
        public static void WaitEventsTimeout(Double timeout) { WaitEventsTimeout0(timeout); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate WindowCallback SetCloseCallbackDelegate0(Window window, WindowCallback closeCallback);
        private static readonly SetCloseCallbackDelegate0 SetCloseCallback0 = ExternLibrary.GetStaticProc<SetCloseCallbackDelegate0>("glfwSetWindowCloseCallback");
        public static WindowCallback SetCloseCallback(Window window, WindowCallback closeCallback) { return SetCloseCallback0(window, closeCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Monitor GetPrimaryMonitorDelegate0();
        private static readonly GetPrimaryMonitorDelegate0 GetPrimaryMonitor0 = ExternLibrary.GetStaticProc<GetPrimaryMonitorDelegate0>("glfwGetPrimaryMonitor");
        private static Monitor GetPrimaryMonitor() { return GetPrimaryMonitor0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetVideoModeInternalDelegate0(Monitor monitor);
        private static readonly GetVideoModeInternalDelegate0 GetVideoModeInternal0 = ExternLibrary.GetStaticProc<GetVideoModeInternalDelegate0>("glfwGetVideoMode");
        private static IntPtr GetVideoModeInternal(Monitor monitor) { return GetVideoModeInternal0(monitor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetVideoModesDelegate0(Monitor monitor, out Int32 count);
        private static readonly GetVideoModesDelegate0 GetVideoModes0 = ExternLibrary.GetStaticProc<GetVideoModesDelegate0>("glfwGetVideoModes");
        private static IntPtr GetVideoModes(Monitor monitor, out Int32 count) { return GetVideoModes0(monitor, out count); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Monitor GetWindowMonitorDelegate0(Window window);
        private static readonly GetWindowMonitorDelegate0 GetWindowMonitor0 = ExternLibrary.GetStaticProc<GetWindowMonitorDelegate0>("glfwGetWindowMonitor");
        public static Monitor GetWindowMonitor(Window window) { return GetWindowMonitor0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetWindowMonitorDelegate0(Window window, Monitor monitor, Int32 x, Int32 y, Int32 width, Int32 height, Int32 refreshRate);
        private static readonly SetWindowMonitorDelegate0 SetWindowMonitor0 = ExternLibrary.GetStaticProc<SetWindowMonitorDelegate0>("glfwSetWindowMonitor");
        public static void SetWindowMonitor(Window window, Monitor monitor, Int32 x, Int32 y, Int32 width, Int32 height, Int32 refreshRate) { SetWindowMonitor0(window, monitor, x, y, width, height, refreshRate); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetGammaRampInternalDelegate0(Monitor monitor);
        private static readonly GetGammaRampInternalDelegate0 GetGammaRampInternal0 = ExternLibrary.GetStaticProc<GetGammaRampInternalDelegate0>("glfwGetGammaRamp");
        internal static IntPtr GetGammaRampInternal(Monitor monitor) { return GetGammaRampInternal0(monitor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetGammaRampDelegate0(Monitor monitor, GammaRampInternal gammaRamp);
        private static readonly SetGammaRampDelegate0 SetGammaRamp0 = ExternLibrary.GetStaticProc<SetGammaRampDelegate0>("glfwSetGammaRamp");
        public static void SetGammaRamp(Monitor monitor, GammaRamp gammaRamp) { SetGammaRamp0(monitor, gammaRamp.ToInternal()); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetGammaDelegate0(Monitor monitor, Single gamma);
        private static readonly SetGammaDelegate0 SetGamma0 = ExternLibrary.GetStaticProc<SetGammaDelegate0>("glfwSetGamma");
        public static void SetGamma(Monitor monitor, Single gamma) { SetGamma0(monitor, gamma); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetClipboardStringInternalDelegate0(Window window);
        private static readonly GetClipboardStringInternalDelegate0 GetClipboardStringInternal0 = ExternLibrary.GetStaticProc<GetClipboardStringInternalDelegate0>("glfwGetClipboardString");
        private static IntPtr GetClipboardStringInternal(Window window) { return GetClipboardStringInternal0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void InitHintDelegate0(Hint hint, Boolean value);
        private static readonly InitHintDelegate0 InitHint0 = ExternLibrary.GetStaticProc<InitHintDelegate0>("glfwInitHint");
        public static void InitHint(Hint hint, Boolean value) { InitHint0(hint, value); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean InitDelegate0();
        private static readonly InitDelegate0 Init0 = ExternLibrary.GetStaticProc<InitDelegate0>("glfwInit");
        public static Boolean Init() { return Init0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void TerminateDelegate0();
        private static readonly TerminateDelegate0 Terminate0 = ExternLibrary.GetStaticProc<TerminateDelegate0>("glfwTerminate");
        public static void Terminate() { Terminate0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate ErrorCallback SetErrorCallbackDelegate0(ErrorCallback errorHandler);
        private static readonly SetErrorCallbackDelegate0 SetErrorCallback0 = ExternLibrary.GetStaticProc<SetErrorCallbackDelegate0>("glfwSetErrorCallback");
        public static ErrorCallback SetErrorCallback(ErrorCallback errorHandler) { return SetErrorCallback0(errorHandler); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Window CreateWindowDelegate0(Int32 width, Int32 height, Byte[] title, Monitor monitor, Window share);
        private static readonly CreateWindowDelegate0 CreateWindow0 = ExternLibrary.GetStaticProc<CreateWindowDelegate0>("glfwCreateWindow");
        private static Window CreateWindow(Int32 width, Int32 height, Byte[] title, Monitor monitor, Window share) { return CreateWindow0(width, height, title, monitor, share); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void DestroyWindowDelegate0(Window window);
        private static readonly DestroyWindowDelegate0 DestroyWindow0 = ExternLibrary.GetStaticProc<DestroyWindowDelegate0>("glfwDestroyWindow");
        public static void DestroyWindow(Window window) { DestroyWindow0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void ShowWindowDelegate0(Window window);
        private static readonly ShowWindowDelegate0 ShowWindow0 = ExternLibrary.GetStaticProc<ShowWindowDelegate0>("glfwShowWindow");
        public static void ShowWindow(Window window) { ShowWindow0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void HideWindowDelegate0(Window window);
        private static readonly HideWindowDelegate0 HideWindow0 = ExternLibrary.GetStaticProc<HideWindowDelegate0>("glfwHideWindow");
        public static void HideWindow(Window window) { HideWindow0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void GetWindowPositionDelegate0(Window window, out Int32 x, out Int32 y);
        private static readonly GetWindowPositionDelegate0 GetWindowPosition0 = ExternLibrary.GetStaticProc<GetWindowPositionDelegate0>("glfwGetWindowPos");
        public static void GetWindowPosition(Window window, out Int32 x, out Int32 y) { GetWindowPosition0(window, out x, out y); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetWindowPositionDelegate0(Window window, Int32 x, Int32 y);
        private static readonly SetWindowPositionDelegate0 SetWindowPosition0 = ExternLibrary.GetStaticProc<SetWindowPositionDelegate0>("glfwSetWindowPos");
        public static void SetWindowPosition(Window window, Int32 x, Int32 y) { SetWindowPosition0(window, x, y); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void GetWindowSizeDelegate0(Window window, out Int32 width, out Int32 height);
        private static readonly GetWindowSizeDelegate0 GetWindowSize0 = ExternLibrary.GetStaticProc<GetWindowSizeDelegate0>("glfwGetWindowSize");
        public static void GetWindowSize(Window window, out Int32 width, out Int32 height) { GetWindowSize0(window, out width, out height); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetWindowSizeDelegate0(Window window, Int32 width, Int32 height);
        private static readonly SetWindowSizeDelegate0 SetWindowSize0 = ExternLibrary.GetStaticProc<SetWindowSizeDelegate0>("glfwSetWindowSize");
        public static void SetWindowSize(Window window, Int32 width, Int32 height) { SetWindowSize0(window, width, height); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void GetFramebufferSizeDelegate0(Window window, out Int32 width, out Int32 height);
        private static readonly GetFramebufferSizeDelegate0 GetFramebufferSize0 = ExternLibrary.GetStaticProc<GetFramebufferSizeDelegate0>("glfwGetFramebufferSize");
        public static void GetFramebufferSize(Window window, out Int32 width, out Int32 height) { GetFramebufferSize0(window, out width, out height); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate PositionCallback SetWindowPositionCallbackDelegate0(Window window, PositionCallback positionCallback);
        private static readonly SetWindowPositionCallbackDelegate0 SetWindowPositionCallback0 = ExternLibrary.GetStaticProc<SetWindowPositionCallbackDelegate0>("glfwSetWindowPosCallback");
        public static PositionCallback SetWindowPositionCallback(Window window, PositionCallback positionCallback) { return SetWindowPositionCallback0(window, positionCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate SizeCallback SetWindowSizeCallbackDelegate0(Window window, SizeCallback sizeCallback);
        private static readonly SetWindowSizeCallbackDelegate0 SetWindowSizeCallback0 = ExternLibrary.GetStaticProc<SetWindowSizeCallbackDelegate0>("glfwSetWindowSizeCallback");
        public static SizeCallback SetWindowSizeCallback(Window window, SizeCallback sizeCallback) { return SetWindowSizeCallback0(window, sizeCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetWindowTitleDelegate0(Window window, Byte[] title);
        private static readonly SetWindowTitleDelegate0 SetWindowTitle0 = ExternLibrary.GetStaticProc<SetWindowTitleDelegate0>("glfwSetWindowTitle");
        private static void SetWindowTitle(Window window, Byte[] title) { SetWindowTitle0(window, title); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void FocusWindowDelegate0(Window window);
        private static readonly FocusWindowDelegate0 FocusWindow0 = ExternLibrary.GetStaticProc<FocusWindowDelegate0>("glfwFocusWindow");
        public static void FocusWindow(Window window) { FocusWindow0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate FocusCallback SetWindowFocusCallbackDelegate0(Window window, FocusCallback focusCallback);
        private static readonly SetWindowFocusCallbackDelegate0 SetWindowFocusCallback0 = ExternLibrary.GetStaticProc<SetWindowFocusCallbackDelegate0>("glfwSetWindowFocusCallback");
        public static FocusCallback SetWindowFocusCallback(Window window, FocusCallback focusCallback) { return SetWindowFocusCallback0(window, focusCallback); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void GetVersionDelegate0(out Int32 major, out Int32 minor, out Int32 revision);
        private static readonly GetVersionDelegate0 GetVersion0 = ExternLibrary.GetStaticProc<GetVersionDelegate0>("glfwGetVersion");
        public static void GetVersion(out Int32 major, out Int32 minor, out Int32 revision) { GetVersion0(out major, out minor, out revision); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetVersionStringDelegate0();
        private static readonly GetVersionStringDelegate0 GetVersionString0 = ExternLibrary.GetStaticProc<GetVersionStringDelegate0>("glfwGetVersionString");
        private static IntPtr GetVersionString() { return GetVersionString0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Double GetTimeDelegate0();
        private static readonly GetTimeDelegate0 GetTime0 = ExternLibrary.GetStaticProc<GetTimeDelegate0>("glfwGetTime");
        private static Double GetTime() { return GetTime0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetTimeDelegate0(Double time);
        private static readonly SetTimeDelegate0 SetTime0 = ExternLibrary.GetStaticProc<SetTimeDelegate0>("glfwSetTime");
        private static void SetTime(Double time) { SetTime0(time); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt64 GetTimerFrequencyDelegate0();
        private static readonly GetTimerFrequencyDelegate0 GetTimerFrequency0 = ExternLibrary.GetStaticProc<GetTimerFrequencyDelegate0>("glfwGetTimerFrequency");
        private static UInt64 GetTimerFrequency() { return GetTimerFrequency0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate UInt64 GetTimerValueDelegate0();
        private static readonly GetTimerValueDelegate0 GetTimerValue0 = ExternLibrary.GetStaticProc<GetTimerValueDelegate0>("glfwGetTimerValue");
        private static UInt64 GetTimerValue() { return GetTimerValue0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void GetWindowFrameSizeDelegate0(Window window, out Int32 left, out Int32 top, out Int32 right, out Int32 bottom);
        private static readonly GetWindowFrameSizeDelegate0 GetWindowFrameSize0 = ExternLibrary.GetStaticProc<GetWindowFrameSizeDelegate0>("glfwGetWindowFrameSize");
        public static void GetWindowFrameSize(Window window, out Int32 left, out Int32 top, out Int32 right, out Int32 bottom) { GetWindowFrameSize0(window, out left, out top, out right, out bottom); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void GetMonitorContentScaleDelegate0(IntPtr monitor, out Single xScale, out Single yScale);
        private static readonly GetMonitorContentScaleDelegate0 GetMonitorContentScale0 = ExternLibrary.GetStaticProc<GetMonitorContentScaleDelegate0>("glfwGetMonitorContentScale");
        public static void GetMonitorContentScale(IntPtr monitor, out Single xScale, out Single yScale) { GetMonitorContentScale0(monitor, out xScale, out yScale); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetMonitorUserPointerDelegate0(IntPtr monitor);
        private static readonly GetMonitorUserPointerDelegate0 GetMonitorUserPointer0 = ExternLibrary.GetStaticProc<GetMonitorUserPointerDelegate0>("glfwGetMonitorUserPointer");
        public static IntPtr GetMonitorUserPointer(IntPtr monitor) { return GetMonitorUserPointer0(monitor); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetMonitorUserPointerDelegate0(IntPtr monitor, IntPtr pointer);
        private static readonly SetMonitorUserPointerDelegate0 SetMonitorUserPointer0 = ExternLibrary.GetStaticProc<SetMonitorUserPointerDelegate0>("glfwSetMonitorUserPointer");
        public static void SetMonitorUserPointer(IntPtr monitor, IntPtr pointer) { SetMonitorUserPointer0(monitor, pointer); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Single GetWindowOpacityDelegate0(IntPtr window);
        private static readonly GetWindowOpacityDelegate0 GetWindowOpacity0 = ExternLibrary.GetStaticProc<GetWindowOpacityDelegate0>("glfwGetWindowOpacity");
        public static Single GetWindowOpacity(IntPtr window) { return GetWindowOpacity0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetWindowOpacityDelegate0(IntPtr window, Single opacity);
        private static readonly SetWindowOpacityDelegate0 SetWindowOpacity0 = ExternLibrary.GetStaticProc<SetWindowOpacityDelegate0>("glfwSetWindowOpacity");
        public static void SetWindowOpacity(IntPtr window, Single opacity) { SetWindowOpacity0(window, opacity); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void WindowHintStringDelegate0(Hint hint, Byte[] value);
        private static readonly WindowHintStringDelegate0 WindowHintString0 = ExternLibrary.GetStaticProc<WindowHintStringDelegate0>("glfwWindowHintString");
        public static void WindowHintString(Hint hint, Byte[] value) { WindowHintString0(hint, value); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void GetWindowContentScaleDelegate0(IntPtr window, out Single xScale, out Single yScale);
        private static readonly GetWindowContentScaleDelegate0 GetWindowContentScale0 = ExternLibrary.GetStaticProc<GetWindowContentScaleDelegate0>("glfwGetWindowContentScale");
        public static void GetWindowContentScale(IntPtr window, out Single xScale, out Single yScale) { GetWindowContentScale0(window, out xScale, out yScale); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void RequestWindowAttentionDelegate0(IntPtr window);
        private static readonly RequestWindowAttentionDelegate0 RequestWindowAttention0 = ExternLibrary.GetStaticProc<RequestWindowAttentionDelegate0>("glfwRequestWindowAttention");
        public static void RequestWindowAttention(IntPtr window) { RequestWindowAttention0(window); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean RawMouseMotionSupportedDelegate0();
        private static readonly RawMouseMotionSupportedDelegate0 RawMouseMotionSupported0 = ExternLibrary.GetStaticProc<RawMouseMotionSupportedDelegate0>("glfwRawMouseMotionSupported");
        public static Boolean RawMouseMotionSupported() { return RawMouseMotionSupported0(); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate WindowMaximizedCallback SetWindowMaximizeCallbackDelegate0(IntPtr window, WindowMaximizedCallback cb);
        private static readonly SetWindowMaximizeCallbackDelegate0 SetWindowMaximizeCallback0 = ExternLibrary.GetStaticProc<SetWindowMaximizeCallbackDelegate0>("glfwSetWindowMaximizeCallback");
        public static WindowMaximizedCallback SetWindowMaximizeCallback(IntPtr window, WindowMaximizedCallback cb) { return SetWindowMaximizeCallback0(window, cb); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate WindowContentsScaleCallback SetWindowContentScaleCallbackDelegate0(IntPtr window, WindowContentsScaleCallback cb);
        private static readonly SetWindowContentScaleCallbackDelegate0 SetWindowContentScaleCallback0 = ExternLibrary.GetStaticProc<SetWindowContentScaleCallbackDelegate0>("glfwSetWindowContentScaleCallback");
        public static WindowContentsScaleCallback SetWindowContentScaleCallback(IntPtr window, WindowContentsScaleCallback cb) { return SetWindowContentScaleCallback0(window, cb); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Int32 GetKeyScanCodeDelegate0(Keys key);
        private static readonly GetKeyScanCodeDelegate0 GetKeyScanCode0 = ExternLibrary.GetStaticProc<GetKeyScanCodeDelegate0>("glfwGetKeyScancode");
        public static Int32 GetKeyScanCode(Keys key) { return GetKeyScanCode0(key); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetWindowAttributeDelegate0(IntPtr window, WindowAttribute attr, Boolean value);
        private static readonly SetWindowAttributeDelegate0 SetWindowAttribute0 = ExternLibrary.GetStaticProc<SetWindowAttributeDelegate0>("glfwSetWindowAttrib");
        public static void SetWindowAttribute(IntPtr window, WindowAttribute attr, Boolean value) { SetWindowAttribute0(window, attr, value); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetJoystickHatsDelegate0(Int32 joystickId, out Int32 count);
        private static readonly GetJoystickHatsDelegate0 GetJoystickHats0 = ExternLibrary.GetStaticProc<GetJoystickHatsDelegate0>("glfwGetJoystickHats");
        private static IntPtr GetJoystickHats(Int32 joystickId, out Int32 count) { return GetJoystickHats0(joystickId, out count); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetJoystickGuidPrivateDelegate0(Int32 joystickId);
        private static readonly GetJoystickGuidPrivateDelegate0 GetJoystickGuidPrivate0 = ExternLibrary.GetStaticProc<GetJoystickGuidPrivateDelegate0>("glfwGetJoystickGUID");
        private static IntPtr GetJoystickGuidPrivate(Int32 joystickId) { return GetJoystickGuidPrivate0(joystickId); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetJoystickUserPointerDelegate0(Int32 joystickId);
        private static readonly GetJoystickUserPointerDelegate0 GetJoystickUserPointer0 = ExternLibrary.GetStaticProc<GetJoystickUserPointerDelegate0>("glfwGetJoystickUserPointer");
        public static IntPtr GetJoystickUserPointer(Int32 joystickId) { return GetJoystickUserPointer0(joystickId); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate void SetJoystickUserPointerDelegate0(Int32 joystickId, IntPtr pointer);
        private static readonly SetJoystickUserPointerDelegate0 SetJoystickUserPointer0 = ExternLibrary.GetStaticProc<SetJoystickUserPointerDelegate0>("glfwSetJoystickUserPointer");
        public static void SetJoystickUserPointer(Int32 joystickId, IntPtr pointer) { SetJoystickUserPointer0(joystickId, pointer); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean JoystickIsGamepadDelegate0(Int32 joystickId);
        private static readonly JoystickIsGamepadDelegate0 JoystickIsGamepad0 = ExternLibrary.GetStaticProc<JoystickIsGamepadDelegate0>("glfwJoystickIsGamepad");
        public static Boolean JoystickIsGamepad(Int32 joystickId) { return JoystickIsGamepad0(joystickId); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean UpdateGamepadMappingsDelegate0(Byte[] mappings);
        private static readonly UpdateGamepadMappingsDelegate0 UpdateGamepadMappings0 = ExternLibrary.GetStaticProc<UpdateGamepadMappingsDelegate0>("glfwUpdateGamepadMappings");
        private static Boolean UpdateGamepadMappings(Byte[] mappings) { return UpdateGamepadMappings0(mappings); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate IntPtr GetGamepadNamePrivateDelegate0(Int32 gamepadId);
        private static readonly GetGamepadNamePrivateDelegate0 GetGamepadNamePrivate0 = ExternLibrary.GetStaticProc<GetGamepadNamePrivateDelegate0>("glfwGetGamepadName");
        private static IntPtr GetGamepadNamePrivate(Int32 gamepadId) { return GetGamepadNamePrivate0(gamepadId); }
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]private delegate Boolean GetGamepadStateDelegate0(Int32 id, out GamePadState state);
        private static readonly GetGamepadStateDelegate0 GetGamepadState0 = ExternLibrary.GetStaticProc<GetGamepadStateDelegate0>("glfwGetGamepadState");
        public static Boolean GetGamepadState(Int32 id, out GamePadState state) { return GetGamepadState0(id, out state); }


    }
}
