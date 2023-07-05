// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Models.FinancialDocuments;
public interface IStaffPaycheckReadOnlyReference : Protocol.Models.IReference<Protocol.Models.FinancialDocuments.IStaffPaycheckReadOnlyModel>, IEquatable<IStaffPaycheckReadOnlyReference>, ICopied<IStaffPaycheckReadOnlyReference>
{
    bool IEquatable<IStaffPaycheckReadOnlyReference>.Equals(IStaffPaycheckReadOnlyReference other) => Id == other.Id;
    int IModelBase.ComputeChecksum()
    {
        unchecked
        {
            var result = 222515 * (int)(Id + 2);
            return result;
        }
    }
}