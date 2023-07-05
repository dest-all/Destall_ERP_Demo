using Client.Communication;
using Client.Communication.Extensions;
using Client.Communication.Json;
using Client.Web.Application;
using Client.Web.View.Services;
using LocalStore.LiteDb;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace Desktop
{
    public static class MauiProgram
    {
        const string serverLocalHostAddress = "http://localhost:5056";
    
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

            builder.Services.AddMudServices();

#if DEBUG
		builder.Services.AddBlazorWebViewDeveloperTools();
		builder.Logging.AddDebug();
#endif

            var client = JsonClientFactory.CreateJsonWebClient(serverLocalHostAddress, out var configurator);

            builder.Services.AddSingleton(sp => new LiteDbLocalStore());

            builder.Services.ConfigureBlazorOnRailWasmApp(
                    sp => client, 
                    sp => sp.GetRequiredService<LiteDbLocalStore>(), 
                    sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("MAUI"), 
                    sp => configurator.AssignSessionKeyHeader(() => 1)
                );

            return builder.Build();
        }
    }
}