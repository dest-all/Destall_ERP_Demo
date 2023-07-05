using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using Client.Web.Application;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Application>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var invoker = 

builder.Services.ConfigureBlazorOnRailWasmApp();
builder.Services.AddMudServices();

await builder.Build().RunAsync();
