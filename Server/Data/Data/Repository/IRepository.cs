using Data.Entities;
using DestallMaterials.WheelProtection.Extensions.Objects;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public interface IRepository : IDisposable
    {
        IQueryable<TSet> Set<TSet>() where TSet : class;

        Task<int> DeleteAsync<TEntity>(IQueryable<TEntity> set)
            where TEntity : Entity;
        Task CreateAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity;
        Task UpdateAsync<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : Entity;

        IEnumerable<IQueryable<T>> Sets<T>() where T : class;

        Task RunInTransactionAsync(Func<Task> action);

        Task<TResult> RunInTransactionAsync<TResult>(Func<Task<TResult>> action);
    }

    public static class RepositoryExtensions
    {
        public static Task DeleteAsync<TEntity>(this IRepository repository, IEnumerable<TEntity> entities)
            where TEntity : Entity => repository.DeleteAsync<TEntity>(entities.Select(e => e.Id));

        public static Task<int> DeleteAsync<TEntity>(this IRepository repo, IEnumerable<long> ids)
            where TEntity : Entity
            => repo.DeleteAsync(repo.Set<TEntity>().Where(e => ids.Contains(e.Id)));

        public static async Task<long> CreateAsync<TEntity>(this IRepository repo, TEntity newEntity)
            where TEntity : Entity
        {
            await repo.CreateAsync(newEntity.Yield());
            return newEntity.Id;
        }

        public static Task<TEntity> CreateAndGetAsync<TEntity>(this IRepository repo, TEntity newEntity)
            where TEntity : Entity
            => repo.CreateAsync(newEntity).Then(id => repo.Set<TEntity>().Where(e => e.Id == id).First());

        public static Task DeleteAsync<TEntity>(this IRepository repo, long id)
            where TEntity : Entity
            => repo.DeleteAsync<TEntity>(new long[1] { id });

        public static Task UpdateAsync<TEntity>(this IRepository repo, TEntity entity)
            where TEntity : Entity
            => repo.UpdateAsync(new TEntity[1] { entity });

    }
}
