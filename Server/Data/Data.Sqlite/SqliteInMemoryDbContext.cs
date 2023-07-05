using Data.EntityFramework;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Data.Sqlite;

public class SqliteInMemoryDbContext : ApplicationDbContext
{
    const string _connectionString = "Filename=:memory:";
    readonly SqliteConnection _connection;

    SqliteInMemoryDbContext(SqliteConnection connection)
    {
        _connection = connection;
    }

    public static async Task<SqliteInMemoryDbContext> CreateAsync()
    {
        var stopwatch = Stopwatch.StartNew();

        try
        {
            var connection = new SqliteConnection(_connectionString);
            await connection.OpenAsync();

            var result = new SqliteInMemoryDbContext(connection);

            await result.Database.EnsureCreatedAsync();

            return result;
        }
        finally
        {
            stopwatch.Stop();
            Console.WriteLine($"Creating DB context took {stopwatch.Elapsed}.");
        }
    }

    public override Task<int> ExecuteDeleteAsync<TEntity>(IQueryable<TEntity> entities) => entities.ExecuteDeleteAsync();

    protected override Task<int> ExecuteRawSqlAsync(string sql)
        => Database.ExecuteSqlRawAsync(sql);

    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        base.OnConfiguring(builder);
        builder.UseSqlite(_connection);
    }
}
