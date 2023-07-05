using BlazorApp;
using Client.Communication.ServerEmbedded;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Client.Communication;
using Data.Sqlite;
using MudBlazor.Services;
using Client.Communication.Extensions;
using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Data.Sqlite.Wasm;
using Microsoft.JSInterop;
using DestallMaterials.WheelProtection.Executions;
using Business.Extensions;
using System.Runtime.CompilerServices;
using Client.Web.Application;
using Client.Web.View.Services;
using LocalStore;
using DestallMaterials.Blazor.Services.UI;
using LocalStore.Browser;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<Client.Web.Application.Application>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");



//var businessFactory = await BusinessSource.CreateWebAsseApplicatiosFactoryAsync();

//Client.Communication.IBusinessServerActionInvokersNet client = ServerEmbeddedClientFactory.Create(businessFactory.Create, out var clientConfigurator);

//await client.OpenActions.SessionActions.StartSession.CallAsync(new StartSessionRequestModel
//{
//    Login = "TechAdmin",
//    Password = "1"
//});

//builder.Services.AddSingleton(client);

//builder.Services.AddSqliteWasmDbContextFactory<SqliteWasmDbContext>(options =>
//{
//    options.UseSqlite("db.sqlite3");
//});

CallConfigurator? callConfigurator = null;

builder.Services.AddMudServices();

builder.Services.AddDbContextFactory<WasmSqliteDbContext>(options => options.UseSqlite("Data Source=db.sqlite3"));

var optionsBuilder = new DbContextOptionsBuilder<WasmSqliteDbContext>();

optionsBuilder.UseSqlite($"Data Source={SynchronizedDbContextFactory.dbFilename}");

var dbContext = new WasmSqliteDbContext(optionsBuilder.Options);

var dbContextFactory = async () => dbContext;

builder.Services.AddSynchronizingDataFactory(dbContextFactory);

builder.Services.AddSingleton(serviceProvider =>
{
    var dbContextFactory = serviceProvider.GetRequiredService<IApplicationDbContextFactory>();

    Task createSuperUser = null;

    var businessFactory = BusinessSource.CreateWebAssemblyBusinessFactory(
        async () => await Run.MeasureExecutionTimeAsync(
            async () => 
            {
                Console.WriteLine($"Starting creating DbContext.");
                var result = await dbContextFactory.CreateAsync();
                createSuperUser = createSuperUser ?? result.EnsureSuperUserPresenceAsync();
                await createSuperUser;
                return result;
            }, 
            t => Console.WriteLine($"DbContext created in {t.Milliseconds} ms.")
        )
    );

    var business = businessFactory.CreateWithoutContext();
    business.Administration.SessionsManagement.OpenFullAccessSessionForTesting.Call("1");

    var result = ServerEmbeddedClientFactory.Create(context => businessFactory.Create(context), out var embeddedCallConfigurator);

    callConfigurator = embeddedCallConfigurator;
    callConfigurator.AssignSessionKeyHeader(() => "1");

    return result;
});


builder.Services.ConfigureBlazorOnRailWasmApp(
    sp => sp.GetRequiredService<IBusinessServerActionInvokersNet>(),
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
        sp.GetRequiredService<IBusinessServerActionInvokersNet>(),
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
            var result = new FeedManager(sp.GetRequiredService<IBusinessServerActionInvokersNet>(), store, sp.GetRequiredService<IUiManipulator>());
            //LoadInitialDataForFeedAsync(client, store, result);

            return result;
        }
    );


builder.Services.AddSingleton(serviceProvider =>
{
    if (callConfigurator is null)
    {
        serviceProvider.GetRequiredService<IBusinessServerActionInvokersNet>();
    }
    return callConfigurator ?? throw new InvalidOperationException();
});

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
