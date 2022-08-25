using GLFW;
using libtui.controls;
using libtui.drawing;
using libtui.utils;
using System;
using System.Threading;

namespace libtui
{
    public delegate void GlobalEvent();
    public delegate void GlobalMouseEvent(MouseEventArgs e);

    public static class App
    {
        private static Window mWindow;
        private static Surface mSuface;
        private static DateTime mLastUpdateDateTime;
        private static readonly Timer mTimer = new Timer();

        public static void PerformanceTest() 
        {
            mWindow = new Window("Performance Test", false);
            var suface = new ComputeParticlesApp();
            suface.Initialize(mWindow);

            mWindow.Resume();
            mTimer.Reset();
            while (!Glfw.WindowShouldClose(mWindow.InternalWindow))
            {
                mTimer.Tick();
                if (!mWindow.Paused)
                {
                    mWindow.Tick(mTimer);
                    suface.Tick(mTimer);
                }

                Glfw.PollEvents();
            }

            Glfw.Terminate();
        }

        public static void Lanuch(string title, IControl control) 
        {
            Context = control;

            mWindow = new Window(title, true);
            mSuface = new Surface();
            mSuface.Initialize(mWindow);

            mWindow.Load(control, new HardwareGraphicsDevice(mSuface));
            mWindow.Resume();
            mTimer.Reset();
            while (!Glfw.WindowShouldClose(mWindow.InternalWindow))
            {
                mTimer.Tick();
                if (!mWindow.Paused)
                {
                    mWindow.Tick(mTimer);
                    var now = DateTime.Now;
                    if ((now - mLastUpdateDateTime).Milliseconds >= Settings.SufaceUpdateTime)
                    {
                        mSuface.Tick(mTimer);
                        mLastUpdateDateTime = now;
                    }
                }

                Glfw.PollEvents();
                if (Settings.LoopSleepTime > 0)
                    Thread.Sleep(Settings.LoopSleepTime);
            }

            Glfw.Terminate();
        }

        public static void ApplySkin(Skin skin) 
        {
            Skin = skin;
            Context.ApplySkin(skin);
        }

        internal static ContentManager Content => mSuface.Content;

        internal static IControl Focused => mWindow.Focused;

        internal static IControl Context { get; private set; }

        internal static Skin Skin { get; private set; }

        internal static string Title
        {
            get { return mWindow.Title; }
            set { mWindow.Title = value; }
        }

        public static CancellationTokenSource SetTimeout(int interval, Action<object> callback)
        {
            throw new NotImplementedException();
        }

        public static CancellationTokenSource SetInterval(int interval, Action<object> callback) 
        {
            throw new NotImplementedException();
        }

        internal static void FireMouseMove(MouseEventArgs e) 
        {
            MouseMove?.Invoke(e);
        }

        internal static void FireMouseButtonDown(MouseEventArgs e)
        {
            MouseButtonDown?.Invoke(e);
        }

        internal static void FireMouseButtonUp(MouseEventArgs e)
        {
            MouseButtonUp?.Invoke(e);
        }

        internal static void FireMouseButtonClick(MouseEventArgs e)
        {
            MouseButtonClick?.Invoke(e);
        }

        internal static void FireActivated() 
        {
            mTimer.Reset();
            mWindow.Resume();
            Activated?.Invoke();
        }

        internal static void FireDeactivate() 
        {
            mTimer.Stop();
            mWindow.Pause();
            Deactivate?.Invoke();
        }

        public static event GlobalMouseEvent MouseMove;
        public static event GlobalMouseEvent MouseButtonDown;
        public static event GlobalMouseEvent MouseButtonUp;
        public static event GlobalMouseEvent MouseButtonClick;
        public static event GlobalEvent Activated;
        public static event GlobalEvent Deactivate;

    }
}
