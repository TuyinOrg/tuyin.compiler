using Chromely.Browser;
using Chromely.Core;
using Chromely.Core.Configuration;
using Xilium.CefGlue;

namespace Tuyin.Sutdio
{
    internal class Factory : DefaultResourceSchemeHandlerFactory
    {
        public Factory(IChromelyConfiguration config, IChromelyErrorHandler chromelyErrorHandler) 
            : base(config, chromelyErrorHandler)
        {
        }

        protected override CefResourceHandler Create(CefBrowser browser, CefFrame frame, string schemeName, CefRequest request)
        {
            //var fileName = Const.BASEROOT + request.Url.Substring(Const.BASEURL.Length, request.Url.Length - Const.BASEURL.Length);

            return base.Create(browser, frame, schemeName, request);
        }
    }
}
