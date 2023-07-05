using Data.Entities.Registers;
using System;
using System.Collections.Generic;
using Data.Repository;
using Data.Entities.Documents;
using System.Threading.Tasks;

namespace Business.StatusMovement;

class DocumentMovesRegistrator
{
    public IRepository Repo { private get; init; }

    public DocumentMovesRegistrator(IRepository repo)
    {
        Repo = repo;
    }

    public async Task<TEntity> RegisterStatusRaiseAsync<TEntity, TRegistryEntry>(TEntity entity, IEnumerable<TRegistryEntry> registryEntries, ushort newStatus)
        where TEntity : Document
        where TRegistryEntry : StatusMovementRegistryEntry
    {
        var repo = Repo;

        if (entity.Status == newStatus)
        {
            throw new InvalidOperationException("Can't raise status to the same level.");
        }

        if (entity.Id <= 0)
        {
            entity = await repo.CreateAndGetAsync(entity);
        }
        else
        {
            await repo.UpdateAsync(entity);
        }

        await repo.RunInTransactionAsync(async () =>
        {
            var entriesToCreate = registryEntries;

            await repo.CreateAsync(entriesToCreate);

            entity.Status = newStatus;

            await repo.UpdateAsync(entity);
        });

        return entity;
    }
}
