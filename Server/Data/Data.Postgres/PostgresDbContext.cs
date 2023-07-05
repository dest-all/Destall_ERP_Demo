using Data.EntityFramework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Postgres
{
    public sealed class PostgresDbContext : ApplicationDbContext
    {
        public override Task<int> ExecuteDeleteAsync<TEntity>(IQueryable<TEntity> entities)
            => entities.ExecuteDeleteAsync();

        protected override Task<int> ExecuteRawSqlAsync(string sql)
            => this.Database.ExecuteSqlRawAsync(sql);

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            base.OnConfiguring(dbContextOptionsBuilder);
            dbContextOptionsBuilder.EnableDetailedErrors();
            dbContextOptionsBuilder.EnableSensitiveDataLogging();
            dbContextOptionsBuilder.UseNpgsql("Host=localhost;Port=5500;Database=blazoronrail;Username=postgres;Password=124816");
        }
    }
}
