// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Linq;
using System;
using System.Collections.Generic;
using Data.EntityFramework;
using Data.Entities.Documents.Trade;
using Protocol.Models.GoodTransactionLines;
using System.Linq.Expressions;

namespace Business.Selectors;
public static partial class IncomingOrderLineSelectors
{
    public static Expression<Func<IncomingOrderLine, IncomingOrderLineModel>> ModelSelector(Data.Repository.IDataRepository repo) => e1 => new IncomingOrderLineModel{Quantity = e1.Quantity, //
 Price = e1.Price, //
 // Referring properties.
    IncomingOrder = repo.IncomingOrders.Where(e2 => e2 == e1.IncomingOrder).Select(IncomingOrderSelectors.ReferenceSelector).FirstOrDefault() ?? new Protocol.Models.Documents.IncomingOrderReference(), //
    Good = repo.Goods.Where(e2 => e2 == e1.Good).Select(GoodSelectors.ReferenceSelector).FirstOrDefault() ?? new Protocol.Models.DataHolders.GoodReference(), //
    };
}