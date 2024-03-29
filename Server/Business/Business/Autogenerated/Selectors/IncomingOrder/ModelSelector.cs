// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Linq;
using System;
using System.Collections.Generic;
using Data.EntityFramework;
using Data.Entities.Documents.Trade;
using Protocol.Models.Documents;
using System.Linq.Expressions;

namespace Business.Selectors;
public static partial class IncomingOrderSelectors
{
    public static Expression<Func<IncomingOrder, IncomingOrderModel>> ModelSelector(Data.Repository.IDataRepository repo) => e1 => new IncomingOrderModel{Number = e1.Number, //
 Status = e1.Status, //
 // Referring properties.
    Reference = repo.IncomingOrders.Where(e2 => e2 == e1).Select(IncomingOrderSelectors.ReferenceSelector).FirstOrDefault(), //
    GoodsSold = e1.GoodsSold.AsQueryable().Select(IncomingOrderLineSelectors.ModelSelector(repo)).ToList(), //
    Customer = repo.Customers.Where(e2 => e2 == e1.Customer).Select(CustomerSelectors.ReferenceSelector).FirstOrDefault() ?? new Protocol.Models.Counterparties.CustomerReference(), //
    Currency = repo.Currencies.Where(e2 => e2 == e1.Currency).Select(CurrencySelectors.ReferenceSelector).FirstOrDefault() ?? new Protocol.Models.ReferrableEntities.CurrencyReference(), //
    Accountable = repo.Employees.Where(e2 => e2 == e1.Accountable).Select(EmployeeSelectors.ReferenceSelector).FirstOrDefault() ?? new Protocol.Models.People.EmployeeReference(), //
    };
}