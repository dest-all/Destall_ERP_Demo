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
    public static partial class WithdrawalSelectors
    {
        public static Expression<Func<Data.Entities.Documents.Finances.Withdrawal, bool>> Express(this IDataRepository repo, Protocol.Models.Filters.IWithdrawalFilterReadOnlyModel filter)
        {
            if (filter is null)
            {
                return e => true;
            }

            var possiblePatterns = repo.Set<OwnedString>();
            bool filtersSum = filter.Sum is not null;
            double sumLeftLimitFilterValue = filtersSum ? filter.Sum.LeftLimit : double.MinValue;
            double sumRightLimitFilterValue = filtersSum ? filter.Sum.RightLimit : double.MaxValue;
            bool filtersNumber = filter.Number is not null;
            filtersNumber = filtersNumber && (filter.Number?.Pattern.HasContent() == true);
            string numberPattern = filtersNumber ? filter.Number.Pattern.ToLower() : "";
            bool filtersStatus = filter.Status is not null;
            double statusLeftLimitFilterValue = filtersStatus ? filter.Status.LeftLimit : double.MinValue;
            double statusRightLimitFilterValue = filtersStatus ? filter.Status.RightLimit : double.MaxValue;
            bool filtersReference = filter.Reference is not null;
            bool filtersReferenceIn = filter.Reference?.In?.Any() == true;
            var referenceInFilterValue = filtersReferenceIn ? filter.Reference.In : default;
            bool filtersReferenceNotIn = filter.Reference?.NotIn?.Any() == true;
            var referenceNotInFilterValue = filtersReferenceNotIn ? filter.Reference.NotIn : default;
            bool filtersReferenceRepresentation = !string.IsNullOrEmpty(filter.Reference?.Representation?.Pattern);
            string referencePattern = filter.Reference?.Representation?.Pattern?.ToLower() ?? "";
            bool filtersCurrency = filter.Currency is not null;
            bool filtersCurrencyIn = filter.Currency?.In?.Any() == true;
            var currencyInFilterValue = filtersCurrencyIn ? filter.Currency.In : default;
            bool filtersCurrencyNotIn = filter.Currency?.NotIn?.Any() == true;
            var currencyNotInFilterValue = filtersCurrencyNotIn ? filter.Currency.NotIn : default;
            bool filtersCurrencyRepresentation = !string.IsNullOrEmpty(filter.Currency?.Representation?.Pattern);
            string currencyPattern = filter.Currency?.Representation?.Pattern?.ToLower() ?? "";
            bool filtersAccountable = filter.Accountable is not null;
            bool filtersAccountableIn = filter.Accountable?.In?.Any() == true;
            var accountableInFilterValue = filtersAccountableIn ? filter.Accountable.In : default;
            bool filtersAccountableNotIn = filter.Accountable?.NotIn?.Any() == true;
            var accountableNotInFilterValue = filtersAccountableNotIn ? filter.Accountable.NotIn : default;
            bool filtersAccountableRepresentation = !string.IsNullOrEmpty(filter.Accountable?.Representation?.Pattern);
            string accountablePattern = filter.Accountable?.Representation?.Pattern?.ToLower() ?? "";
            if (!(filtersSum || filtersNumber || filtersStatus || filtersReference || filtersCurrency || filtersAccountable))
            {
                return item => true;
            }

            return item => (!filtersSum || //
 item.Sum >= sumLeftLimitFilterValue && item.Sum <= sumRightLimitFilterValue //
            ) //
 && (!filtersNumber || //
 item.Number == numberPattern //
            ) //
 && (!filtersStatus || //
 item.Status >= statusLeftLimitFilterValue && item.Status <= statusRightLimitFilterValue //
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
 && (!filtersCurrency || //
 ( //
            !filtersCurrencyIn || currencyInFilterValue.Contains(item.Currency.Id) //
            ) //
 && ( //
            !filtersCurrencyNotIn || !currencyNotInFilterValue.Contains(item.Currency.Id) // 
            ) //
 && ( //
            !filtersCurrencyRepresentation || possiblePatterns.Any(pp => pp.Id == item.Id && pp.Value == currencyPattern) //
            ) //
            ) //
 && (!filtersAccountable || //
 ( //
            !filtersAccountableIn || accountableInFilterValue.Contains(item.Accountable.Id) //
            ) //
 && ( //
            !filtersAccountableNotIn || !accountableNotInFilterValue.Contains(item.Accountable.Id) // 
            ) //
 && ( //
            !filtersAccountableRepresentation || possiblePatterns.Any(pp => pp.Id == item.Id && pp.Value == accountablePattern) //
            ) //
            ) //
            ;
        }

        public static IQueryable<Data.Entities.Documents.Finances.Withdrawal> OrderBy(this IQueryable<Data.Entities.Documents.Finances.Withdrawal> source, IEnumerable<string> orderings)
        {
            if (orderings.IsEmpty())
            {
                return source;
            }

            IOrderedQueryable<Data.Entities.Documents.Finances.Withdrawal>? orderedQueryable = null;
            foreach (var ordering in orderings)
            {
                orderedQueryable = ordering switch
                {
                    "Sum" => orderedQueryable?.ThenBy(i => i.Sum) ?? source.OrderBy(i => i.Sum), //
                    "-Sum" => orderedQueryable?.ThenByDescending(i => i.Sum) ?? source.OrderByDescending(i => i.Sum), //
                    "Number" => orderedQueryable?.ThenBy(i => i.Number) ?? source.OrderBy(i => i.Number), //
                    "-Number" => orderedQueryable?.ThenByDescending(i => i.Number) ?? source.OrderByDescending(i => i.Number), //
                    "Status" => orderedQueryable?.ThenBy(i => i.Status) ?? source.OrderBy(i => i.Status), //
                    "-Status" => orderedQueryable?.ThenByDescending(i => i.Status) ?? source.OrderByDescending(i => i.Status), //
                    "Reference" => orderedQueryable?.ThenBy(i => i.Representation) ?? source.OrderBy(i => i.Representation), //
                    "-Reference" => orderedQueryable?.ThenByDescending(i => i.Representation) ?? source.OrderByDescending(i => i.Representation), //
                    "Representation" => orderedQueryable?.ThenBy(i => i.Representation) ?? source.OrderBy(i => i.Representation), //
                    "-Representation" => orderedQueryable?.ThenByDescending(i => i.Representation) ?? source.OrderByDescending(i => i.Representation), //
                    "Currency" => orderedQueryable?.ThenBy(i => i.Currency.Representation) ?? source.OrderBy(i => i.Currency.Representation), //
                    "-Currency" => orderedQueryable?.ThenByDescending(i => i.Currency.Representation) ?? source.OrderByDescending(i => i.Currency.Representation), //
                    "Accountable" => orderedQueryable?.ThenBy(i => i.Accountable.Representation) ?? source.OrderBy(i => i.Accountable.Representation), //
                    "-Accountable" => orderedQueryable?.ThenByDescending(i => i.Accountable.Representation) ?? source.OrderByDescending(i => i.Accountable.Representation), //
                    _ => throw new ArgumentException($"Invalid ordering option {ordering}.") //
                };
            }

            return orderedQueryable;
        }
    }
}