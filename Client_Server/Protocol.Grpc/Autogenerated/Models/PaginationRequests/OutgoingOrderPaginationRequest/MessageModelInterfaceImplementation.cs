// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.PaginationRequests;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class OutgoingOrderPaginationRequestModelList : IReadOnlyList<Protocol.Models.PaginationRequests.IOutgoingOrderPaginationRequestReadOnlyModel>
{
    public OutgoingOrderPaginationRequestModelList(RepeatedField<OutgoingOrderPaginationRequestModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.PaginationRequests.IOutgoingOrderPaginationRequestReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.PaginationRequests.IOutgoingOrderPaginationRequestReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator OutgoingOrderPaginationRequestModelList(RepeatedField<OutgoingOrderPaginationRequestModel> from)
    {
        var result = new OutgoingOrderPaginationRequestModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<OutgoingOrderPaginationRequestModel>(OutgoingOrderPaginationRequestModelList from) => from.Message;
}

public sealed partial class OutgoingOrderPaginationRequestModel : IOutgoingOrderPaginationRequestReadOnlyModel, ICopied<OutgoingOrderPaginationRequestModel>
{
    OutgoingOrderPaginationRequestModel ICopied<OutgoingOrderPaginationRequestModel>.Copy() => this.Clone();
    Protocol.Models.PaginationRequests.IOutgoingOrderPaginationRequestReadOnlyModel ICopied<Protocol.Models.PaginationRequests.IOutgoingOrderPaginationRequestReadOnlyModel>.Copy() => ((ICopied<OutgoingOrderPaginationRequestModel>)this).Copy();
    public OutgoingOrderPaginationRequestModel(RepeatedField<System.String> @orderings)
    {
        orderings_ = @orderings;
    }

    IReadOnlyList<System.String> IOutgoingOrderPaginationRequestReadOnlyModel.Orderings => Orderings;
    Protocol.Models.Filters.IOutgoingOrderFilterReadOnlyModel IOutgoingOrderPaginationRequestReadOnlyModel.Filter { get => Filter; }
}