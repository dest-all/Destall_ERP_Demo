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
using Data.Entities.Registers;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Business.StatusMovement;

public partial class IncomingPaymentStatusMover
{
    public override Task<IncomingPaymentModel> MoveDownToDraftAsync(IIncomingPaymentReadOnlyModel item)
        => MoveDownAsync(item);

    public override async Task<IncomingPaymentModel> MoveDownToReadyAsync(IIncomingPaymentReadOnlyModel item)
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

    public override async Task<IncomingPaymentModel> MoveDownToSubmittedAsync(IIncomingPaymentReadOnlyModel item)
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

    public override async Task<IncomingPaymentModel> MoveUpToSubmittedAsync(IIncomingPaymentReadOnlyModel item)
    {
        var saved = await _business.Actions.IncomingPayment.Save.CallAsync(item.Mutate(i => i.Status = item.Status.AddOne()));
        return saved.ToMutable();
    }


    public override Task<IncomingPaymentModel> MoveUpToReadyAsync(IIncomingPaymentReadOnlyModel item)
        => MoveUpAsync(item, IncomingPayment => new CashEntry
        {
            ActorId = IncomingPayment.Reference.Id,
            CurrencyId = IncomingPayment.Currency.Id,
            Reserved = -IncomingPayment.Sum,
            Status = IncomingPayment.Status.AddOne()
        }.Yield());

    public override Task<IncomingPaymentModel> MoveUpToExecutedAsync(IIncomingPaymentReadOnlyModel item)
        => MoveUpAsync(item, incomingPayment => {
            var cashEntry = new CashEntry
            {
                ActorId = incomingPayment.Reference.Id,
                CurrencyId = incomingPayment.Currency.Id,
                Reserved = incomingPayment.Sum,
                Added = incomingPayment.Sum,
                Status = incomingPayment.Status.AddOne()
            } as StatusMovementRegistryEntry;

            var accountEntry = new AccountEntry
            {
                ActorId = item.Reference.Id,
                CurrencyId = item.Currency.Id,
                DebtorId = item.Payer.Id,
                SumReceivable = incomingPayment.Sum,
                Status = incomingPayment.Status.AddOne(),
                BaseDocumentId = incomingPayment.Order.Id
            };

            return cashEntry.And(accountEntry);
        });

    static Task<IncomingPaymentModel> GetDocumentModelAsync(IDataRepository repo, long id)
        => repo.IncomingPayments.Where(io => io.Id == id).Select(IncomingPaymentSelectors.ModelSelector(repo)).FirstOrDefaultAsync();

    static async Task<bool> NotEnoughCashAsync(
        IIncomingPaymentReadOnlyModel item,
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
    async Task<IncomingPaymentModel> MoveDownAsync(IIncomingPaymentReadOnlyModel item)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;

        var currentStatus = item.Status;

        var cashEntries = repo.CashEntries.Where(se => se.ActorId == actorId && se.Status == currentStatus);
        var accountEntries = repo.AccountEntries.Where(se => se.ActorId == actorId && se.Status == currentStatus);
        var entity = await repo
            .IncomingPayments
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

    async Task<IncomingPaymentModel> MoveUpAsync(IIncomingPaymentReadOnlyModel item, Func<IIncomingPaymentReadOnlyModel, IEnumerable<StatusMovementRegistryEntry>> composeEntries)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;
        var newStatus = item.Status.AddOne();
        var registrator = new DocumentMovesRegistrator(repo);

        var existingEntity = await repo
            .IncomingPayments
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
