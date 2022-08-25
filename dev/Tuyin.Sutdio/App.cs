using Chromely;
using Chromely.Core;
using Chromely.Core.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Xilium.CefGlue;

namespace Tuyin.Sutdio
{
    internal class App : ChromelyBasicApp
    {
        public Factory Factory { get; }

        public App(IChromelyConfiguration config, IChromelyErrorHandler chromelyErrorHandler) 
        {
            Factory = new Factory(config, chromelyErrorHandler);
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);   
            services.AddSingleton(typeof(IChromelySchemeHandler), x => new Resource(Factory));
        }
    }
}
