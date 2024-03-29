// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

// Created in ModelInterproperties
using System;
using System.Linq;
using Common.Extensions.Object;
using Protocol.Models;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Models.People;
public interface IEmployeeReadOnlyModel : IEquatable<IEmployeeReadOnlyModel>, Protocol.Models.IModelBase, ICopied<IEmployeeReadOnlyModel>, IReferrableModel
{
    System.String FirstName { get; }

    System.String LastName { get; }

    new Protocol.Models.People.IEmployeeReadOnlyReference Reference { get; }

    IReference IReferrableModel.Reference => this.Reference;
    int Protocol.Models.IModelBase.ComputeChecksum()
    {
        int result = 0;
        unchecked
        {
            result += 29614 * FirstName.ComputeChecksum();
            result += 22913 * LastName.ComputeChecksum();
            result += 31123 * Reference?.ComputeChecksum() ?? 466845;
        }

        return result;
    }

    bool IEquatable<IEmployeeReadOnlyModel>.Equals(IEmployeeReadOnlyModel other)
    {
        var result = FirstName.CompareConsideringNulls(other.FirstName) //
 && LastName.CompareConsideringNulls(other.LastName) //
 && Reference.CompareConsideringNulls(other.Reference) //
        ;
        return result;
    }
}