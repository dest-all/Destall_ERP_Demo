using Business.ModelsComposition;
using Business.RegisterEntries;
using Business.Selectors;
using Common.Extensions.Number;
using Data.Entities.Documents.Finances;
using Data.Entities.Registers;
using Data.Repository;
using DestallMaterials.WheelProtection.Extensions.Objects;
using Microsoft.EntityFrameworkCore;
using Protocol.Exceptions;
using Protocol.Models;
using Protocol.Models.FinancialDocuments;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Business.StatusMovement;

public partial class WithdrawalStatusMover
{
    public override Task<WithdrawalModel> MoveDownToDraftAsync(IWithdrawalReadOnlyModel item)
        => MoveDownAsync(item);

    public override async Task<WithdrawalModel> MoveDownToReadyAsync(IWithdrawalReadOnlyModel item)
    {
        return await MoveDownAsync(item);
    }

    public override async Task<WithdrawalModel> MoveDownToSubmittedAsync(IWithdrawalReadOnlyModel item)
    {
        return await MoveDownAsync(item);
    }

    public override async Task<WithdrawalModel> MoveUpToSubmittedAsync(IWithdrawalReadOnlyModel item)
    {
        var saved = await _business.Actions.Withdrawal.Save.CallAsync(item.Mutate(i => i.Status = item.Status.AddOne()));
        return saved.ToMutable();
    }

    
    public override async Task<WithdrawalModel> MoveUpToReadyAsync(IWithdrawalReadOnlyModel item)
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
                    $" to move {item.Reference} to the ready status.";
                throw new InsufficientResourceException(message);
            }
        }

        return await MoveUpAsync(item, withdrawal => new CashEntry
        {
            ActorId = withdrawal.Reference.Id,
            CurrencyId = withdrawal.Currency.Id,
            Reserved = withdrawal.Sum,
            Status = withdrawal.Status.AddOne()
        });
    }

    public override async Task<WithdrawalModel> MoveUpToExecutedAsync(IWithdrawalReadOnlyModel item)
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
                var message = $"Not enough present cash" +
                    $" to move {item.Reference} to the executed status.";
                throw new InsufficientResourceException(message);
            }
        }

        return await MoveUpAsync(item, withdrawal => new CashEntry
        {
            ActorId = withdrawal.Reference.Id,
            CurrencyId = withdrawal.Currency.Id,
            Reserved = -withdrawal.Sum,
            Added = -withdrawal.Sum,
            Status = withdrawal.Status.AddOne()
        });
    }

    static Task<WithdrawalModel> GetDocumentModelAsync(IDataRepository repo, long id)
        => repo.Withdrawals.Where(io => io.Id == id).Select(WithdrawalSelectors.ModelSelector(repo)).FirstOrDefaultAsync();


    static async Task<bool> NotEnoughCashAsync(
        IWithdrawalReadOnlyModel item,
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
    async Task<WithdrawalModel> MoveDownAsync(IWithdrawalReadOnlyModel item)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;

        var currentStatus = item.Status;

        var entries = repo.CashEntries.Where(se => se.ActorId == actorId && se.Status == currentStatus);
        var entity = await repo
            .Withdrawals
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

    async Task<WithdrawalModel> MoveUpAsync(IWithdrawalReadOnlyModel item, Func<IWithdrawalReadOnlyModel, StatusMovementRegistryEntry> composeEntries)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;
        var newStatus = item.Status.AddOne();
        var registrator = new DocumentMovesRegistrator(repo);

        var existingEntity = await repo
            .Withdrawals
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
