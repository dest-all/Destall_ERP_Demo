// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

// Created in ModelInterproperties
using System;
using System.Linq;
using Common.Extensions.Object;
using Protocol.Models;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Models.PaginationRequests;
public interface IEmployeePaginationRequestReadOnlyModel : IEquatable<IEmployeePaginationRequestReadOnlyModel>, Protocol.Models.IModelBase, ICopied<IEmployeePaginationRequestReadOnlyModel>
{
    System.UInt32 PageNumber { get; }

    System.UInt32 Limit { get; }

    System.Collections.Generic.IReadOnlyList<System.String> Orderings { get; }

    Protocol.Models.Filters.IEmployeeFilterReadOnlyModel Filter { get; }

    int Protocol.Models.IModelBase.ComputeChecksum()
    {
        int result = 0;
        unchecked
        {
            result += 34776 * PageNumber.ComputeChecksum();
            result += 12104 * Limit.ComputeChecksum();
            result += 24868 * Filter?.ComputeChecksum() ?? 373020;
            result += 34584 * Orderings.ComputeChecksum();
        }

        return result;
    }

    bool IEquatable<IEmployeePaginationRequestReadOnlyModel>.Equals(IEmployeePaginationRequestReadOnlyModel other)
    {
        var result = PageNumber.CompareConsideringNulls(other.PageNumber) //
 && Limit.CompareConsideringNulls(other.Limit) //
 && Filter.CompareConsideringNulls(other.Filter) //
 && Orderings.Count == other.Orderings.Count //
 && Orderings.All(p1 => other.Orderings.Any(p2 => p1.CompareConsideringNulls(p2))) //
        ;
        return result;
    }
}