using Business.ModelsComposition;
using Data.Entities.Documents.Trade;
using Data.Entities.Registers;
using Protocol.Models.Documents;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Business.Selectors;
using System.Collections.Generic;
using System;
using Protocol.Models;
using Common.Extensions.Number;
using Business.RegisterEntries;
using Protocol.Models.DataHolders;
using DestallMaterials.WheelProtection.Extensions.Dictionaries;
using Protocol.Models.GoodTransactionLines;
using DestallMaterials.WheelProtection.Extensions.Enumerables;
using DestallMaterials.WheelProtection.Extensions.Strings;
using Protocol.Exceptions;
using DestallMaterials.WheelProtection.Extensions.Objects;

namespace Business.StatusMovement;

public partial class IncomingOrderStatusMover
{
    async Task<IncomingOrderModel> MoveDownAsync(IIncomingOrderReadOnlyModel item)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;

        var currentStatus = item.Status;

        var stockEntries = repo.StockEntries.Where(se => se.ActorId == actorId && se.Status == currentStatus);
        var accountEntries = repo.AccountEntries.Where(se => se.ActorId == actorId && se.Status == currentStatus);

        var entity = await repo
            .IncomingOrders
            .IncludeInnerTables()
            .FirstOrDefaultAsync(i => i.Id == actorId);

        var result = await repo.RunInTransactionAsync(async () => {
            await repo.DeleteAsync(stockEntries);
            await repo.DeleteAsync(accountEntries);
            var lines = entity.GoodsSold;
            item.Fill(entity, repo);
            await repo.DeleteAsync(lines.Skip(entity.GoodsSold.Count));
            entity.Status--;
            await repo.UpdateAsync(entity);
            return await GetOrderModelAsync(repo, item.Reference.Id);
        });

        return result;
    }

    async Task<IncomingOrderModel> MoveUpAsync(IIncomingOrderReadOnlyModel item, Func<IIncomingOrderReadOnlyModel, IEnumerable<StatusMovementRegistryEntry>> composeEntries)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;
        var newStatus = item.Status.AddOne();
        var registrator = new DocumentMovesRegistrator(repo);

        var existingEntity = await repo
            .IncomingOrders
            .IncludeInnerTables()
            .FirstOrDefaultAsync(i => i.Id == actorId);

        var result = await repo.RunInTransactionAsync(async () => {
            var lines = existingEntity.GoodsSold;
            item.Fill(existingEntity, repo);
            await repo.DeleteAsync(lines.Skip(existingEntity.GoodsSold.Count));
            var registryEntries = composeEntries(item);
            var entity = await registrator.RegisterStatusRaiseAsync(existingEntity, registryEntries, newStatus);
            return await GetOrderModelAsync(repo, entity.Id);
        });

        return result;
    }

    static async Task<KeyValuePair<IGoodReadOnlyReference, StockEntryExtensions.GoodRemainders>[]> GetInsufficientStocksAsync(
        IIncomingOrderReadOnlyModel item,
        IDataRepository repo,
        Func<IIncomingOrderLineReadOnlyModel, StockEntryExtensions.GoodRemainders> formRequirements)
    {
        var goodsSold = item.GoodsSold;
        var remainders = await repo
            .GetGoodRemainders(goodsSold.Select(gs => gs.Good.Id))
            .ToDictionaryAsync(gs => gs.Good.Id, gs => gs as StockEntryExtensions.GoodRemainders);

        var requirements = item.GoodsSold.ToDictionary(gs => gs.Good,
            formRequirements);

        remainders.EnsureKeysArePresent(
            goodsSold.Select(gb => gb.Good.Id),
            gr => new StockEntryExtensions.GoodRemainders()
        );

        var insufficientStocks = goodsSold.ToDictionary(gb => gb.Good,
            gb => remainders[gb.Good.Id] - requirements[gb.Good]
        ).Where(gb => gb.Value.IsNegative).ToArray();

        return insufficientStocks;
    }

    public override Task<IncomingOrderModel> MoveDownToDraftAsync(IIncomingOrderReadOnlyModel item)
        => MoveDownAsync(item);

    public override Task<IncomingOrderModel> MoveDownToReadyAsync(IIncomingOrderReadOnlyModel item)
        => MoveDownAsync(item);


    public override Task<IncomingOrderModel> MoveDownToSubmittedAsync(IIncomingOrderReadOnlyModel item)
        => MoveDownAsync(item);

    public override async Task<IncomingOrderModel> MoveUpToSubmittedAsync(IIncomingOrderReadOnlyModel item)
    {
        var saved = await _business.Actions.IncomingOrder.Save.CallAsync(item.Mutate(i => i.Status = item.Status.AddOne()));
        return saved.ToMutable();
    }

    private static Task<IncomingOrderModel> GetOrderModelAsync(IDataRepository repo, long id)
        => repo.IncomingOrders.Where(io => io.Id == id).Select(IncomingOrderSelectors.ModelSelector(repo)).FirstOrDefaultAsync();

    public override async Task<IncomingOrderModel> MoveUpToReadyAsync(IIncomingOrderReadOnlyModel item)
    {
        using (var repo = await GetRepositoryAsync())
        {
            var insufficientStocks = await GetInsufficientStocksAsync(item, repo, gs => new StockEntryExtensions.GoodRemainders
            {
                Reservable = gs.Quantity
            });

            if (insufficientStocks.HasContent())
            {
                var message = $"Not enough reservable stock for {insufficientStocks.Select(iS => iS.Key.Representation).Join(", ")}" +
                    $" to move {item.Reference} to the ready status.";
                throw new InsufficientResourceException(message);
            }
        }

        return await MoveUpAsync(item, e => e.GoodsSold
               .GroupBy(g => g.Good.Id)
               .Select(group => new StockEntry
               {
                   ActorId = e.Reference.Id,
                   GoodId = group.Key,
                   Reserved = group.Sum(i => i.Quantity),
                   Status = e.Status.AddOne()
               })
        );

    }
    public override async Task<IncomingOrderModel> MoveUpToExecutedAsync(IIncomingOrderReadOnlyModel item)
    {
        using (var repo = await GetRepositoryAsync())
        {
            var insufficientStocks = await GetInsufficientStocksAsync(item, repo, gs => new StockEntryExtensions.GoodRemainders
            {
                Present = gs.Quantity
            });

            if (insufficientStocks.HasContent())
            {
                var message = $"Not enough present stock for {insufficientStocks.Select(iS => iS.Key.Representation).Join(", ")}" +
                    $" to move {item.Reference} to the executed status.";
                throw new InsufficientResourceException(message);
            }
        }

        return await MoveUpAsync(item,
            e => {
                var stockEntries = e.GoodsSold.GroupBy(g => g.Good.Id).Select(group => new StockEntry
                {
                    ActorId = e.Reference.Id,
                    GoodId = group.Key,
                    Reserved = -group.Sum(i => i.Quantity),
                    Added = -group.Sum(i => i.Quantity),
                    Status = e.Status.AddOne()
                });

                var accountEntries = new AccountEntry
                {
                    ActorId = e.Reference.Id,
                    CurrencyId = e.Currency.Id,
                    DebtorId = e.Customer.Id,
                    SumReceivable = e.GoodsSold.Sum(gs => gs.Quantity * gs.Price),
                    Status = e.Status.AddOne(),
                    BaseDocumentId = e.Reference.Id
                };

                var entries = stockEntries.Cast<StatusMovementRegistryEntry>().Concat(accountEntries.Yield());

                return entries;
            }
       );
    }
}
