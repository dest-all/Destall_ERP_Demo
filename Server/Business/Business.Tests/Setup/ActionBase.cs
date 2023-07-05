using Business.ActionPoints;
using Business.Extensions;
using Business.Services;
using Data.EntityFramework;
using Data.EntityFramework.Repository;
using Data.Repository;
using Data.Samples;
using Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Protocol;

namespace Business.Tests.Setup
{
    public abstract class ActionBase
    {
        protected static IBusinessActionsNet Business { get; private set; }
        protected static IExecutionContext Context { get; private set; }

        static FactoriesSet.FacilitySetup _facilitySetup;

        protected static async Task<IDataRepository> GetRepositoryAsync()
            => await _facilitySetup.GetRepositoryAsync(Context);

        class TestExecutionContext : IExecutionContext
        {
            public long OperationId { get; init; }

            public string SessionKey { get; init; }

            public string Language => Protocol.Constants.Languages.English;
        }

        protected static ApplicationDbContext DbContext;

        class Config
        {
            public string Db { get; set; }
        }

        //[OneTimeSetUp]
        static ActionBase()
        {
            var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText("setup\\config.json"));

            string dbName = config.Db;

            var dbContext = SqliteDbContext.ForFileName(dbName);

            DbContext = dbContext;

            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var entities = InitialData.Entities;

            DbContext.AddRange(entities);
            DbContext.SaveChanges();

            dbContext.EnsureSuperUserPresenceAsync().Wait();

            DbContext = dbContext;

            var release = dbContext.ChangeTracker.Clear;

            _facilitySetup = new FactoriesSet.FacilitySetup()
            {
                GetHttpClientAsync = async () => new(),
                GetRepositoryAsync = async (context) => new DataRepository(dbContext, release),
                CreateLogger = type => new TestLogger()
            };

            var businessFactory = new BusinessNetFactory(_facilitySetup);

            var noContextBusiness = businessFactory.CreateWithoutContext();

            var session = noContextBusiness.Administration.SessionsManagement.EnsureCreatedSessionForUser.Call(1);

            Context = new TestExecutionContext
            {
                OperationId = 1,
                SessionKey = session.Key
            };

            Business = businessFactory.Create(Context);

            dbContext.Database.BeginTransaction();
        }

        //[OneTimeTearDown]
        //public void Dispose()
        //{
        //    DbContext.Database.RollbackTransaction();
        //    Business.Dispose();
        //    DbContext.Dispose();
        //}
    }
}