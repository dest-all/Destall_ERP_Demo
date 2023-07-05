using Business.ActionPoints;
using Business.Extensions;
using Business.Services;
using Client.Communication.ServerEmbedded;
using Data.EntityFramework;
using Data.EntityFramework.Repository;
using Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Protocol;
using System.Threading;

namespace BlazorApp
{
    file class TestLogger : ILogger
    {
        class Disposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
        public IDisposable BeginScope<TState>(TState state) where TState : notnull => new Disposable();

        public bool IsEnabled(LogLevel logLevel) => true;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) => Console.WriteLine(formatter(state, exception));
    }

    public class BusinessSource
    {
        public static BusinessNetFactory CreateWebAssemblyBusinessFactory(Func<Task<ApplicationDbContext>> getDbContext)
        {
            Task? preparations = null;
            Func<Task<ApplicationDbContext>> factoryWithPreparation = async () =>
            {
                var dbContext = await getDbContext();

                preparations = preparations ?? dbContext.Database.EnsureCreatedAsync().ContinueWith(t => dbContext.EnsureSuperUserPresenceAsync());

                await preparations;

                return dbContext;
            };

            FactoriesSet.FacilitySetup facilitySetup = new FactoriesSet.FacilitySetup()
            {
                GetHttpClientAsync = async () => new(),
                GetRepositoryAsync = async () => 
                {
                    var dbContext = await factoryWithPreparation();
                    var release = () => dbContext.ChangeTracker.Clear();
                    return new DataRepository(dbContext, release);
                },
                CreateLogger = type => new TestLogger()
            };

            var businessFactory = new BusinessNetFactory(facilitySetup);

            return businessFactory;
        }
    }
}
