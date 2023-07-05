using Business.ModelsComposition;
using Business.RegisterEntries;
using Business.Selectors;
using Data.Repository;
using Protocol.Exceptions;
using Protocol.Models.FinancialDocuments;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Common.Extensions.Number;
using Data.Entities.Documents.Finances;
using DestallMaterials.WheelProtection.Extensions.Objects;
using Protocol.Models;

namespace Business.StatusMovement;

public partial class DepositStatusMover
{
    public override Task<DepositModel> MoveDownToDraftAsync(IDepositReadOnlyModel item)
        => MoveDownAsync(item);

    public override async Task<DepositModel> MoveDownToReadyAsync(IDepositReadOnlyModel item)
    {
        using (var repo = await GetRepositoryAsync())
        {
            var notEnough = await NotEnoughCashAsync(item, repo,
                sum => new CashRemainders
                {
                    Present = sum
                });

            if (notEnough)
            {
                var message = $"Not enough reservable cash" +
                    $" to rollback {item.Reference} to the ready status.";
                throw new InsufficientResourceException(message);
            }
        }

        return await MoveDownAsync(item);
    }

    public override async Task<DepositModel> MoveDownToSubmittedAsync(IDepositReadOnlyModel item)
    {
        using (var repo = await GetRepositoryAsync())
        {
            var notEnough = await NotEnoughCashAsync(item, repo,
                sum => new CashRemainders
                {
                    Reservable = sum
                });

            if (notEnough)
            {
                var message = $"Not enough reservable cash" +
                    $" to rollback {item.Reference} to the submitted status.";
                throw new InsufficientResourceException(message);
            }
        }

        return await MoveDownAsync(item);
    }

    public override async Task<DepositModel> MoveUpToSubmittedAsync(IDepositReadOnlyModel item)
    {
        var saved = await _business.Actions.Deposit.Save.CallAsync(item.Mutate(i => i.Status = item.Status.AddOne()));
        return saved.ToMutable();
    }

    
    public override Task<DepositModel> MoveUpToReadyAsync(IDepositReadOnlyModel item)
        => MoveUpAsync(item, deposit => new CashEntry
        {
            ActorId = deposit.Reference.Id,
            CurrencyId = deposit.Currency.Id,
            Reserved = -deposit.Sum,
            Status = deposit.Status.AddOne()
        });

    public override Task<DepositModel> MoveUpToExecutedAsync(IDepositReadOnlyModel item)
        => MoveUpAsync(item, deposit => new CashEntry
        {
            ActorId = deposit.Reference.Id,
            CurrencyId = deposit.Currency.Id,
            Reserved = deposit.Sum,
            Added = deposit.Sum,
            Status = deposit.Status.AddOne()
        });

    static Task<DepositModel> GetDocumentModelAsync(IDataRepository repo, long id)
        => repo.Deposits.Where(io => io.Id == id).Select(DepositSelectors.ModelSelector(repo)).FirstOrDefaultAsync();

    static async Task<bool> NotEnoughCashAsync(
        IDepositReadOnlyModel item,
        IDataRepository repo,
        Func<double, CashRemainders> formRequirements)
    {
        var sum = item.Sum;
        var remainder = await repo
            .GetCashRemainders(item.Currency.Id.Yield())
            .FirstOrDefaultAsync() ?? new CashRemainders();

        var requirements = formRequirements(sum);

        var notEnough = (remainder - requirements).IsNegative;

        return notEnough;
    }
    async Task<DepositModel> MoveDownAsync(IDepositReadOnlyModel item)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;

        var currentStatus = item.Status;

        var entries = repo.CashEntries.Where(se => se.ActorId == actorId && se.Status == currentStatus);
        var entity = await repo
            .Deposits
            .FirstOrDefaultAsync(i => i.Id == actorId);

        var result = await repo.RunInTransactionAsync(async () => {
            await repo.DeleteAsync(entries);
            item.Fill(entity);
            entity.Status--;
            await repo.UpdateAsync(entity);
            return await GetDocumentModelAsync(repo, item.Reference.Id);
        });

        return result;
    }

    async Task<DepositModel> MoveUpAsync(IDepositReadOnlyModel item, Func<IDepositReadOnlyModel, CashEntry> composeEntries)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;
        var newStatus = item.Status.AddOne();
        var registrator = new DocumentMovesRegistrator(repo);

        var existingEntity = await repo
            .Deposits
            .FirstOrDefaultAsync(i => i.Id == actorId);

        var result = await repo.RunInTransactionAsync(async () => {
            item.Fill(existingEntity);
            var registryEntries = composeEntries(item);
            var entity = await registrator.RegisterStatusRaiseAsync(existingEntity, registryEntries.Yield(), newStatus);
            return await GetDocumentModelAsync(repo, entity.Id);
        });

        return result;
    }

}
