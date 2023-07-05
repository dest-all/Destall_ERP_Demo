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
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.StatusMovement;

public partial class OutgoingPaymentStatusMover
{
    public override Task<OutgoingPaymentModel> MoveDownToDraftAsync(IOutgoingPaymentReadOnlyModel item)
        => MoveDownAsync(item);

    public override Task<OutgoingPaymentModel> MoveDownToReadyAsync(IOutgoingPaymentReadOnlyModel item) 
        => MoveDownAsync(item);

    public override Task<OutgoingPaymentModel> MoveDownToSubmittedAsync(IOutgoingPaymentReadOnlyModel item) 
        => MoveDownAsync(item);

    public override async Task<OutgoingPaymentModel> MoveUpToSubmittedAsync(IOutgoingPaymentReadOnlyModel item)
    {
        var saved = await _business.Actions.OutgoingPayment.Save.CallAsync(item.Mutate(i => i.Status = item.Status.AddOne()));
        return saved.ToMutable();
    }


    public override async Task<OutgoingPaymentModel> MoveUpToReadyAsync(IOutgoingPaymentReadOnlyModel item)
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

        return await MoveUpAsync(item, deposit => new CashEntry
        {
            ActorId = deposit.Reference.Id,
            CurrencyId = deposit.Currency.Id,
            Reserved = deposit.Sum,
            Status = deposit.Status.AddOne()
        }.Yield());
    }

    public override async Task<OutgoingPaymentModel> MoveUpToExecutedAsync(IOutgoingPaymentReadOnlyModel item)
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

        return await MoveUpAsync(item, outgoingPayment => {
            var cashEntry = new CashEntry
            {
                ActorId = outgoingPayment.Reference.Id,
                CurrencyId = outgoingPayment.Currency.Id,
                Reserved = -outgoingPayment.Sum,
                Added = -outgoingPayment.Sum,
                Status = outgoingPayment.Status.AddOne()
            } as StatusMovementRegistryEntry;

            var accountEntry = new AccountEntry
            {
                ActorId = outgoingPayment.Reference.Id,
                SumPayable = outgoingPayment.Sum,
                CreditorId = outgoingPayment.Receiver.Id,
                Status = outgoingPayment.Status.AddOne(),
                CurrencyId = outgoingPayment.Currency.Id,
                BaseDocumentId = outgoingPayment.Order.Id
            };

            return cashEntry.And(accountEntry);
        });
    }

    static Task<OutgoingPaymentModel> GetDocumentModelAsync(IDataRepository repo, long id)
        => repo.OutgoingPayments.Where(io => io.Id == id).Select(OutgoingPaymentSelectors.ModelSelector(repo)).FirstOrDefaultAsync();


    static async Task<bool> NotEnoughCashAsync(
        IOutgoingPaymentReadOnlyModel item,
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
    async Task<OutgoingPaymentModel> MoveDownAsync(IOutgoingPaymentReadOnlyModel item)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;

        var currentStatus = item.Status;

        var cashEntries = repo.CashEntries.Where(se => se.ActorId == actorId && se.Status == currentStatus);
        var accountEntries = repo.AccountEntries.Where(se => se.ActorId == actorId && se.Status == currentStatus);
        var entity = await repo
            .OutgoingPayments
            .FirstOrDefaultAsync(i => i.Id == actorId);

        var result = await repo.RunInTransactionAsync(async () => {
            await repo.DeleteAsync(cashEntries);
            await repo.DeleteAsync(accountEntries);
            item.Fill(entity);
            entity.Status--;
            await repo.UpdateAsync(entity);
            return await GetDocumentModelAsync(repo, item.Reference.Id);
        });

        return result;
    }

    async Task<OutgoingPaymentModel> MoveUpAsync(IOutgoingPaymentReadOnlyModel item, Func<IOutgoingPaymentReadOnlyModel, IEnumerable<StatusMovementRegistryEntry>> composeEntries)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;
        var newStatus = item.Status.AddOne();
        var registrator = new DocumentMovesRegistrator(repo);

        var existingEntity = await repo
            .OutgoingPayments
            .FirstOrDefaultAsync(i => i.Id == actorId);

        var result = await repo.RunInTransactionAsync(async () => {
            item.Fill(existingEntity);
            var registryEntries = composeEntries(item);
            var entity = await registrator.RegisterStatusRaiseAsync(existingEntity, registryEntries, newStatus);
            return await GetDocumentModelAsync(repo, entity.Id);
        });

        return result;
    }
}
