// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Linq;
using System;
using System.Collections.Generic;
using Data.EntityFramework;
using Data.Entities;
using Protocol.Models.ReferrableEntities;
using System.Linq.Expressions;

namespace Business.Selectors;
public static partial class CurrencySelectors
{
    public static Expression<Func<Currency, CurrencyModel>> ModelSelector(Data.Repository.IDataRepository repo) => e1 => new CurrencyModel{Name = e1.Name, //
 Primary = e1.Primary, //
 // Referring properties.
    Reference = repo.Currencies.Where(e2 => e2 == e1).Select(CurrencySelectors.ReferenceSelector).FirstOrDefault(), //
    };
}