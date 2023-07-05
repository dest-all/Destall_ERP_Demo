// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.Filters;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class CustomerFilterModelList : IReadOnlyList<Protocol.Models.Filters.ICustomerFilterReadOnlyModel>
{
    public CustomerFilterModelList(RepeatedField<CustomerFilterModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.Filters.ICustomerFilterReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.Filters.ICustomerFilterReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator CustomerFilterModelList(RepeatedField<CustomerFilterModel> from)
    {
        var result = new CustomerFilterModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<CustomerFilterModel>(CustomerFilterModelList from) => from.Message;
}

public sealed partial class CustomerFilterModel : ICustomerFilterReadOnlyModel, ICopied<CustomerFilterModel>
{
    CustomerFilterModel ICopied<CustomerFilterModel>.Copy() => this.Clone();
    Protocol.Models.Filters.ICustomerFilterReadOnlyModel ICopied<Protocol.Models.Filters.ICustomerFilterReadOnlyModel>.Copy() => ((ICopied<CustomerFilterModel>)this).Copy();
    Protocol.Models.Filters.IStringFilterReadOnlyModel ICustomerFilterReadOnlyModel.Name { get => Name; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel ICustomerFilterReadOnlyModel.Reference { get => Reference; }
}