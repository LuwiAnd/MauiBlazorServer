using Microsoft.Extensions.Logging;
//using Microsoft.AspNetCore.Components;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Components.Web;
using SharedRazorClassLibrary.Services;
using System;

namespace MauiBlazorHybridApp
{
    public static class MauiProgram
    {
        public static IServiceProvider Services { get; private set; } = null!;
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            // I Maui ska allt köras lokalt, så ingen server-side rendering behövs
            //builder.Services.AddRazorComponents()
            //    .AddInteractiveServerComponents(); // Denna används däremot i webbprojektet.
            //builder.Services.AddRazorComponents();

            builder.Services.AddSingleton<PendingLinkService>();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif
            var app = builder.Build();
            Services = app.Services;

            //return builder.Build();
            return app;
        }
    }
}
