// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Linq;
using System;
using System.Collections.Generic;
using Data.EntityFramework;
using Data.Entities.DataHolders.Actors;
using Protocol.Models.Counterparties;
using System.Linq.Expressions;

namespace Business.Selectors;
public static partial class SupplierSelectors
{
    public static Expression<Func<Supplier, SupplierModel>> ModelSelector(Data.Repository.IDataRepository repo) => e1 => new SupplierModel{Name = e1.Name, //
 // Referring properties.
    Reference = repo.Suppliers.Where(e2 => e2 == e1).Select(SupplierSelectors.ReferenceSelector).FirstOrDefault(), //
    };
}