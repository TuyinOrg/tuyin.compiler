using compute;
using GLFW;

namespace wasi
{
    public static class App
    {
        public static void Lanuch(Surface surface) 
        {
            Windows windows = new Windows("wasi");
            windows.LoadSurface(surface);
            windows.Resume();
            while (!Glfw.WindowShouldClose(windows.InternalWindow))
            {
                if (!windows.Paused)
                {
                    windows.Tick();
                }

                Glfw.PollEvents();
            }

            Glfw.Terminate();
        }
    }
}
