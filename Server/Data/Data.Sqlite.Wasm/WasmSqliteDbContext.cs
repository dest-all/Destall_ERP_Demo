using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Sqlite.Wasm;

public sealed class WasmSqliteDbContext : ApplicationDbContext
{
    public WasmSqliteDbContext(DbContextOptions<WasmSqliteDbContext> options) : base(options)
    { 
    }

    public override Task<int> ExecuteDeleteAsync<TEntity>(IQueryable<TEntity> entities)
        => entities.ExecuteDeleteAsync<TEntity>();

    protected override Task<int> ExecuteRawSqlAsync(string sql)
        => Database.ExecuteSqlRawAsync(sql);
}
