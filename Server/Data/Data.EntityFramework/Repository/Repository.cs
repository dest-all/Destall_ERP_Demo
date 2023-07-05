using Data.Entities;
using Data.Entities.Documents.Trade;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;

namespace Data.EntityFramework.Repository
{
    public class DbContextRepository : IRepository
    {
        protected readonly ApplicationDbContext _context;
        readonly Action _release;

        public bool IsDisposed { get; private set; }

        public DbContextRepository(ApplicationDbContext context, Action release)
        {
            _context = context;
            _release = release;
        }

        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }
            GC.SuppressFinalize(this);
            IsDisposed = true;
            _release();
        }

        public async Task CreateAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity
        {
            _context.ChangeTracker.Clear();
            _context.AddRange(entities);
            try
            {
                var ids = entities.Select(e => e.EnsureValidId()).ToArray();
                await _context.SaveChangesAsync();
            }
            finally
            {
                _context.ChangeTracker.Clear();
            }
        }

        public async Task UpdateAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity
        {
            _context.ChangeTracker.Clear();
            await _context.AddRangeAsync(entities);

#if DEBUG
            var addedIds = new List<long>();
#endif

            try
            {
                foreach (var entry in _context.ChangeTracker.Entries<Entity>())
                {
                    var entity = entry.Entity;
                    if (entity.Id > 0)
                    {
                        entry.State = EntityState.Modified;
                    }
#if DEBUG
                    else if (entity is IncomingOrderLine && entry.State == EntityState.Added)
                    {
                        addedIds.Add(entity.Id);
                    }
#endif
                }
                await _context.SaveChangesAsync();
            }
#if DEBUG
            catch (Exception ex)
            {
                _context.ChangeTracker.Clear();
                var conflictingLines = await _context.IncomingOrderLines.Where(iol => addedIds.Contains(iol.Id)).ToArrayAsync();
                var a = 10;
            }
#endif
            finally
            {
                _context.ChangeTracker.Clear();
            }
        }

        public async Task<TEntity> SaveAsync<TEntity>(TEntity entity) where TEntity : Entity
        {
            _context.ChangeTracker.Clear();
            entity.EnsureValidId();
            _context.Add(entity);
            try
            {
                await _context.SaveChangesAsync();
                _context.Update(entity);
            }
            finally
            {
                _context.ChangeTracker.Clear();
            }
            return entity;
        }

        public async Task<TEntity[]> SaveAsync<TEntity>(ICollection<TEntity> entities) where TEntity : Entity
        {
            _context.ChangeTracker.Clear();
            foreach (var entity in entities)
            {
                entity.EnsureValidId();
            }
            _context.AddRange(entities);
            try
            {
                await _context.SaveChangesAsync();
                var result = entities.ToArray();
                _context.UpdateRange(result);
                return result;
            }
            finally 
            {
                _context.ChangeTracker.Clear();
            }

        }

        public virtual IQueryable<TSet> Set<TSet>() where TSet : class => _context.Set<TSet>().AsNoTracking();

        public virtual IEnumerable<IQueryable<T>> Sets<T>() where T : class
        {
            yield return Set<T>();
        }

        public async Task RunInTransactionAsync(Func<Task> action)
        {
            if (_context.Database.CurrentTransaction is not null)
            {
                await action();
                return;
            }
            IDbContextTransaction transaction = null;
            try
            {
                transaction = await _context.Database.BeginTransactionAsync();
                await action();
                await transaction.CommitAsync();
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                transaction?.Dispose();
            }
        }

        public async Task<TResult> RunInTransactionAsync<TResult>(Func<Task<TResult>> func)
        {
            if (_context.Database.CurrentTransaction is not null)
            {
                return await func();
            }
            IDbContextTransaction transaction = null;
            try
            {
                transaction = await _context.Database.BeginTransactionAsync();
                var result = await func();
                await transaction.CommitAsync();
                return result;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
            finally
            {
                transaction?.Dispose();
            }
            
        }

        public Task<int> DeleteAsync<TEntity>(IQueryable<TEntity> set) where TEntity : Entity
            => _context.ExecuteDeleteAsync(set);
    }
}
