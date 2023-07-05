// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using DestallMaterials.WheelProtection.Extensions.Enumerables;
using DestallMaterials.WheelProtection.Extensions.Strings;
using Data.Repository;
using Data.Entities;
using Data.Extensions;

namespace Business.Selectors
{
    public static partial class CustomerSelectors
    {
        public static Expression<Func<Data.Entities.DataHolders.Actors.Customer, bool>> Express(this IDataRepository repo, Protocol.Models.Filters.ICustomerFilterReadOnlyModel filter)
        {
            if (filter is null)
            {
                return e => true;
            }

            var possiblePatterns = repo.Set<OwnedString>();
            bool filtersName = filter.Name is not null;
            filtersName = filtersName && (filter.Name?.Pattern.HasContent() == true);
            string namePattern = filtersName ? filter.Name.Pattern.ToLower() : "";
            bool filtersReference = filter.Reference is not null;
            bool filtersReferenceIn = filter.Reference?.In?.Any() == true;
            var referenceInFilterValue = filtersReferenceIn ? filter.Reference.In : default;
            bool filtersReferenceNotIn = filter.Reference?.NotIn?.Any() == true;
            var referenceNotInFilterValue = filtersReferenceNotIn ? filter.Reference.NotIn : default;
            bool filtersReferenceRepresentation = !string.IsNullOrEmpty(filter.Reference?.Representation?.Pattern);
            string referencePattern = filter.Reference?.Representation?.Pattern?.ToLower() ?? "";
            if (!(filtersName || filtersReference))
            {
                return item => true;
            }

            return item => (!filtersName || //
 item.Name == namePattern //
            ) //
 && (!filtersReference || //
 ( //
            !filtersReferenceIn || referenceInFilterValue.Contains(item.Id) //
            ) //
 && ( //
            !filtersReferenceNotIn || !referenceNotInFilterValue.Contains(item.Id) //
            ) //
 && ( //
            !filtersReferenceRepresentation || possiblePatterns.Any(pp => pp.Id == item.Id && pp.Value == referencePattern) //
            ) //
            ) //
            ;
        }

        public static IQueryable<Data.Entities.DataHolders.Actors.Customer> OrderBy(this IQueryable<Data.Entities.DataHolders.Actors.Customer> source, IEnumerable<string> orderings)
        {
            if (orderings.IsEmpty())
            {
                return source;
            }

            IOrderedQueryable<Data.Entities.DataHolders.Actors.Customer>? orderedQueryable = null;
            foreach (var ordering in orderings)
            {
                orderedQueryable = ordering switch
                {
                    "Name" => orderedQueryable?.ThenBy(i => i.Name) ?? source.OrderBy(i => i.Name), //
                    "-Name" => orderedQueryable?.ThenByDescending(i => i.Name) ?? source.OrderByDescending(i => i.Name), //
                    "Reference" => orderedQueryable?.ThenBy(i => i.Representation) ?? source.OrderBy(i => i.Representation), //
                    "-Reference" => orderedQueryable?.ThenByDescending(i => i.Representation) ?? source.OrderByDescending(i => i.Representation), //
                    "Representation" => orderedQueryable?.ThenBy(i => i.Representation) ?? source.OrderBy(i => i.Representation), //
                    "-Representation" => orderedQueryable?.ThenByDescending(i => i.Representation) ?? source.OrderByDescending(i => i.Representation), //
                    _ => throw new ArgumentException($"Invalid ordering option {ordering}.") //
                };
            }

            return orderedQueryable;
        }
    }
}