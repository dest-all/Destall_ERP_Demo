using Data.Entities;
using Data.Entities.Registers;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using DestallMaterials.WheelProtection.DataWorks;
using DestallMaterials.WheelProtection.Extensions.Enumerables;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.EntityFramework;

[TargetDbContext]
public abstract partial class ApplicationDbContext : DbContext
{
    IReadOnlyList<ReferrableEntity>? _entitiesToReindex;

    public DbSet<OwnedString> OwnedStrings { get; set; }

    public static long SuperUserId { get; internal set; }

    protected ApplicationDbContext(DbContextOptions options) : base(options)
    {
        SavingChanges += ApplicationDbContext_SavingChanges;
        SavedChanges += async (o, e) => await RenewSearchPatternsForTrackedEntitiesAsync();
    }

    public async Task RenewSearchPatternsAsync(IEnumerable<ReferrableEntity> referrableEntities)
    {
        var kvs = referrableEntities.SelectMany(GetChunksToStore);
        await RenewSearchIndexAsync(kvs);
    }

    async Task RenewSearchPatternsForTrackedEntitiesAsync()
    {
        if (_entitiesToReindex.IsEmpty())
        {
            return; 
        }
        try
        {
            await RenewSearchPatternsAsync(_entitiesToReindex);
        }
        catch (Exception ex)
        {
            _entitiesToReindex = null;
        }
    }

    static IEnumerable<KeyValuePair<long, string>> GetChunksToStore(ReferrableEntity e) 
        => e.Representation.ToLower().GetAllChunks().Distinct().Select(ch => new KeyValuePair<long, string>(e.Id, ch));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.AutoExpandDataScheme();

        modelBuilder.Entity<AccountEntry>().HasIndex(ae => ae.ActorId);
        modelBuilder.Entity<AccountEntry>().HasIndex(ae => ae.BaseDocumentId);
        modelBuilder.Entity<AccountEntry>().HasIndex(ae => ae.Status);

        modelBuilder.Entity<StockEntry>().HasIndex(ae => ae.ActorId);
        modelBuilder.Entity<StockEntry>().HasIndex(ae => ae.Status);

        modelBuilder.Entity<OwnedString>().HasKey(e => new 
        {
            e.Id,
            e.Value
        });
    }

    public void SubscribeForDisposition(Action<ApplicationDbContext> onDisposed)
    {
        _onDisposed = onDisposed;
    }

    Action<ApplicationDbContext> _onDisposed;

    public override void Dispose()
    {
        base.Dispose();
        if (_onDisposed != null)
        {
            _onDisposed(this);
        }
    }

    protected ApplicationDbContext()
    {
        SavingChanges += ApplicationDbContext_SavingChanges;
        SavedChanges += async (o,e) => await RenewSearchPatternsForTrackedEntitiesAsync();
    }

    private void ApplicationDbContext_SavingChanges(object? sender, SavingChangesEventArgs e)
    {
        var entitiesToReindex = new List<ReferrableEntity>();
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is Entity entity)
            {
                if (entity is ReferrableEntity referrableEntity)
                {
                    var oldRepresentation = referrableEntity.Representation;
                    entity.BeforeSave();
                    if (oldRepresentation != referrableEntity.Representation)
                    {
                        entitiesToReindex.Add(referrableEntity);
                    }
                }
                else 
                {
                    entity.BeforeSave();
                }
            }
        }
        _entitiesToReindex = entitiesToReindex;
    }

    public abstract Task<int> ExecuteDeleteAsync<TEntity>(IQueryable<TEntity> entities);

    protected abstract Task<int> ExecuteRawSqlAsync(string sql);


    async Task<int> RenewSearchIndexAsync(IEnumerable<KeyValuePair<long, string>> items)
    {
        if (items.IsEmpty())
        {
            return 0;
        }

        string sql = $@"delete from {nameof(OwnedStrings)}
where {nameof(OwnedString.Id)} in ({string.Join(',', items.Select(kv => kv.Key))});

insert into {nameof(OwnedStrings)}
({nameof(OwnedString.Id)}, Value)
values {string.Join(',', items.Select(i => $"({i.Key}, '{i.Value}')"))};";


        var result = await this.ExecuteRawSqlAsync(sql);

        return result;
    }
}