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
using Protocol.Extensions;
using Protocol.Models.FinancialDocuments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Business.StatusMovement;

public partial class StaffPaycheckStatusMover
{
    public override Task<StaffPaycheckModel> MoveDownToDraftAsync(IStaffPaycheckReadOnlyModel item)
        => MoveDownAsync(item);

    public override async Task<StaffPaycheckModel> MoveDownToReadyAsync(IStaffPaycheckReadOnlyModel item) 
        => await MoveDownAsync(item);

    public override async Task<StaffPaycheckModel> MoveDownToSubmittedAsync(IStaffPaycheckReadOnlyModel item) 
        => await MoveDownAsync(item);

    public override async Task<StaffPaycheckModel> MoveUpToSubmittedAsync(IStaffPaycheckReadOnlyModel item)
    {
        if (!item.Exists())
        { 
            item = await _business.Actions.StaffPaycheck.Save.CallAsync(item);
        }

        var result = await MoveUpAsync(item, paycheck => new SalarySettlementEntry
        {
            ActorId = paycheck.Reference.Id,
            Accrued = paycheck.Sum,
            PaidToId = paycheck.PaidTo.Id,
            Status = paycheck.Status
        }.Yield());
 
        return result;
    }


    public override async Task<StaffPaycheckModel> MoveUpToReadyAsync(IStaffPaycheckReadOnlyModel item)
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
                    $" to move {item.Reference?.Representation} to the ready status.";
                throw new InsufficientResourceException(message);
            }
        }

        return await MoveUpAsync(item, paycheck => new CashEntry
        {
            ActorId = paycheck.Reference.Id,
            CurrencyId = paycheck.Currency.Id,
            Reserved = paycheck.Sum,
            Status = paycheck.Status
        }.Yield());
    }

    public override async Task<StaffPaycheckModel> MoveUpToExecutedAsync(IStaffPaycheckReadOnlyModel item)
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

        return await MoveUpAsync(item, paycheck => new CashEntry
        {
            ActorId = paycheck.Reference.Id,
            CurrencyId = paycheck.Currency.Id,
            Reserved = -paycheck.Sum,
            Added = -paycheck.Sum,
            Status = paycheck.Status
        }.And<StatusMovementRegistryEntry>(new SalarySettlementEntry
        {
            Accrued = -paycheck.Sum,
            ActorId = paycheck.Reference.Id,
            PaidToId = paycheck.PaidTo.Id,
            Status = paycheck.Status,
            Paid = paycheck.Sum - paycheck.Withheld
        }));
    }

    static Task<StaffPaycheckModel> GetDocumentModelAsync(IDataRepository repo, long id)
        => repo.StaffPaychecks.Where(io => io.Id == id).Select(StaffPaycheckSelectors.ModelSelector(repo)).FirstOrDefaultAsync();


    static async Task<bool> NotEnoughCashAsync(
        IStaffPaycheckReadOnlyModel item,
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
    async Task<StaffPaycheckModel> MoveDownAsync(IStaffPaycheckReadOnlyModel item)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;

        var currentStatus = item.Status;

        var cashEntries = repo.CashEntries.Where(se => se.ActorId == actorId && se.Status == currentStatus);
        var salarySettlementEntries = repo.SalarySettlementEntries.Where(se => se.ActorId == actorId && se.Status == currentStatus);

        var entity = await repo
            .StaffPaychecks
            .FirstOrDefaultAsync(i => i.Id == actorId);

        var result = await repo.RunInTransactionAsync(async () => {
            await repo.DeleteAsync(cashEntries);
            await repo.DeleteAsync(salarySettlementEntries);
            item.Fill(entity);
            entity.Status--;
            await repo.UpdateAsync(entity);
            return await GetDocumentModelAsync(repo, item.Reference.Id);
        });

        return result;
    }

    async Task<StaffPaycheckModel> MoveUpAsync(IStaffPaycheckReadOnlyModel item, Func<IStaffPaycheckReadOnlyModel, IEnumerable<StatusMovementRegistryEntry>> composeEntries)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;
        var newStatus = item.Status.AddOne();
        var registrator = new DocumentMovesRegistrator(repo);

        var existingEntity = await repo
            .StaffPaychecks
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
