// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.PaginationRequests;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class IncomingPaymentPaginationRequestModelList : IReadOnlyList<Protocol.Models.PaginationRequests.IIncomingPaymentPaginationRequestReadOnlyModel>
{
    public IncomingPaymentPaginationRequestModelList(RepeatedField<IncomingPaymentPaginationRequestModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.PaginationRequests.IIncomingPaymentPaginationRequestReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.PaginationRequests.IIncomingPaymentPaginationRequestReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator IncomingPaymentPaginationRequestModelList(RepeatedField<IncomingPaymentPaginationRequestModel> from)
    {
        var result = new IncomingPaymentPaginationRequestModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<IncomingPaymentPaginationRequestModel>(IncomingPaymentPaginationRequestModelList from) => from.Message;
}

public sealed partial class IncomingPaymentPaginationRequestModel : IIncomingPaymentPaginationRequestReadOnlyModel, ICopied<IncomingPaymentPaginationRequestModel>
{
    IncomingPaymentPaginationRequestModel ICopied<IncomingPaymentPaginationRequestModel>.Copy() => this.Clone();
    Protocol.Models.PaginationRequests.IIncomingPaymentPaginationRequestReadOnlyModel ICopied<Protocol.Models.PaginationRequests.IIncomingPaymentPaginationRequestReadOnlyModel>.Copy() => ((ICopied<IncomingPaymentPaginationRequestModel>)this).Copy();
    public IncomingPaymentPaginationRequestModel(RepeatedField<System.String> @orderings)
    {
        orderings_ = @orderings;
    }

    IReadOnlyList<System.String> IIncomingPaymentPaginationRequestReadOnlyModel.Orderings => Orderings;
    Protocol.Models.Filters.IIncomingPaymentFilterReadOnlyModel IIncomingPaymentPaginationRequestReadOnlyModel.Filter { get => Filter; }
}