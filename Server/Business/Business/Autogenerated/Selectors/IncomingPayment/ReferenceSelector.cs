// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models;
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
    public static readonly Expression<Func<IncomingPayment, IncomingPaymentReference>> ReferenceSelector = //
 e1 => new Protocol.Models.FinancialDocuments.IncomingPaymentReference //
    {Id = e1.Id, //
 Representation = e1.Representation //
    };
}