using compute.drawing;
using System;
using System.IO;

namespace compute.environment
{
    public interface IAppHost 
    {
        Size Size { get; }
        IntPtr WindowHandle { get; }
        IntPtr InstanceHandle { get; }
        Platform Platform { get; }
        Stream Open(string path);
    }
}
