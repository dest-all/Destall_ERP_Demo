// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

// Created in ModelInterproperties
using System;
using System.Linq;
using Common.Extensions.Object;
using Protocol.Models;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Models.Filters;
public interface IReferenceCollectionFilterReadOnlyModel : IEquatable<IReferenceCollectionFilterReadOnlyModel>, Protocol.Models.IModelBase, ICopied<IReferenceCollectionFilterReadOnlyModel>
{
    Protocol.Models.Filters.IReferenceFilterReadOnlyModel Any { get; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel All { get; }

    int Protocol.Models.IModelBase.ComputeChecksum()
    {
        int result = 0;
        unchecked
        {
            result += 14752 * Any?.ComputeChecksum() ?? 221280;
            result += 11773 * All?.ComputeChecksum() ?? 176595;
        }

        return result;
    }

    bool IEquatable<IReferenceCollectionFilterReadOnlyModel>.Equals(IReferenceCollectionFilterReadOnlyModel other)
    {
        var result = Any.CompareConsideringNulls(other.Any) //
 && All.CompareConsideringNulls(other.All) //
        ;
        return result;
    }
}