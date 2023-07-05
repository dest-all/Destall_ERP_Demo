// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

// Created in ModelInterproperties
using System;
using System.Linq;
using Common.Extensions.Object;
using Protocol.Models;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Models.Filters;
public interface IEmployeeFilterReadOnlyModel : IEquatable<IEmployeeFilterReadOnlyModel>, Protocol.Models.IModelBase, ICopied<IEmployeeFilterReadOnlyModel>
{
    Protocol.Models.Filters.IStringFilterReadOnlyModel FirstName { get; }

    Protocol.Models.Filters.IStringFilterReadOnlyModel LastName { get; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel Reference { get; }

    int Protocol.Models.IModelBase.ComputeChecksum()
    {
        int result = 0;
        unchecked
        {
            result += 29614 * FirstName?.ComputeChecksum() ?? 444210;
            result += 22913 * LastName?.ComputeChecksum() ?? 343695;
            result += 31123 * Reference?.ComputeChecksum() ?? 466845;
        }

        return result;
    }

    bool IEquatable<IEmployeeFilterReadOnlyModel>.Equals(IEmployeeFilterReadOnlyModel other)
    {
        var result = FirstName.CompareConsideringNulls(other.FirstName) //
 && LastName.CompareConsideringNulls(other.LastName) //
 && Reference.CompareConsideringNulls(other.Reference) //
        ;
        return result;
    }
}