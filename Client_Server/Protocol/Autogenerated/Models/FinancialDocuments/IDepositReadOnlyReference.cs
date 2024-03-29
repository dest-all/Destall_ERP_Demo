// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Models.FinancialDocuments;
public interface IDepositReadOnlyReference : Protocol.Models.IReference<Protocol.Models.FinancialDocuments.IDepositReadOnlyModel>, IEquatable<IDepositReadOnlyReference>, ICopied<IDepositReadOnlyReference>
{
    bool IEquatable<IDepositReadOnlyReference>.Equals(IDepositReadOnlyReference other) => Id == other.Id;
    int IModelBase.ComputeChecksum()
    {
        unchecked
        {
            var result = 210447 * (int)(Id + 2);
            return result;
        }
    }
}