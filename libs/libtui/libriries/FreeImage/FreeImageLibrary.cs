using libtui.utils;
using System.Collections.Generic;

namespace FreeImage
{
    internal class FreeImageLibrary : EnviromentLibrary
    {
        public FreeImageLibrary() : base("FreeImage")
        {
        }

        protected override IEnumerable<string> GetLinuxLibraries()
        {
            yield return $@"runtimes\{GetPlatformIdentity()}\native\lib\libfreeimage.so";
        }

        protected override IEnumerable<string> GetOSXLibraries()
        {
            yield return $@"runtimes\{GetPlatformIdentity()}\native\lib\libfreeimage.dylib";
        }

        protected override IEnumerable<string> GetWindowsLibraries()
        {
            yield return $@"runtimes\{GetPlatformIdentity()}\native\lib\freeimage.dll";
        }
    }
}
