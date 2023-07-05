using Data.Entities;
using DestallMaterials.WheelProtection.DataWorks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net.WebSockets;

namespace Data.Tests;

public class TestEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class TestOwnedString
{
    public string Value { get; set; }

    public TestEntity Owner { get; set; }

    public int OwnerId { get; set; }
}

public class TestDbContext : DbContext
{

    public List<string> Logs { get; } = new List<string>();

    public DbSet<TestEntity> Entities { get; set; }
    public DbSet<TestOwnedString> OwnedStrings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.UseSqlite($@"Data source=test.db").EnableSensitiveDataLogging();
        optionsBuilder.LogTo(Logs.Add);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TestOwnedString>().HasKey(ts => new
        {
            ts.OwnerId,
            ts.Value
        });
    }

    internal async Task<int> RenewSearchIndexAsync(IEnumerable<KeyValuePair<long, string>> items)
    {
        var itemsToPaste = items.SelectMany(kv => kv.Value.GetAllChunks().Distinct().Select(ch => new
        {
            Key = kv.Key,
            Value = ch
        }));


        string sql = $@"delete from {nameof(TestDbContext.OwnedStrings)}
where OwnerId in ({string.Join(',', items.Select(kv => kv.Key))});

insert into {nameof(TestDbContext.OwnedStrings)}
(OwnerId, Value)
values {string.Join(',', itemsToPaste.Select(i => $"({i.Key}, '{i.Value}')"))};";


        var result = await this.Database.ExecuteSqlRawAsync(sql);

        return result;
    }
}

public class DbSchema
{

    [Test]
    public async Task MustCreateDbSuccessfully()
    {
        using var dbContext = new TestDbContext();

        await dbContext.Database.EnsureDeletedAsync();

        await dbContext.Database.EnsureCreatedAsync();
        await dbContext.Database.EnsureDeletedAsync();
    }

    [Test]
    public async Task MustAddEntitiesCorrectly()
    {
        using var dbContext = new TestDbContext();

        dbContext.SavedChanges += async (e, o) =>
        {
            var renewSearchPattern = dbContext.ChangeTracker
                .Entries<TestEntity>()
                .Select(kv => new KeyValuePair<long, string>(kv.Entity.Id, kv.Entity.Name));

            await dbContext.RenewSearchIndexAsync(renewSearchPattern);
        };

        var commands = new List<string>();

        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();

        var entities = new string[]
        {
            "Samir",
            "Anton",
            "Muraddin",
            "Arthadoxonicos",
            "Azzazzello",
            "Cro",
        }.Select((n, i) => new TestEntity
        {
            Id = i + 1,
            Name = n
        }).ToList();

        dbContext.AddRange(entities);

        await dbContext.SaveChangesAsync();

        const string pattern = "r";

        var foundEntitiesQuery = dbContext.Entities.Where(e => dbContext.OwnedStrings.Any(os => os.OwnerId == e.Id && os.Value == pattern));

        var results = await foundEntitiesQuery.ToArrayAsync();

    }
}