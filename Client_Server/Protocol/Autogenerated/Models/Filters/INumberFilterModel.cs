// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

// Created in ModelInterproperties
using System;
using System.Linq;
using Common.Extensions.Object;
using Protocol.Models;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Models.Filters;
public interface INumberFilterReadOnlyModel : IEquatable<INumberFilterReadOnlyModel>, Protocol.Models.IModelBase, ICopied<INumberFilterReadOnlyModel>
{
    System.Double LeftLimit { get; }

    System.Double RightLimit { get; }

    int Protocol.Models.IModelBase.ComputeChecksum()
    {
        int result = 0;
        unchecked
        {
            result += 35170 * LeftLimit.ComputeChecksum();
            result += 27744 * RightLimit.ComputeChecksum();
        }

        return result;
    }

    bool IEquatable<INumberFilterReadOnlyModel>.Equals(INumberFilterReadOnlyModel other)
    {
        var result = LeftLimit.CompareConsideringNulls(other.LeftLimit) //
 && RightLimit.CompareConsideringNulls(other.RightLimit) //
        ;
        return result;
    }
}