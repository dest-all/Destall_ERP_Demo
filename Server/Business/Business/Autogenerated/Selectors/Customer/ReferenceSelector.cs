// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using Data.EntityFramework;
using Data.Entities.DataHolders.Actors;
using Protocol.Models.Counterparties;
using System.Linq.Expressions;

namespace Business.Selectors;
public static partial class CustomerSelectors
{
    public static readonly Expression<Func<Customer, CustomerReference>> ReferenceSelector = //
 e1 => new Protocol.Models.Counterparties.CustomerReference //
    {Id = e1.Id, //
 Representation = e1.Representation //
    };
}