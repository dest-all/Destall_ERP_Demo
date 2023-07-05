using Client.Communication;
using Client.Communication.Extensions;
using Client.Communication.Json;
using Client.Web.Application;
using Client.Web.View.Services;
using LocalStore;
using LocalStore.Browser;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using VisualClient.JsonProtocol;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Application>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

const string serverLocalHostAddress = "http://localhost:5056";

CallConfigurator? callConfigurator = null;
builder.Services.AddSingleton(serviceProvider =>
{
    var result = JsonClientFactory.CreateJsonWebClient(serverLocalHostAddress, out var jsonConfigurator);
    callConfigurator = jsonConfigurator;
    callConfigurator.AssignSessionKeyHeader(() => 1);
    return result;
});

builder.Services.AddSingleton(serviceProvider =>
{
    if (callConfigurator is null)
    {
        serviceProvider.GetRequiredService<IBusinessServerActionInvokersNet>();
    }
    return callConfigurator ?? throw new InvalidOperationException();
});

builder.Services.AddSingleton<ILocalStore, BrowserLocalStorage>();
builder.Services.AddSingleton<IDomAccessor, JsDomAccessor>();
builder.Services.AddSingleton<IAccountManager, AccountManager>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddMudServices();

await builder.Build().RunAsync();
