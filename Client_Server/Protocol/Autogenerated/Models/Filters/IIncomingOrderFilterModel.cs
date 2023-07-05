// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

// Created in ModelInterproperties
using System;
using System.Linq;
using Common.Extensions.Object;
using Protocol.Models;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Models.Filters;
public interface IIncomingOrderFilterReadOnlyModel : IEquatable<IIncomingOrderFilterReadOnlyModel>, Protocol.Models.IModelBase, ICopied<IIncomingOrderFilterReadOnlyModel>
{
    Protocol.Models.Filters.IStringFilterReadOnlyModel Number { get; }

    Protocol.Models.Filters.INumberFilterReadOnlyModel Status { get; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel Reference { get; }

    Protocol.Models.Filters.ICollectionFilterReadOnlyModel GoodsSold { get; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel Customer { get; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel Currency { get; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel Accountable { get; }

    int Protocol.Models.IModelBase.ComputeChecksum()
    {
        int result = 0;
        unchecked
        {
            result += 25097 * Number?.ComputeChecksum() ?? 376455;
            result += 22869 * Status?.ComputeChecksum() ?? 343035;
            result += 31123 * Reference?.ComputeChecksum() ?? 466845;
            result += 29547 * GoodsSold?.ComputeChecksum() ?? 443205;
            result += 25451 * Customer?.ComputeChecksum() ?? 381765;
            result += 25438 * Currency?.ComputeChecksum() ?? 381570;
            result += 31923 * Accountable?.ComputeChecksum() ?? 478845;
        }

        return result;
    }

    bool IEquatable<IIncomingOrderFilterReadOnlyModel>.Equals(IIncomingOrderFilterReadOnlyModel other)
    {
        var result = Number.CompareConsideringNulls(other.Number) //
 && Status.CompareConsideringNulls(other.Status) //
 && Reference.CompareConsideringNulls(other.Reference) //
 && GoodsSold.CompareConsideringNulls(other.GoodsSold) //
 && Customer.CompareConsideringNulls(other.Customer) //
 && Currency.CompareConsideringNulls(other.Currency) //
 && Accountable.CompareConsideringNulls(other.Accountable) //
        ;
        return result;
    }
}