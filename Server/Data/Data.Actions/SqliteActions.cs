using Data.Entities;
using Data.EntityFramework.Repository;
using Data.Sqlite;
using DestallMaterials.WheelProtection.Extensions.Enumerables;
using Microsoft.EntityFrameworkCore;

namespace Data.Actions
{
    public class SqliteActions
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task ReindexSearchPatterns()
        {
            const string dbPath = @"C:\Users\igor_\Documents\Projects\Blazor_OnRail\Web.Server\bin\Debug\net7.0\db.db";

            using var dbContext = SqliteDbContext.ForFilePath(dbPath);

            using var repo = new DataRepository(dbContext, () => { });

            var sets = repo.Sets<ReferrableEntity>().ToArray();

            var processedBatches = await sets.ToArrayAsync(s => ProcessBatchAsync(dbContext, s));

            var processedOverall = processedBatches.Sum();
        }

        static async Task<int> ProcessBatchAsync(SqliteDbContext dbContext, IQueryable<ReferrableEntity> set)
        {
            const int pace = 5000;

            int i = 0;

            ReferrableEntity[] entities;

            int processed = 0;

            do
            {
                entities = await set.Skip(i * pace).Take(pace).ToArrayAsync();
                await dbContext.RenewSearchPatternsAsync(entities);
                i++;
                processed += entities.Length;
            }
            while (entities.HasContent());

            return processed;
        }
    }
}