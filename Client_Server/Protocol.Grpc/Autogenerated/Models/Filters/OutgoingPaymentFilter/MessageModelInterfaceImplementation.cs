// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.Filters;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class OutgoingPaymentFilterModelList : IReadOnlyList<Protocol.Models.Filters.IOutgoingPaymentFilterReadOnlyModel>
{
    public OutgoingPaymentFilterModelList(RepeatedField<OutgoingPaymentFilterModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.Filters.IOutgoingPaymentFilterReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.Filters.IOutgoingPaymentFilterReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator OutgoingPaymentFilterModelList(RepeatedField<OutgoingPaymentFilterModel> from)
    {
        var result = new OutgoingPaymentFilterModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<OutgoingPaymentFilterModel>(OutgoingPaymentFilterModelList from) => from.Message;
}

public sealed partial class OutgoingPaymentFilterModel : IOutgoingPaymentFilterReadOnlyModel, ICopied<OutgoingPaymentFilterModel>
{
    OutgoingPaymentFilterModel ICopied<OutgoingPaymentFilterModel>.Copy() => this.Clone();
    Protocol.Models.Filters.IOutgoingPaymentFilterReadOnlyModel ICopied<Protocol.Models.Filters.IOutgoingPaymentFilterReadOnlyModel>.Copy() => ((ICopied<OutgoingPaymentFilterModel>)this).Copy();
    Protocol.Models.Filters.INumberFilterReadOnlyModel IOutgoingPaymentFilterReadOnlyModel.Sum { get => Sum; }

    Protocol.Models.Filters.IStringFilterReadOnlyModel IOutgoingPaymentFilterReadOnlyModel.Number { get => Number; }

    Protocol.Models.Filters.INumberFilterReadOnlyModel IOutgoingPaymentFilterReadOnlyModel.Status { get => Status; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IOutgoingPaymentFilterReadOnlyModel.Reference { get => Reference; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IOutgoingPaymentFilterReadOnlyModel.Order { get => Order; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IOutgoingPaymentFilterReadOnlyModel.Receiver { get => Receiver; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IOutgoingPaymentFilterReadOnlyModel.Currency { get => Currency; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IOutgoingPaymentFilterReadOnlyModel.Accountable { get => Accountable; }
}