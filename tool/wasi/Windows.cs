using compute;
using compute.drawing;
using compute.environment;
using GLFW;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using Path = System.IO.Path;

namespace wasi
{
    class Windows : IAppHost
    {
        private readonly List<Surface> _surfaces = new List<Surface>();
        private readonly Timer _timer = new Timer();
        private readonly NativeWindow _window;

        private string _title;
        private bool _running;
        private int _frameCount;
        private float _timeElapsed;

        internal Window InternalWindow => _window.Window;

        public string Title 
        {
            get { return _title; }
            set { _title = value; }
        }

        public IntPtr WindowHandle => GetNativeWindow(_window.Window);

        public IntPtr InstanceHandle => Process.GetCurrentProcess().Handle;

        public Platform Platform => GetPlatform(_window);

        public Size Size 
        {
            get { return _window.Size; }
            set 
            {
                if (_window.Size != value)
                    _window.Size = value;
            }
        }

        public bool Paused => !_running;

        public int Width => Size.Width;

        public int Height => Size.Height;

        public Stream Open(string path) =>  new FileStream(Platform == Platform.Win32 ? path : Path.Combine("bin", path), FileMode.Open, FileAccess.Read);

        static Windows() 
        {
            if (!Vulkan.IsSupported)
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

        public Windows()
            : this(string.Empty) 
        {
        }

        public Windows(string title)
        {
            _title = title;
            _window = new NativeWindow();
            _window.Initialize();
            _window.SizeChanged += _window_SizeChanged;
            _window.Disposed += _window_Disposed;
        }

        internal void Tick()
        {
            _timer.Tick();
            CalculateFrameRateStats();

            for (var i = 0; i < _surfaces.Count; i++)
                _surfaces[i].Tick(_timer);
        }

        public void Resume() 
        {
            _running = true;
            _timer.Reset();
        }

        public void Pause() 
        {
            _running = false;
        }

        private void _window_Disposed(object sender, EventArgs e)
        {
            Pause();
        }

        private void _window_SizeChanged(object sender, SizeChangeEventArgs e)
        {
            for (var i = 0; i < _surfaces.Count; i++)
                _surfaces[i].Resize();
        }

        private void CalculateFrameRateStats()
        {
            _frameCount++;

            if (_timer.TotalTime - _timeElapsed >= 1.0f)
            {
                float fps = _frameCount;
                float mspf = 1000.0f / fps;

                _window.Title = $"{Title}    Fps: {fps}    Mspf: {mspf}";

                // Reset for next average.
                _frameCount = 0;
                _timeElapsed += 1.0f;
            }
        }

        private static Platform GetPlatform(NativeWindow nativeWindow) 
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return Platform.Win32;
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                // XServer
                return Platform.Linux;
                // Wayland
                //return Native.GetEglContext(nativeWindow);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return Platform.MacOS;
            }

            throw new PlatformNotSupportedException();
        }

        private static IntPtr GetNativeWindow(Window window) 
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
            _window.Dispose();

            for(var i = 0; i < _surfaces.Count; i++)
                _surfaces[i].Dispose();
        }

        public void LoadSurface(Surface surface)
        {
            surface.Disposed += Surface_Disposed;
            surface.Initialize(this);

            _surfaces.Add(surface);
        }

        private void Surface_Disposed(object sender, EventArgs e)
        {
            var surface = sender as Surface;
            surface.Disposed -= Surface_Disposed;
            _surfaces.Remove(surface);
        }
    }
}
