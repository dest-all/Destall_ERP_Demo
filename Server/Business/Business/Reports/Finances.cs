using Business.Actions;
using Business.Selectors;
using Data.Entities.Documents.Trade;
using Data.Samples;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using Microsoft.EntityFrameworkCore;
using Protocol.Models.FinancialDocuments;
using Protocol.Models.StatusMovementRegistryEntries;
using Protocol.Models.Transportables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Reports;

[ProcessServerRequest]
public partial class Finances : ActionContainer
{
    [ClientCaching(10, 1)]
    [ServerCaching(30, 1)]
    [Permissions("Reports_Financial")]
    public async Task<IReadOnlyList<ICashEntryReadOnlyModel>> GetCashBalance()
    {
        using var repo = await GetRepositoryAsync();

        var cashEntries = repo.CashEntries;

        var byCurrencyBalances = await cashEntries
            .GroupBy(ce => ce.CurrencyId)
            .Select(gr => new CashEntryModel
            {
                Added = gr.Sum(g => g.Added),
                Reserved = gr.Sum(g => g.Reserved),
                Currency = new Protocol.Models.ReferrableEntities.CurrencyReference
                {
                    Id = gr.First().CurrencyId ?? 0,
                    Representation = gr.First().Currency.Representation
                }
            })
            .ToArrayAsync();

        return byCurrencyBalances;
    }

    [ClientCaching(10, 1)]
    [ServerCaching(30, 1)]
    [Permissions("Reports_Financial")]
    public async Task<IReadOnlyList<IUnsettledOrderReadOnlyModel>> GetUnsettledIncomingOrders()
    {
        using var repo = await GetRepositoryAsync();

        var closedStatus = IncomingOrder.Statuses.Closed.Index;
        var sumbittedStatus = IncomingOrder.Statuses.Submitted.Index;

        var query = repo.AccountEntries
            .Join(
                repo.IncomingOrders,
                ae => ae.BaseDocumentId,
                io => io.Id,
                (ae, io) => new
                {
                    AccountEntry = ae,
                    IncomingOrder = io
                }
            )
            .Where(i => i.IncomingOrder.Status < closedStatus && i.IncomingOrder.Status > sumbittedStatus)
            .GroupBy(i => new
            {
                i.IncomingOrder.Id,
                i.IncomingOrder.Representation
            })
            .Select(gr => new
            {
                IncomingOrder = gr.Key,
                TotalValue = gr.Where(g => g.AccountEntry.ActorId == gr.Key.Id).Sum(i => i.AccountEntry.SumReceivable),
                ReceivedPayment = gr.Where(g => g.AccountEntry.ActorId != gr.Key.Id).Sum(i => i.AccountEntry.SumReceivable)
            })
            .Where(gr => gr.TotalValue > gr.ReceivedPayment);

        var orders = await query.ToArrayAsync();

        var orderIds = orders.Select(o => o.IncomingOrder.Id).ToArray();

        var payments = await repo.IncomingPayments
            .Where(ip => orderIds.Contains(ip.OrderId ?? 0))
            .GroupBy(ip => ip.OrderId, ip => new IncomingPaymentReference
            {
                Representation = ip.Representation,
                Id = ip.Id
            }).ToDictionaryAsync(gr => gr.Key, gr => gr.Select(g => g));

        var result = orders.Select(o => new UnsettledOrderModel
        {
            IncomingOrder = new Protocol.Models.Documents.IncomingOrderReference
            {
                Id = o.IncomingOrder.Id,
                Representation = o.IncomingOrder.Representation
            },
            IncomingPayments = payments.GetValueOrDefault(o.IncomingOrder.Id)?.ToList() ?? new(),
            TotalValue = o.TotalValue,
            ReceivedPayment = o.ReceivedPayment,
            LeftToReceive = o.TotalValue - o.ReceivedPayment
        }).ToArray();

        return result;
    }

    [ClientCaching(10, 1)]
    [ServerCaching(30, 1)]
    [Permissions("Reports_Financial")]
    public async Task<IReadOnlyList<IUnsettledOrderReadOnlyModel>> GetUnsettledOutgoingOrders()
    {
        using var repo = await GetRepositoryAsync();

        var closedStatus = OutgoingOrder.Statuses.Closed.Index;
        var sumbittedStatus = OutgoingOrder.Statuses.Submitted.Index;

        var query = repo.AccountEntries
            .Join(
                repo.OutgoingOrders,
                ae => ae.BaseDocumentId,
                io => io.Id,
                (ae, io) => new {
                    AccountEntry = ae,
                    OutgoingOrder = io
                }
            )
            .Where(i => i.OutgoingOrder.Status < closedStatus && i.OutgoingOrder.Status > sumbittedStatus)
            .GroupBy(i => new {
                i.OutgoingOrder.Id,
                i.OutgoingOrder.Representation
            })
            .Select(gr => new {
                OutgoingOrder = gr.Key,
                TotalValue = gr.Where(g => g.AccountEntry.ActorId == gr.Key.Id).Sum(i => i.AccountEntry.SumPayable),
                PaidSum = gr.Where(g => g.AccountEntry.ActorId != gr.Key.Id).Sum(i => i.AccountEntry.SumPayable)
            })
            .Where(gr => gr.TotalValue > gr.PaidSum);

        var orders = await query.ToArrayAsync();

        var orderIds = orders.Select(o => o.OutgoingOrder.Id).ToArray();

        var payments = await repo.OutgoingPayments
            .Where(ip => orderIds.Contains(ip.OrderId ?? 0))
            .GroupBy(ip => ip.OrderId, ip => new OutgoingPaymentReference
            {
                Representation = ip.Representation,
                Id = ip.Id
            }).ToDictionaryAsync(gr => gr.Key, gr => gr.Select(g => g));

        var result = orders.Select(o => new UnsettledOrderModel
        {
            OutgoingOrder = new Protocol.Models.Documents.OutgoingOrderReference
            {
                Id = o.OutgoingOrder.Id,
                Representation = o.OutgoingOrder.Representation
            },
            OutgoingPayments = payments.GetValueOrDefault(o.OutgoingOrder.Id)?.ToList() ?? new(),
            TotalValue = o.TotalValue,
            ReceivedPayment = o.PaidSum,
            LeftToReceive = o.TotalValue - o.PaidSum
        }).ToArray();

        return result;
    }

}
