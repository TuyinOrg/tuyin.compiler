using Chromely.Core;
using Chromely.Core.Network;

namespace Tuyin.Sutdio
{
    internal class Resource : IChromelySchemeHandler
    {
        public Resource(Factory factory) 
        {
            Name = Const.NAME;
            Scheme = new UrlScheme(Const.NAME, Const.SCHEME, Const.HOST, string.Empty, string.Empty, UrlSchemeType.LocalResource, false);
            HandlerFactory = factory;
            IsCorsEnabled = true;
            IsSecure = false;
        }

        public string Name { get; set; }
        public UrlScheme Scheme { get; set; }

        // Needed for CefSharp
        public object Handler { get; set; }
        public object HandlerFactory { get; set; }
        public bool IsCorsEnabled { get; set; }
        public bool IsSecure { get; set; }
    }
}
