using Chromely.Core;
using Chromely.Core.Configuration;
using Chromely.Core.Host;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tuyin.Sutdio
{
    internal class Window : Chromely.Window
    {
        public Window(IChromelyNativeHost nativeHost, IChromelyConfiguration config, ChromelyHandlersResolver handlersResolver) 
            : base(nativeHost, config, handlersResolver)
        {
        }
    }
}
