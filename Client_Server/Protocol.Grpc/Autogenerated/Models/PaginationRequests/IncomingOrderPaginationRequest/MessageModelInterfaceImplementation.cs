// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.PaginationRequests;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class IncomingOrderPaginationRequestModelList : IReadOnlyList<Protocol.Models.PaginationRequests.IIncomingOrderPaginationRequestReadOnlyModel>
{
    public IncomingOrderPaginationRequestModelList(RepeatedField<IncomingOrderPaginationRequestModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.PaginationRequests.IIncomingOrderPaginationRequestReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.PaginationRequests.IIncomingOrderPaginationRequestReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator IncomingOrderPaginationRequestModelList(RepeatedField<IncomingOrderPaginationRequestModel> from)
    {
        var result = new IncomingOrderPaginationRequestModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<IncomingOrderPaginationRequestModel>(IncomingOrderPaginationRequestModelList from) => from.Message;
}

public sealed partial class IncomingOrderPaginationRequestModel : IIncomingOrderPaginationRequestReadOnlyModel, ICopied<IncomingOrderPaginationRequestModel>
{
    IncomingOrderPaginationRequestModel ICopied<IncomingOrderPaginationRequestModel>.Copy() => this.Clone();
    Protocol.Models.PaginationRequests.IIncomingOrderPaginationRequestReadOnlyModel ICopied<Protocol.Models.PaginationRequests.IIncomingOrderPaginationRequestReadOnlyModel>.Copy() => ((ICopied<IncomingOrderPaginationRequestModel>)this).Copy();
    public IncomingOrderPaginationRequestModel(RepeatedField<System.String> @orderings)
    {
        orderings_ = @orderings;
    }

    IReadOnlyList<System.String> IIncomingOrderPaginationRequestReadOnlyModel.Orderings => Orderings;
    Protocol.Models.Filters.IIncomingOrderFilterReadOnlyModel IIncomingOrderPaginationRequestReadOnlyModel.Filter { get => Filter; }
}