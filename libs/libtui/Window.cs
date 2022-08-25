using GLFW;
using libtui.controls;
using libtui.drawing;
using libtui.utils;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Path = System.IO.Path;

namespace libtui
{
    class Window
    {
        private readonly NativeWindow mWindow;

        internal void Load(IControl control, IGraphicDevice graphicDevice)
        {
            _control = control;
            _attached = control;
            _foucsed = control;
            _graphicDevice = graphicDevice;
        }

        private string _title;
        private bool _running;
        private int _frameCount;
        private float _timeElapsed;
        private IControl _control;
        private IControl _attached;
        private IControl _foucsed;
        private IGraphicDevice _graphicDevice;
        private Point _downPosition;

        internal GLFW.Window InternalWindow => mWindow.Window;

        internal IControl Focused => _foucsed;

        public string Title 
        {
            get { return _title; }
            set { _title = value; }
        }

        public IntPtr WindowHandle => GetNativeWindow(mWindow.Window);

        public IntPtr InstanceHandle => Process.GetCurrentProcess().Handle;

        public Platform Platform => GetPlatform(mWindow);

        public Size Size 
        {
            get { return mWindow.Size; }
            set 
            {
                if (mWindow.Size != value)
                    mWindow.Size = value;
            }
        }

        public bool Paused => !_running;

        public int Width => Size.Width;

        public int Height => Size.Height;

        public Stream Open(string path) =>  new FileStream(Platform == Platform.Win32 ? path : Path.Combine("bin", path), FileMode.Open, FileAccess.Read);

        static Window() 
        {
            if (!GLFW.Vulkan.IsSupported)
                throw new NotSupportedException("not support vulkan.");

            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, true);
            Glfw.WindowHint(Hint.Decorated, true);

#if OPENGL
            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Gl.Import(Glfw.GetProcAddress);
#else
            Glfw.WindowHint(Hint.ClientApi, ClientApi.None);
#endif
        }

        public Window()
            : this(string.Empty, true) 
        {
        }

        public Window(string title, bool enableEvent)
        {
            _running = false;

            _title = title;
            mWindow = new NativeWindow();
            mWindow.Initialize();
            if (enableEvent)
            {
                mWindow.SizeChanged += _window_SizeChanged;
                mWindow.MouseButton += _window_MouseButton;
                mWindow.MouseEnter += _window_MouseEnter;
                mWindow.MouseLeave += _window_MouseLeave;
                mWindow.MouseMoved += _window_MouseMoved;
                mWindow.MouseScroll += _window_MouseScroll;
                mWindow.KeyPress += _window_KeyPress;
                mWindow.KeyRelease += _window_KeyRelease;
                mWindow.KeyRepeat += _window_KeyRepeat;
                mWindow.CharacterInput += _window_CharacterInput;
                mWindow.FileDrop += _window_FileDrop;
                mWindow.FocusChanged += _window_FocusChanged;
                mWindow.MaximizeChanged += MWindow_MaximizeChanged;
                mWindow.Disposed += _window_Disposed;
            }
        }

        internal void Tick(Timer timer)
        {
            CalculateFrameRateStats(timer);

            if (_graphicDevice != null)
            {
                _control.Paint(new PaintEventArgs(_graphicDevice, null));
                _graphicDevice?.Flush();
            }
        }

        public void Resume() => _running = true;

        public void Pause() => _running = false;

        private void MWindow_MaximizeChanged(object sender, MaximizeEventArgs e)
        {
        }

        private void _window_FocusChanged(object sender, EventArgs e)
        {
            if (mWindow.IsFocused)
            {
                _foucsed.OnGetFocus();
                App.FireActivated();
            }
            else 
            {
                _foucsed.OnLostFocus();
                App.FireDeactivate();
            }
        }

        private void _window_FileDrop(object sender, FileDropEventArgs e)
        {
            _attached.FileDrop(e.Filenames);
        }

        private void _window_CharacterInput(object sender, CharEventArgs e)
        {
            _foucsed.OnCharInput(e);
        }

        private void _window_KeyRepeat(object sender, KeyEventArgs e)
        {
            _foucsed.OnKeyPress(e);
        }

        private void _window_KeyRelease(object sender, KeyEventArgs e)
        {
            _foucsed.OnKeyUp(e);
        }

        private void _window_KeyPress(object sender, KeyEventArgs e)
        {
            _foucsed.OnKeyDown(e);
        }

        private void _window_MouseScroll(object sender, MouseEventArgs e)
        {
            _foucsed.OnMouseWheel(ResetLocation(_foucsed, e));
        }

        private void _window_MouseMoved(object sender, MouseEventArgs e)
        {
            App.FireMouseMove(e);
            var c = FindControl(_control, (int)e.X, (int)e.Y);
            if (_attached != c)
            {
                _attached.OnMouseLeave(ResetLocation(_attached, e));
                _attached = c;
                _attached?.OnMosueEnter(ResetLocation(_attached, e));
            }

            _attached?.OnMouseMove(ResetLocation(_attached, e));
        }

        private void _window_MouseLeave(object sender, MouseEventArgs e)
        {
            _control.OnMouseLeave(e);
        }

        private void _window_MouseEnter(object sender, MouseEventArgs e)
        {
            _control.OnMosueEnter(e);
        }

        private void _window_MouseButton(object sender, MouseEventArgs e)
        {
            if (e.Action == InputState.Press)
            {
                App.FireMouseButtonDown(e);
                _downPosition = e.Location;
                _attached?.OnMouseDown(ResetLocation(_attached, e));

                if ((_attached?.AllowFocus ?? false) && _foucsed != _attached)
                {
                    _foucsed.OnLostFocus();
                    _foucsed = _attached;
                    _foucsed.OnGetFocus();
                }
            }
            else if (e.Action == InputState.Release)
            {
                App.FireMouseButtonUp(e);
                _attached?.OnMouseUp(ResetLocation(_attached, e));
                if (MathTools.GetDistance(_downPosition, e.Location) < 2)
                {
                    App.FireMouseButtonClick(e);
                    _attached?.OnMouseClick(ResetLocation(_attached, e));
                }
            }
        }

        private void _window_SizeChanged(object sender, SizeChangeEventArgs e)
        {
            _control.OnSizeChanged(e);
        }

        private void _window_Disposed(object sender, EventArgs e)
        {
            Pause();
        }

        private IControl FindControl(IControl curr, int x, int y) 
        {
            if (curr.Bounds.Contains(x, y))
            {
                if (curr is IContainerControl cc)
                {
                    var sub = cc.FindChildren(x, y);
                    if (sub != null) 
                    {
                        return sub;
                    }
                }

                return curr;
            }

            return null;
        }

        private MouseEventArgs ResetLocation(IControl control, MouseEventArgs e) 
        {
            return new MouseEventArgs(e.X - control.Location.X, e.Y - control.Location.Y, e.Delta, e.Action, e.Button, e.Modifiers, e.Tag, e.Rectangle);
        }

        private void CalculateFrameRateStats(Timer timer)
        {
            _frameCount++;

            if (timer.TotalTime - _timeElapsed >= 1.0f)
            {
                float fps = _frameCount;
                float mspf = 1000.0f / fps;

                mWindow.Title = $"{Title}    Fps: {fps}    Mspf: {mspf}";
                _frameCount = 0;
                _timeElapsed += 1.0f;
            }
        }

        private static Platform GetPlatform(NativeWindow nativeWindow) 
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return libtui.Platform.Win32;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // XServer
                return libtui.Platform.Linux;
                // Wayland
                //return Native.GetEglContext(nativeWindow);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return libtui.Platform.MacOS;
            }

            throw new PlatformNotSupportedException();
        }

        private static IntPtr GetNativeWindow(GLFW.Window window) 
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Native.GetWin32Window(window);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // XServer
                return Native.GetGLXWindow(window);
                // Wayland
                //return Native.GetEglContext(nativeWindow);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return Native.GetCocoaWindow(window);
            }

            throw new PlatformNotSupportedException();
        }

        private static IntPtr GetNativeContext(GLFW.Window window)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Native.GetWglContext(window);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // XServer
                return Native.GetGLXContext(window);
                // Wayland
                //return Native.GetEglContext(nativeWindow);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return Native.GetNSGLContext(window);
            }

            throw new PlatformNotSupportedException();
        }

        public void Dispose()
        {
            _running = false;
            mWindow.Dispose();
        }
    }
}
