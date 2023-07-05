// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.PaginationRequests;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class OutgoingPaymentPaginationRequestModelList : IReadOnlyList<Protocol.Models.PaginationRequests.IOutgoingPaymentPaginationRequestReadOnlyModel>
{
    public OutgoingPaymentPaginationRequestModelList(RepeatedField<OutgoingPaymentPaginationRequestModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.PaginationRequests.IOutgoingPaymentPaginationRequestReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.PaginationRequests.IOutgoingPaymentPaginationRequestReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator OutgoingPaymentPaginationRequestModelList(RepeatedField<OutgoingPaymentPaginationRequestModel> from)
    {
        var result = new OutgoingPaymentPaginationRequestModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<OutgoingPaymentPaginationRequestModel>(OutgoingPaymentPaginationRequestModelList from) => from.Message;
}

public sealed partial class OutgoingPaymentPaginationRequestModel : IOutgoingPaymentPaginationRequestReadOnlyModel, ICopied<OutgoingPaymentPaginationRequestModel>
{
    OutgoingPaymentPaginationRequestModel ICopied<OutgoingPaymentPaginationRequestModel>.Copy() => this.Clone();
    Protocol.Models.PaginationRequests.IOutgoingPaymentPaginationRequestReadOnlyModel ICopied<Protocol.Models.PaginationRequests.IOutgoingPaymentPaginationRequestReadOnlyModel>.Copy() => ((ICopied<OutgoingPaymentPaginationRequestModel>)this).Copy();
    public OutgoingPaymentPaginationRequestModel(RepeatedField<System.String> @orderings)
    {
        orderings_ = @orderings;
    }

    IReadOnlyList<System.String> IOutgoingPaymentPaginationRequestReadOnlyModel.Orderings => Orderings;
    Protocol.Models.Filters.IOutgoingPaymentFilterReadOnlyModel IOutgoingPaymentPaginationRequestReadOnlyModel.Filter { get => Filter; }
}