// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Linq;
using System;
using System.Collections.Generic;
using Data.EntityFramework;
using Data.Entities.Documents.Finances;
using Protocol.Models.FinancialDocuments;
using System.Linq.Expressions;

namespace Business.Selectors;
public static partial class IncomingPaymentSelectors
{
    public static Expression<Func<IncomingPayment, IncomingPaymentModel>> ModelSelector(Data.Repository.IDataRepository repo) => e1 => new IncomingPaymentModel{Sum = e1.Sum, //
 Number = e1.Number, //
 Status = e1.Status, //
 // Referring properties.
    Reference = repo.IncomingPayments.Where(e2 => e2 == e1).Select(IncomingPaymentSelectors.ReferenceSelector).FirstOrDefault(), //
    Order = repo.IncomingOrders.Where(e2 => e2 == e1.Order).Select(IncomingOrderSelectors.ReferenceSelector).FirstOrDefault() ?? new Protocol.Models.Documents.IncomingOrderReference(), //
    Payer = repo.Customers.Where(e2 => e2 == e1.Payer).Select(CustomerSelectors.ReferenceSelector).FirstOrDefault() ?? new Protocol.Models.Counterparties.CustomerReference(), //
    Currency = repo.Currencies.Where(e2 => e2 == e1.Currency).Select(CurrencySelectors.ReferenceSelector).FirstOrDefault() ?? new Protocol.Models.ReferrableEntities.CurrencyReference(), //
    Accountable = repo.Employees.Where(e2 => e2 == e1.Accountable).Select(EmployeeSelectors.ReferenceSelector).FirstOrDefault() ?? new Protocol.Models.People.EmployeeReference(), //
    };
}