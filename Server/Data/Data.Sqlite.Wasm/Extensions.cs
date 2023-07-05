using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;

namespace Data.Sqlite.Wasm
{
    public static class Extensions
    {
        public static IServiceCollection AddSynchronizingDataFactory(
            this IServiceCollection service, Func<Task<WasmSqliteDbContext>> dbContextFactory) =>
            service.AddSingleton<IApplicationDbContextFactory, SynchronizedDbContextFactory>(
                    serviceProvider =>
                    {
                        var js = serviceProvider.GetRequiredService<IJSRuntime>();
                        return new SynchronizedDbContextFactory(js, dbContextFactory);
                    }
                );
    }
}
