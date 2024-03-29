// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.StatusMovementRegistryEntries;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class AccountEntryModelList : IReadOnlyList<Protocol.Models.StatusMovementRegistryEntries.IAccountEntryReadOnlyModel>
{
    public AccountEntryModelList(RepeatedField<AccountEntryModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.StatusMovementRegistryEntries.IAccountEntryReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.StatusMovementRegistryEntries.IAccountEntryReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator AccountEntryModelList(RepeatedField<AccountEntryModel> from)
    {
        var result = new AccountEntryModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<AccountEntryModel>(AccountEntryModelList from) => from.Message;
}

public sealed partial class AccountEntryModel : IAccountEntryReadOnlyModel, ICopied<AccountEntryModel>
{
    AccountEntryModel ICopied<AccountEntryModel>.Copy() => this.Clone();
    Protocol.Models.StatusMovementRegistryEntries.IAccountEntryReadOnlyModel ICopied<Protocol.Models.StatusMovementRegistryEntries.IAccountEntryReadOnlyModel>.Copy() => ((ICopied<AccountEntryModel>)this).Copy();
    System.UInt16 IAccountEntryReadOnlyModel.Status { get => (ushort)Status; }

    System.DateTime IAccountEntryReadOnlyModel.RegisteredAt { get => RegisteredAt.ToDateTime().ToLocalTime(); }

    Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference IAccountEntryReadOnlyModel.Currency { get => Currency; }

    Protocol.Models.Counterparties.ISupplierReadOnlyReference IAccountEntryReadOnlyModel.Creditor { get => Creditor; }

    Protocol.Models.Counterparties.ICustomerReadOnlyReference IAccountEntryReadOnlyModel.Debtor { get => Debtor; }
}