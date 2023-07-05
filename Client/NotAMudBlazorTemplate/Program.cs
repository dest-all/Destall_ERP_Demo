using Business.ActionPoints;
using Client.Communication;
using Client.Communication.Extensions;
using Client.Communication.Grpc;
using Client.Communication.Json;
using Client.Web.Application;
using Client.Web.View.Services;
using DestallMaterials.Blazor.Services.UI;
using DestallMaterials.WheelProtection.Extensions.Strings;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using Grpc.Net.Client;
using Grpc.Net.Client.Web;
using LocalStore;
using LocalStore.Browser;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.JSInterop;
using MudBlazor.Services;
using Protocol.Models;
using Protocol.Models.DataHolders;
using Protocol.Models.Filters;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Application>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

const string serverLocalHostAddress = "http://localhost:5056";

string remoteAddress = builder.Configuration["backendUrl"];

string serverAddress = remoteAddress ?? serverLocalHostAddress;

CallConfigurator? callConfigurator = null;

var client = JsonClientFactory.CreateJsonWebClient(serverAddress, out var jsonConfigurator);
callConfigurator = jsonConfigurator;

//var client = GrpcClientFactory.CreateForAddress(serverAddress, out var grpcCallConfigurator);
//callConfigurator = grpcCallConfigurator;




//callConfigurator.AssignSessionKeyHeader(() => 1);

builder.Services.ConfigureBlazorOnRailWasmApp(
    sp => {
        var config = sp.GetRequiredService<IConfiguration>();

        var logger = sp.GetRequiredService<ILogger>();
        logger.LogInformation($"Arguments were {args.Join(",\n\t")}");
        logger.LogInformation($"Using address {serverAddress} to connect to backend.");

        return client;
    },
    sp => new BrowserLocalStorage(sp.GetRequiredService<IJSRuntime>()),
    sp => sp.GetRequiredService<ILoggerFactory>().CreateLogger("WASM"),
    sp =>
    {
        if (callConfigurator is null)
        {
            sp.GetRequiredService<IBusinessServerActionInvokersNet>();
        }
        return callConfigurator ?? throw new InvalidOperationException();
    },
    sp => {
        var result = new AccountManager(
        client,
        sp.GetRequiredService<ILocalStore>(),
        sp.GetRequiredService<ILogger>(),
        sp.GetRequiredService<CallConfigurator>());
#if DEBUG
        result.AuthoriseNewSessionAsync("techadmin", "");
#endif

        return result;
    },
        sp =>
        {
            var store = sp.GetRequiredService<ILocalStore>();
            var result = new FeedManager(client, store, sp.GetRequiredService<IUiManipulator>());
            //LoadInitialDataForFeedAsync(client, store, result);

            return result;
        }
    );

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

await builder.Build().RunAsync();
