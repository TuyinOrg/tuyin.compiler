using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using JetBrains.Annotations;

#pragma warning disable 0419

namespace GLFW
{
    /// <summary>
    ///     The base class the vast majority of the GLFW functions, excluding only Vulkan and native platform specific
    ///     functions.
    /// </summary>
    [SuppressUnmanagedCodeSecurity]
    internal static partial class Glfw 
    {
        #region Fields and Constants

        /// <summary>
        ///     The native library name,
        ///     <para>For Unix users using an installed version of GLFW, this needs refactored to <c>glfw</c>.</para>
        /// </summary>
#if Windows
        public const string LIBRARY = "glfw3";
#elif OSX
        public const string LIBRARY = "libglfw.3"; // mac
#else
        public const string LIBRARY = "glfw";
#endif

        private static readonly ErrorCallback errorCallback = GlfwError;

        #endregion

        #region Constructors

        static Glfw()
        {
            Init();
            SetErrorCallback(errorCallback);
        }

        #endregion

        /// <summary>
        ///     Returns and clears the error code of the last error that occurred on the calling thread, and optionally
        ///     a description of it.
        ///     <para>
        ///         If no error has occurred since the last call, it returns <see cref="ErrorCode.None" /> and the
        ///         description pointer is set to <c>null</c>.
        ///     </para>
        /// </summary>
        /// <param name="description">The description string, or <c>null</c> if there is no error.</param>
        /// <returns>The error code.</returns>
        public static ErrorCode GetError(out string description)
        {
            var code = GetErrorPrivate(out var ptr);
            description = code == ErrorCode.None ? null : Util.PtrToStringUTF8(ptr);
            return code;
        }

        /// <summary>
        ///     Helper function to call <see cref="WindowHintString(Hint, byte[])" /> with UTF-8 encoding.
        /// </summary>
        /// <param name="hint">The window hit to set.</param>
        /// <param name="value">The new value of the window hint.</param>
        // ReSharper disable once InconsistentNaming
        public static void WindowHintStringUTF8(Hint hint, string value)
        {
            WindowHintString(hint, Encoding.UTF8.GetBytes(value));
        }

        /// <summary>
        ///     Helper function to call <see cref="WindowHintString(Hint, byte[])" /> with ASCII encoding.
        /// </summary>
        /// <param name="hint">The window hit to set.</param>
        /// <param name="value">The new value of the window hint.</param>
        // ReSharper disable once InconsistentNaming
        public static void WindowHintStringASCII(Hint hint, string value)
        {
            WindowHintString(hint, Encoding.ASCII.GetBytes(value));
        }

        /// <summary>
        ///     Returns the state of all hats of the specified joystick as a bitmask.
        /// </summary>
        /// <param name="joystickId">The joystick to query.</param>
        /// <returns>A bitmask enumeration containing the state of the joystick hats.</returns>
        public static Hat GetJoystickHats(int joystickId)
        {
            var hat = Hat.Centered;
            var ptr = GetJoystickHats(joystickId, out var count);
            for (var i = 0; i < count; i++)
            {
                var value = Marshal.ReadByte(ptr, i);
                hat |= (Hat) value;
            }

            return hat;
        }

        /// <summary>
        ///     Returns the SDL compatible GUID, as a hexadecimal string, of the specified joystick.
        ///     <para>
        ///         The GUID is what connects a joystick to a gamepad mapping. A connected joystick will always have a GUID even
        ///         if there is no gamepad mapping assigned to it.
        ///     </para>
        /// </summary>
        /// <param name="joystickId">The joystick to query.</param>
        /// <returns>The GUID of the joystick, or <c>null</c> if the joystick is not present or an error occurred.</returns>
        public static string GetJoystickGuid(int joystickId)
        {
            var ptr = GetJoystickGuidPrivate(joystickId);
            return ptr == IntPtr.Zero ? null : Util.PtrToStringUTF8(ptr);
        }

        /// <summary>
        ///     Parses the specified string and updates the internal list with any gamepad mappings it finds.
        ///     <para>
        ///         This string may contain either a single gamepad mapping or many mappings separated by newlines. The parser
        ///         supports the full format of the SDL <c>gamecontrollerdb.txt</c> source file including empty lines and comments.
        ///     </para>
        /// </summary>
        /// <param name="mappings">The string containing the gamepad mappings.</param>
        /// <returns><c>true</c> if successful, or <c>false</c> if an error occurred.</returns>
        public static bool UpdateGamepadMappings(string mappings)
        {
            return UpdateGamepadMappings(Encoding.ASCII.GetBytes(mappings));
        }

        /// <summary>
        ///     Returns the human-readable name of the gamepad from the gamepad mapping assigned to the specified joystick.
        /// </summary>
        /// <param name="gamepadId">The joystick to query.</param>
        /// <returns>
        ///     The name of the gamepad, or <c>null</c> if the joystick is not present, does not have a mapping or an error
        ///     occurred.
        /// </returns>
        public static string GetGamepadName(int gamepadId)
        {
            var ptr = GetGamepadNamePrivate(gamepadId);
            return ptr == IntPtr.Zero ? null : Util.PtrToStringUTF8(ptr);
        }

        #region Properties

        /// <summary>
        ///     Gets the window whose OpenGL or OpenGL ES context is current on the calling thread, or <see cref="Window.None" />
        ///     if no context is current.
        /// </summary>
        /// <value>
        ///     The current context.
        /// </value>
        public static Window CurrentContext => GetCurrentContext();

        /// <summary>
        ///     Gets an array of handles for all currently connected monitors.
        ///     <para>The primary monitor is always first in the array.</para>
        /// </summary>
        /// <value>
        ///     The monitors.
        /// </value>
        public static Monitor[] Monitors
        {
            get
            {
                var ptr = GetMonitors(out var count);
                var monitors = new Monitor[count];
                var offset = 0;
                for (var i = 0; i < count; i++, offset += IntPtr.Size)
                {
                    monitors[i] = Marshal.PtrToStructure<Monitor>(ptr + offset);
                }

                return monitors;
            }
        }

        /// <summary>
        ///     Gets the primary monitor. This is usually the monitor where elements like the task bar or global menu bar are
        ///     located.
        /// </summary>
        /// <value>
        ///     The primary monitor, or <see cref="Monitor.None" /> if no monitors were found or if an error occurred.
        /// </value>
        public static Monitor PrimaryMonitor => GetPrimaryMonitor();

        /// <summary>
        ///     Gets or sets the value of the GLFW timer.
        ///     <para>
        ///         The resolution of the timer is system dependent, but is usually on the order of a few micro- or nanoseconds.
        ///         It uses the highest-resolution monotonic time source on each supported platform.
        ///     </para>
        /// </summary>
        /// <value>
        ///     The time.
        /// </value>
        public static double Time
        {
            get => GetTime();
            set => SetTime(value);
        }

        /// <summary>
        ///     Gets the frequency, in Hz, of the raw timer.
        /// </summary>
        /// <value>
        ///     The frequency of the timer, in Hz, or zero if an error occurred.
        /// </value>
        public static ulong TimerFrequency => GetTimerFrequency();

        /// <summary>
        ///     Gets the current value of the raw timer, measured in 1 / frequency seconds.
        /// </summary>
        /// <value>
        ///     The timer value.
        /// </value>
        public static ulong TimerValue => GetTimerValue();

        /// <summary>
        ///     Gets the version of the native GLFW library.
        /// </summary>
        /// <value>
        ///     The version.
        /// </value>
        public static Version Version
        {
            get
            {
                GetVersion(out var major, out var minor, out var revision);
                return new Version(major, minor, revision);
            }
        }

        /// <summary>
        ///     Gets the compile-time generated version string of the GLFW library binary.
        ///     <para>It describes the version, platform, compiler and any platform-specific compile-time options.</para>
        /// </summary>
        /// <value>
        ///     The version string.
        /// </value>
        public static string VersionString => Util.PtrToStringUTF8(GetVersionString());

        #endregion

        #region Methods

        /// <summary>
        ///     This function creates a window and its associated OpenGL or OpenGL ES context. Most of the options controlling how
        ///     the window and its context should be created are specified with window hints.
        /// </summary>
        /// <param name="width">The desired width, in screen coordinates, of the window. This must be greater than zero.</param>
        /// <param name="height">The desired height, in screen coordinates, of the window. This must be greater than zero.</param>
        /// <param name="title">The initial window title.</param>
        /// <param name="monitor">The monitor to use for full screen mode, or <see cref="Monitor.None" /> for windowed mode.</param>
        /// <param name="share">
        ///     A window instance whose context to share resources with, or <see cref="Window.None" /> to not share
        ///     resources..
        /// </param>
        /// <returns>The created window, or <see cref="Window.None" /> if an error occurred.</returns>
        public static Window CreateWindow(int width, int height, [NotNull] string title, Monitor monitor, Window share)
        {
            return CreateWindow(width, height, Encoding.UTF8.GetBytes(title), monitor, share);
        }

        /// <summary>
        ///     Gets the client API.
        /// </summary>
        /// <param name="window">A window instance.</param>
        /// <returns>The client API.</returns>
        public static ClientApi GetClientApi(Window window)
        {
            return (ClientApi) GetWindowAttribute(window, (int) ContextAttributes.ClientApi);
        }

        /// <summary>
        ///     Gets the contents of the system clipboard, if it contains or is convertible to a UTF-8 encoded
        ///     string.
        /// </summary>
        /// <param name="window">A window instance.</param>
        /// <returns>The contents of the clipboard as a UTF-8 encoded string, or <c>null</c> if an error occurred.</returns>
        [NotNull]
        public static string GetClipboardString(Window window)
        {
            return Util.PtrToStringUTF8(GetClipboardStringInternal(window));
        }

        /// <summary>
        ///     Gets the API used to create the context of the specified window.
        /// </summary>
        /// <param name="window">A window instance.</param>
        /// <returns>The API used to create the context.</returns>
        public static ContextApi GetContextCreationApi(Window window)
        {
            return (ContextApi) GetWindowAttribute(window, (int) ContextAttributes.ContextCreationApi);
        }

        /// <summary>
        ///     Gets the context version of the specified window.
        /// </summary>
        /// <param name="window">A window instance.</param>
        /// <returns>The context version.</returns>
        public static Version GetContextVersion(Window window)
        {
            GetContextVersion(window, out var major, out var minor, out var revision);
            return new Version(major, minor, revision);
        }

        /// <summary>
        ///     Gets whether the specified API extension is supported by the current OpenGL or OpenGL ES context.
        ///     <para>It searches both for client API extension and context creation API extensions.</para>
        /// </summary>
        /// <param name="extension">The extension name.</param>
        /// <returns><c>true</c> if the extension is supported; otherwise <c>false</c>.</returns>
        public static bool GetExtensionSupported(string extension)
        {
            return GetExtensionSupported(Encoding.ASCII.GetBytes(extension));
        }

        /// <summary>
        ///     Gets the current gamma ramp of the specified monitor.
        /// </summary>
        /// <param name="monitor">The monitor to query.</param>
        /// <returns>The current gamma ramp, or empty structure if an error occurred.</returns>
        public static GammaRamp GetGammaRamp(Monitor monitor)
        {
            return (GammaRamp) Marshal.PtrToStructure<GammaRampInternal>(GetGammaRampInternal(monitor));
        }

        /// <summary>
        ///     Gets value indicating if specified window is using a debug context.
        /// </summary>
        /// <param name="window">A window instance.</param>
        /// <returns><c>true</c> if window context is debug context, otherwise <c>false</c>.</returns>
        public static bool GetIsDebugContext(Window window)
        {
            return GetWindowAttribute(window, (int) ContextAttributes.OpenglDebugContext) == (int) Constants.True;
        }

        /// <summary>
        ///     Gets value indicating if specified window is using a forward compatible context.
        /// </summary>
        /// <param name="window">A window instance.</param>
        /// <returns><c>true</c> if window context is forward compatible, otherwise <c>false</c>.</returns>
        public static bool GetIsForwardCompatible(Window window)
        {
            return GetWindowAttribute(window, (int) ContextAttributes.OpenglForwardCompat) == (int) Constants.True;
        }

        /// <summary>
        ///     Gets the values of all axes of the specified joystick. Each element in the array is a value
        ///     between -1.0 and 1.0.
        ///     <para>
        ///         Querying a joystick slot with no device present is not an error, but will return an empty array. Call
        ///         <see cref="JoystickPresent" /> to check device presence.
        ///     </para>
        /// </summary>
        /// <param name="joystick">The joystick to query.</param>
        /// <returns>An array of axes values.</returns>
        public static float[] GetJoystickAxes(Joystick joystick)
        {
            var ptr = GetJoystickAxes(joystick, out var count);
            var axes = new float[count];
            if (count > 0)
                Marshal.Copy(ptr, axes, 0, count);
            return axes;
        }

        /// <summary>
        ///     Gets the state of all buttons of the specified joystick.
        /// </summary>
        /// <param name="joystick">The joystick to query.</param>
        /// <returns>An array of values, either <see cref="InputState.Press" /> and <see cref="InputState.Release" />.</returns>
        public static InputState[] GetJoystickButtons(Joystick joystick)
        {
            var ptr = GetJoystickButtons(joystick, out var count);
            var states = new InputState[count];
            for (var i = 0; i < count; i++)
                states[i] = (InputState) Marshal.ReadByte(ptr, i);
            return states;
        }

        /// <summary>
        ///     Gets the name of the specified joystick.
        ///     <para>
        ///         Querying a joystick slot with no device present is not an error. <see cref="JoystickPresent" /> to check
        ///         device presence.
        ///     </para>
        /// </summary>
        /// <param name="joystick">The joystick to query.</param>
        /// <returns>The name of the joystick, or <c>null</c> if the joystick is not present or an error occurred.</returns>
        public static string GetJoystickName(Joystick joystick)
        {
            return Util.PtrToStringUTF8(GetJoystickNameInternal(joystick));
        }

        /// <summary>
        ///     Gets the localized name of the specified printable key. This is intended for displaying key
        ///     bindings to the user.
        ///     <para>
        ///         If the key is <see cref="Keys.Unknown" />, the scancode is used instead, otherwise the scancode is ignored.
        ///         If a non-printable key or (if the key is <see cref="Keys.Unknown" />) a scancode that maps to a non-printable
        ///         key is specified, this function returns NULL.
        ///     </para>
        /// </summary>
        /// <param name="key">The key to query.</param>
        /// <param name="scanCode">The scancode of the key to query.</param>
        /// <returns>The localized name of the key.</returns>
        public static string GetKeyName(Keys key, int scanCode)
        {
            return Util.PtrToStringUTF8(GetKeyNameInternal(key, scanCode));
        }

        /// <summary>
        ///     Gets a human-readable name, encoded as UTF-8, of the specified monitor.
        ///     <para>
        ///         The name typically reflects the make and model of the monitor and is not guaranteed to be unique among the
        ///         connected monitors.
        ///     </para>
        /// </summary>
        /// <param name="monitor">The monitor to query.</param>
        /// <returns>The name of the monitor, or <c>null</c> if an error occurred.</returns>
        public static string GetMonitorName(Monitor monitor)
        {
            return Util.PtrToStringUTF8(GetMonitorNameInternal(monitor));
        }

        /// <summary>
        ///     Gets the address of the specified OpenGL or OpenGL ES core or extension function, if it is
        ///     supported by the current context.
        ///     This function does not apply to Vulkan. If you are rendering with Vulkan, use
        ///     <see cref="Vulkan.GetInstanceProcAddress" /> instead.
        /// </summary>
        /// <param name="procName">Name of the function.</param>
        /// <returns>The address of the function, or <see cref="IntPtr.Zero" /> if an error occurred.</returns>
        public static IntPtr GetProcAddress(string procName)
        {
            return GetProcAddress(Encoding.ASCII.GetBytes(procName));
        }

        /// <summary>
        ///     Gets the profile of the specified window.
        /// </summary>
        /// <param name="window">A window instance.</param>
        /// <returns>Profile of the window.</returns>
        public static Profile GetProfile(Window window)
        {
            return (Profile) GetWindowAttribute(window, (int) ContextAttributes.OpenglProfile);
        }

        /// <summary>
        ///     Gets the robustness value of the specified window.
        /// </summary>
        /// <param name="window">A window instance.</param>
        /// <returns>Current set value of the robustness.</returns>
        public static Robustness GetRobustness(Window window)
        {
            return (Robustness) GetWindowAttribute(window, (int) ContextAttributes.ContextRobustness);
        }

        /// <summary>
        ///     Gets the current video mode of the specified monitor.
        ///     <para>
        ///         If you have created a full screen window for that monitor, the return value will depend on whether that
        ///         window is iconified.
        ///     </para>
        /// </summary>
        /// <param name="monitor">The monitor to query.</param>
        /// <returns>The current mode of the monitor, or <c>null</c> if an error occurred.</returns>
        public static VideoMode GetVideoMode(Monitor monitor)
        {
            var ptr = GetVideoModeInternal(monitor);
            return Marshal.PtrToStructure<VideoMode>(ptr);
        }

        /// <summary>
        ///     Gets an array of all video modes supported by the specified monitor.
        ///     <para>
        ///         The returned array is sorted in ascending order, first by color bit depth (the sum of all channel depths) and
        ///         then by resolution area (the product of width and height).
        ///     </para>
        /// </summary>
        /// <param name="monitor">The monitor to query.</param>
        /// <returns>The array of video modes.</returns>
        public static VideoMode[] GetVideoModes(Monitor monitor)
        {
            var pointer = GetVideoModes(monitor, out var count);
            var modes = new VideoMode[count];
            for (var i = 0; i < count; i++, pointer += Marshal.SizeOf<VideoMode>())
                modes[i] = Marshal.PtrToStructure<VideoMode>(pointer);
            return modes;
        }

        /// <summary>
        ///     Gets the value of an attribute of the specified window or its OpenGL or OpenGL ES context.
        /// </summary>
        /// <param name="window">A window instance.</param>
        /// <param name="attribute">The window attribute whose value to return.</param>
        /// <returns>The value of the attribute, or zero if an error occurred.</returns>
        public static bool GetWindowAttribute(Window window, WindowAttribute attribute)
        {
            return GetWindowAttribute(window, (int) attribute) == (int) Constants.True;
        }

        /// <summary>
        ///     Sets the system clipboard to the specified string.
        /// </summary>
        /// <param name="window">A window instance.</param>
        /// <param name="str">The string to set to the clipboard.</param>
        public static void SetClipboardString(Window window, string str)
        {
            SetClipboardString(window, Encoding.UTF8.GetBytes(str));
        }

        /// <summary>
        ///     Sets the window title, encoded as UTF-8, of the specified window.
        /// </summary>
        /// <param name="window">A window instance.</param>
        /// <param name="title">The title to set.</param>
        public static void SetWindowTitle(Window window, string title)
        {
            SetWindowTitle(window, Encoding.UTF8.GetBytes(title));
        }

        /// <summary>
        ///     Sets hints for the next call to <see cref="CreateWindow" />. The hints, once set, retain their values
        ///     until changed by a call to <see cref="WindowHint(Hint, int)" /> or <see cref="DefaultWindowHints" />, or until the
        ///     library is
        ///     terminated.
        ///     <para>
        ///         This function does not check whether the specified hint values are valid. If you set hints to invalid values
        ///         this will instead be reported by the next call to <see cref="CreateWindow" />.
        ///     </para>
        /// </summary>
        /// <param name="hint">The hint.</param>
        /// <param name="value">The value.</param>
        public static void WindowHint(Hint hint, bool value)
        {
            WindowHint(hint, value ? Constants.True : Constants.False);
        }

        /// <summary>
        ///     Sets hints for the next call to <see cref="CreateWindow" />. The hints, once set, retain their values
        ///     until changed by a call to <see cref="WindowHint(Hint, int)" /> or <see cref="DefaultWindowHints" />, or until the
        ///     library is
        ///     terminated.
        ///     <para>
        ///         This function does not check whether the specified hint values are valid. If you set hints to invalid values
        ///         this will instead be reported by the next call to <see cref="CreateWindow" />.
        ///     </para>
        /// </summary>
        /// <param name="hint">The hint.</param>
        /// <param name="value">The value.</param>
        public static void WindowHint(Hint hint, ClientApi value) { WindowHint(hint, (int) value); }

        /// <summary>
        ///     Sets hints for the next call to <see cref="CreateWindow" />. The hints, once set, retain their values
        ///     until changed by a call to <see cref="WindowHint(Hint, int)" /> or <see cref="DefaultWindowHints" />, or until the
        ///     library is
        ///     terminated.
        ///     <para>
        ///         This function does not check whether the specified hint values are valid. If you set hints to invalid values
        ///         this will instead be reported by the next call to <see cref="CreateWindow" />.
        ///     </para>
        /// </summary>
        /// <param name="hint">The hint.</param>
        /// <param name="value">The value.</param>
        public static void WindowHint(Hint hint, Constants value) { WindowHint(hint, (int) value); }

        /// <summary>
        ///     Sets hints for the next call to <see cref="CreateWindow" />. The hints, once set, retain their values
        ///     until changed by a call to <see cref="WindowHint(Hint, int)" /> or <see cref="DefaultWindowHints" />, or until the
        ///     library is
        ///     terminated.
        ///     <para>
        ///         This function does not check whether the specified hint values are valid. If you set hints to invalid values
        ///         this will instead be reported by the next call to <see cref="CreateWindow" />.
        ///     </para>
        /// </summary>
        /// <param name="hint">The hint.</param>
        /// <param name="value">The value.</param>
        public static void WindowHint(Hint hint, ContextApi value) { WindowHint(hint, (int) value); }

        /// <summary>
        ///     Sets hints for the next call to <see cref="CreateWindow" />. The hints, once set, retain their values
        ///     until changed by a call to <see cref="WindowHint(Hint, int)" /> or <see cref="DefaultWindowHints" />, or until the
        ///     library is
        ///     terminated.
        ///     <para>
        ///         This function does not check whether the specified hint values are valid. If you set hints to invalid values
        ///         this will instead be reported by the next call to <see cref="CreateWindow" />.
        ///     </para>
        /// </summary>
        /// <param name="hint">The hint.</param>
        /// <param name="value">The value.</param>
        public static void WindowHint(Hint hint, Robustness value) { WindowHint(hint, (int) value); }

        /// <summary>
        ///     Sets hints for the next call to <see cref="CreateWindow" />. The hints, once set, retain their values
        ///     until changed by a call to <see cref="WindowHint(Hint, int)" /> or <see cref="DefaultWindowHints" />, or until the
        ///     library is
        ///     terminated.
        ///     <para>
        ///         This function does not check whether the specified hint values are valid. If you set hints to invalid values
        ///         this will instead be reported by the next call to <see cref="CreateWindow" />.
        ///     </para>
        /// </summary>
        /// <param name="hint">The hint.</param>
        /// <param name="value">The value.</param>
        public static void WindowHint(Hint hint, Profile value) { WindowHint(hint, (int) value); }

        /// <summary>
        ///     Sets hints for the next call to <see cref="CreateWindow" />. The hints, once set, retain their values
        ///     until changed by a call to <see cref="WindowHint(Hint, int)" /> or <see cref="DefaultWindowHints" />, or until the
        ///     library is
        ///     terminated.
        ///     <para>
        ///         This function does not check whether the specified hint values are valid. If you set hints to invalid values
        ///         this will instead be reported by the next call to <see cref="CreateWindow" />.
        ///     </para>
        /// </summary>
        /// <param name="hint">The hint.</param>
        /// <param name="value">The value.</param>
        public static void WindowHint(Hint hint, ReleaseBehavior value) { WindowHint(hint, (int) value); }

        private static void GetContextVersion(Window window, out int major, out int minor, out int revision)
        {
            major = GetWindowAttribute(window, (int) ContextAttributes.ContextVersionMajor);
            minor = GetWindowAttribute(window, (int) ContextAttributes.ContextVersionMinor);
            revision = GetWindowAttribute(window, (int) ContextAttributes.ContextVersionRevision);
        }

        private static void GlfwError(ErrorCode code, IntPtr message)
        {
            throw new Exception(Util.PtrToStringUTF8(message));
        }

        #endregion
    }
}