using Business.ModelsComposition;
using Data.Entities.Documents.Trade;
using Data.Entities.Registers;
using Protocol.Models.Documents;
using System.Linq;
using System.Threading.Tasks;
using Data.Repository;
using Microsoft.EntityFrameworkCore;
using Business.Selectors;
using Common.Extensions.Number;
using Protocol.Models;
using System.Collections.Generic;
using System;
using Business.RegisterEntries;
using Protocol.Models.DataHolders;
using DestallMaterials.WheelProtection.Extensions.Dictionaries;
using DestallMaterials.WheelProtection.Extensions.Enumerables;
using DestallMaterials.WheelProtection.Extensions.Strings;
using System.Threading;
using Protocol.Exceptions;
using Protocol.Models.GoodTransactionLines;
using DestallMaterials.WheelProtection.Extensions.Objects;

namespace Business.StatusMovement;

public partial class OutgoingOrderStatusMover
{
    public override Task<OutgoingOrderModel> MoveDownToDraftAsync(IOutgoingOrderReadOnlyModel item)
        => MoveDownAsync(item);

    public override async Task<OutgoingOrderModel> MoveDownToReadyAsync(IOutgoingOrderReadOnlyModel item)
    {
        using (var repo = await GetRepositoryAsync())
        {
            var insufficientStocks = await GetInsufficientStocksAsync(item, repo, gs => new StockEntryExtensions.GoodRemainders
            {
                Present = gs.Quantity
            });

            if (insufficientStocks.HasContent())
            {
                var message = $"Not enough stock remainders for {insufficientStocks.Select(iS => iS.Key.Representation).Join(", ")}" +
                    $" to rollback {item.Reference} to the ready status.";
                throw new InsufficientResourceException(message);
            }
        }

        return await MoveDownAsync(item);
    }

    public override async Task<OutgoingOrderModel> MoveDownToSubmittedAsync(IOutgoingOrderReadOnlyModel item)
    {
        using (var repo = await GetRepositoryAsync())
        {
            var insufficientStocks = await GetInsufficientStocksAsync(item, repo, 
                gs => new StockEntryExtensions.GoodRemainders
                {
                    Reservable = gs.Quantity
                });

            if (insufficientStocks.HasContent())
            {
                var message = $"Not enough reservable stocks for {insufficientStocks.Select(iS => iS.Key.Representation).Join(", ")}" +
                    $" to rollback {item.Reference} to the submitted status.";
                throw new InsufficientResourceException(message);
            }
        }

        return await MoveDownAsync(item);
    }

    public override async Task<OutgoingOrderModel> MoveUpToSubmittedAsync(IOutgoingOrderReadOnlyModel item)
    {
        var saved = await _business.Actions.OutgoingOrder.Save.CallAsync(item.Mutate(i => i.Status = item.Status.AddOne()));
        return saved.ToMutable();
    }

    public override Task<OutgoingOrderModel> MoveUpToReadyAsync(IOutgoingOrderReadOnlyModel item) 
        => MoveUpAsync(item, e => e.GoodsBought
                            .GroupBy(g => g.Good.Id)
                            .Select(group => new StockEntry
                            {
                                ActorId = e.Reference.Id,
                                GoodId = group.Key,
                                Reserved = -group.Sum(i => i.Quantity),
                                Status = e.Status.AddOne()
                            }));

    public override Task<OutgoingOrderModel> MoveUpToExecutedAsync(IOutgoingOrderReadOnlyModel item)
        => MoveUpAsync(item, e => {

            var stockEntries = e.GoodsBought.GroupBy(g => g.Good.Id).Select(group => new StockEntry
            {
                ActorId = e.Reference.Id,
                GoodId = group.Key,
                Reserved = group.Sum(i => i.Quantity),
                Added = group.Sum(i => i.Quantity),
                Status = e.Status.AddOne()
            });

            var accountEntries = new AccountEntry
            {
                ActorId = e.Reference.Id,
                CurrencyId = e.Currency.Id,
                CreditorId = e.Supplier.Id,
                SumPayable = e.GoodsBought.Sum(gs => gs.Quantity * gs.Price),
                Status = e.Status.AddOne(),
                BaseDocumentId = e.Reference.Id
            };

            var entries = stockEntries.Cast<StatusMovementRegistryEntry>().Concat(accountEntries.Yield());

            return entries;
        });



    static async Task<KeyValuePair<IGoodReadOnlyReference, StockEntryExtensions.GoodRemainders>[]> GetInsufficientStocksAsync(
        IOutgoingOrderReadOnlyModel item,
        IDataRepository repo,
        Func<IOutgoingOrderLineReadOnlyModel, StockEntryExtensions.GoodRemainders> formRequirements)
    {
        var goodsBought = item.GoodsBought;
        var remainders = await repo
            .GetGoodRemainders(goodsBought.Select(gs => gs.Good.Id))
            .ToDictionaryAsync(gs => gs.Good.Id, gs => gs as StockEntryExtensions.GoodRemainders);

        var requirements = item.GoodsBought.ToDictionary(gs => gs.Good,
            formRequirements);

        remainders.EnsureKeysArePresent(
            goodsBought.Select(gb => gb.Good.Id),
            gr => new StockEntryExtensions.GoodRemainders()
        );

        var insufficientStocks = goodsBought.ToDictionary(gb => gb.Good,
            gb => remainders[gb.Good.Id] - requirements[gb.Good]
        ).Where(gb => gb.Value.IsNegative).ToArray();

        return insufficientStocks;
    }


    static Task<OutgoingOrderModel> GetOrderModelAsync(IDataRepository repo, long id)
        => repo.OutgoingOrders.Where(io => io.Id == id).Select(OutgoingOrderSelectors.ModelSelector(repo)).FirstOrDefaultAsync();


    async Task<OutgoingOrderModel> MoveDownAsync(IOutgoingOrderReadOnlyModel item)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;

        var currentStatus = item.Status;

        var stockEntries = repo.StockEntries.Where(se => se.ActorId == actorId && se.Status == currentStatus);
        var accountEntries = repo.AccountEntries.Where(se => se.ActorId == actorId && se.Status == currentStatus);

        var entity = await repo
            .OutgoingOrders
            .IncludeInnerTables()
            .FirstOrDefaultAsync(i => i.Id == actorId);

        var result = await repo.RunInTransactionAsync(async () => {
            await repo.DeleteAsync(stockEntries);
            await repo.DeleteAsync(accountEntries);
            var lines = entity.GoodsBought;
            item.Fill(entity, repo);
            await repo.DeleteAsync(lines.Skip(entity.GoodsBought.Count));
            entity.Status--;
            await repo.UpdateAsync(entity);
            return await GetOrderModelAsync(repo, item.Reference.Id);
        });

        return result;
    }

    async Task<OutgoingOrderModel> MoveUpAsync(IOutgoingOrderReadOnlyModel item, Func<IOutgoingOrderReadOnlyModel, IEnumerable<StatusMovementRegistryEntry>> composeEntries)
    {
        using var repo = await GetRepositoryAsync();
        var actorId = item.Reference.Id;
        var newStatus = item.Status.AddOne();
        var registrator = new DocumentMovesRegistrator(repo);

        var existingEntity = await repo
            .OutgoingOrders
            .IncludeInnerTables()
            .FirstOrDefaultAsync(i => i.Id == actorId);

        var result = await repo.RunInTransactionAsync(async () => {
            var lines = existingEntity.GoodsBought;
            item.Fill(existingEntity, repo);
            await repo.DeleteAsync(lines.Skip(existingEntity.GoodsBought.Count));
            var registryEntries = composeEntries(item);
            var entity = await registrator.RegisterStatusRaiseAsync(existingEntity, registryEntries, newStatus);
            return await GetOrderModelAsync(repo, entity.Id);
        });

        return result;
    }
}
