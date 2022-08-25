using libtui.utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace FreeType
{
    internal class FreeTypeLibrary : EnviromentLibrary
    {
        public FreeTypeLibrary() : base("FreeType")
        {
        }

        protected override IEnumerable<string> GetLinuxLibraries()
        {
            yield return $@"runtimes\{GetPlatformIdentity()}\native\lib\libfreetype.so";
        }

        protected override IEnumerable<string> GetOSXLibraries()
        {
            yield return $@"runtimes\{GetPlatformIdentity()}\native\lib\libfreetype.dylib";
        }

        protected override IEnumerable<string> GetWindowsLibraries()
        {
            yield return $@"runtimes\{GetPlatformIdentity()}\native\lib\freetype.dll";
        }
    }
}
