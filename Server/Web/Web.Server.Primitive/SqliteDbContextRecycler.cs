using Data.EntityFramework;
using Data.Sqlite;
using DestallMaterials.WheelProtection.Queues;

namespace Web.Server.Asp;

public sealed class SqliteDbContextRecycler : Recycler<ApplicationDbContext>
{
    public SqliteDbContextRecycler() : base(1)
    {
    }

    protected override ApplicationDbContext CreateNew() => new SqliteDbContext();

    protected override void Discard(ApplicationDbContext item) => item.Dispose();

    protected override bool IsWell(ApplicationDbContext item) => true;
}