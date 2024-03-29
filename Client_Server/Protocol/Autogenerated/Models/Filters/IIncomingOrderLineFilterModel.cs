// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

// Created in ModelInterproperties
using System;
using System.Linq;
using Common.Extensions.Object;
using Protocol.Models;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Models.Filters;
public interface IIncomingOrderLineFilterReadOnlyModel : IEquatable<IIncomingOrderLineFilterReadOnlyModel>, Protocol.Models.IModelBase, ICopied<IIncomingOrderLineFilterReadOnlyModel>
{
    Protocol.Models.Filters.INumberFilterReadOnlyModel Quantity { get; }

    Protocol.Models.Filters.INumberFilterReadOnlyModel Price { get; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IncomingOrder { get; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel Good { get; }

    int Protocol.Models.IModelBase.ComputeChecksum()
    {
        int result = 0;
        unchecked
        {
            result += 20791 * Quantity?.ComputeChecksum() ?? 311865;
            result += 11242 * Price?.ComputeChecksum() ?? 168630;
            result += 37695 * IncomingOrder?.ComputeChecksum() ?? 565425;
            result += 12434 * Good?.ComputeChecksum() ?? 186510;
        }

        return result;
    }

    bool IEquatable<IIncomingOrderLineFilterReadOnlyModel>.Equals(IIncomingOrderLineFilterReadOnlyModel other)
    {
        var result = Quantity.CompareConsideringNulls(other.Quantity) //
 && Price.CompareConsideringNulls(other.Price) //
 && IncomingOrder.CompareConsideringNulls(other.IncomingOrder) //
 && Good.CompareConsideringNulls(other.Good) //
        ;
        return result;
    }
}