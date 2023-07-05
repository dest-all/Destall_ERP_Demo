using Data.EntityFramework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Sqlite;

public class SqliteDbContext : ApplicationDbContext
{
    static string _defaultConnectionString = $@"Data source={AppDomain.CurrentDomain.BaseDirectory}\db.db";
    readonly string _connectionString;
    
    protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
    {
        base.OnConfiguring(dbContextOptionsBuilder);
        dbContextOptionsBuilder.UseSqlite(_connectionString);

#if DEBUG
        dbContextOptionsBuilder.EnableSensitiveDataLogging();
        dbContextOptionsBuilder.LogTo(message => 
        {
            if (message.Contains(" Executing DbCommand"))
            {
                message = message.Split(" Executing DbCommand").Last();
                return;
            }
        });
#endif

    }

    public static SqliteDbContext ForFileName(string fileName) => ForFilePath($@"{AppDomain.CurrentDomain.BaseDirectory}\{fileName}");

    public static SqliteDbContext ForFilePath(string filePath) => new SqliteDbContext($"Data source={filePath}");

    public override Task<int> ExecuteDeleteAsync<TEntity>(IQueryable<TEntity> entities) => entities.ExecuteDeleteAsync();

    protected override Task<int> ExecuteRawSqlAsync(string sql)
        => Database.ExecuteSqlRawAsync(sql);


    public SqliteDbContext(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SqliteDbContext() : this(_defaultConnectionString)
    {
    }

}

public class InMemorySqliteDbContext : SqliteDbContext
{
    public const string InMemoryConnectionString = "Filename=:memory:";

    readonly SqliteConnection _connection;

    public InMemorySqliteDbContext() : base(InMemoryConnectionString)
    {
        var cnn = new SqliteConnection(InMemoryConnectionString);

        try
        {
            cnn.Open();
        }
        catch { }
        _connection = cnn;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
    {
        base.OnConfiguring(dbContextOptionsBuilder);
        dbContextOptionsBuilder.UseSqlite(_connection);
    }
}
