// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.Counterparties;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class CustomerModelList : IReadOnlyList<Protocol.Models.Counterparties.ICustomerReadOnlyModel>
{
    public CustomerModelList(RepeatedField<CustomerModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.Counterparties.ICustomerReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.Counterparties.ICustomerReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator CustomerModelList(RepeatedField<CustomerModel> from)
    {
        var result = new CustomerModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<CustomerModel>(CustomerModelList from) => from.Message;
}

public sealed partial class CustomerModel : ICustomerReadOnlyModel, ICopied<CustomerModel>
{
    CustomerModel ICopied<CustomerModel>.Copy() => this.Clone();
    Protocol.Models.Counterparties.ICustomerReadOnlyModel ICopied<Protocol.Models.Counterparties.ICustomerReadOnlyModel>.Copy() => ((ICopied<CustomerModel>)this).Copy();
    Protocol.Models.Counterparties.ICustomerReadOnlyReference ICustomerReadOnlyModel.Reference { get => Reference; }
}