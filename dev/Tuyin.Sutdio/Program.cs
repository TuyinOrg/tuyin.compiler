// See https://aka.ms/new-console-template for more information
using Chromely.Core;
using Chromely.Core.Configuration;
using Chromely.Core.Network;
using Chromely.NativeHosts;
using Tuyin.Sutdio;
using Window = Tuyin.Sutdio.Window;

// create a configuration with OS-specific defaults
var config = DefaultConfiguration.CreateForRuntimePlatform();

// your configuration
config.UrlSchemes.Add(new UrlScheme(Const.NAME, Const.SCHEME, Const.HOST, Const.BASEROOT, string.Empty, UrlSchemeType.FolderResource, false));
config.CustomSettings.Add("enable-webgl-draft-extensions", "1");
config.CustomSettings.Add("enable-gpu", "1");
config.CustomSettings.Add("enable-webgl", "1");
config.WindowOptions.Title = "My Awesome Chromely App!";
config.StartUrl = $"{Const.SCHEME}://{Const.HOST}/index.html";

var app = new App(config, new Report());

ThreadApt.STA();
AppBuilder
    .Create(args)
    .UseConfig<IChromelyConfiguration>(config)
    .UseWindow<Window>()
    .UseApp<App>(app)
    .Build()
    .Run();
