// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Collections.Generic;
using DestallMaterials.WheelProtection.Copying;
using System.Linq;
using MemoryPack;

namespace Protocol.Models.PaginationRequests;
[MemoryPackable]
public partial class OutgoingPaymentPaginationRequestModel : Protocol.Models.ModelBase, Protocol.Models.PaginationRequests.IOutgoingPaymentPaginationRequestReadOnlyModel, ICopied<OutgoingPaymentPaginationRequestModel>, IPackable<OutgoingPaymentPaginationRequestModel>
{
    public byte[] Pack() => MemoryPackSerializer.Serialize(this);
    public static OutgoingPaymentPaginationRequestModel Unpack(byte[] bytes) => MemoryPackSerializer.Deserialize<OutgoingPaymentPaginationRequestModel>(bytes);
    public OutgoingPaymentPaginationRequestModel Copy() => new(this);
    Protocol.Models.PaginationRequests.IOutgoingPaymentPaginationRequestReadOnlyModel ICopied<Protocol.Models.PaginationRequests.IOutgoingPaymentPaginationRequestReadOnlyModel>.Copy() => ((ICopied<OutgoingPaymentPaginationRequestModel>)this).Copy();
    System.UInt32 IOutgoingPaymentPaginationRequestReadOnlyModel.PageNumber => this.PageNumber;
    public System.UInt32 PageNumber { get; set; }

    System.UInt32 IOutgoingPaymentPaginationRequestReadOnlyModel.Limit => this.Limit;
    public System.UInt32 Limit { get; set; }

    IReadOnlyList<string> IOutgoingPaymentPaginationRequestReadOnlyModel.Orderings => this.Orderings;
    public List<string> Orderings { get; set; } = new();
    Protocol.Models.Filters.IOutgoingPaymentFilterReadOnlyModel IOutgoingPaymentPaginationRequestReadOnlyModel.Filter => this.Filter;
    public Protocol.Models.Filters.OutgoingPaymentFilterModel Filter { get; set; } = new();
    public OutgoingPaymentPaginationRequestModel()
    {
    }

    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    [MemoryPackConstructor]
    public OutgoingPaymentPaginationRequestModel(System.UInt32 @pageNumber, //
 System.UInt32 @limit, //
 List<string> @orderings, //
 Protocol.Models.Filters.OutgoingPaymentFilterModel @filter//
    )
    {
        PageNumber = @pageNumber;
        Limit = @limit;
        Filter = @filter;
        Orderings = @orderings;
    }

    public OutgoingPaymentPaginationRequestModel(IOutgoingPaymentPaginationRequestReadOnlyModel from)
    {
        this.PageNumber = from.PageNumber;
        this.Limit = from.Limit;
        Orderings = from?.Orderings.ToList();
        Filter = from.Filter is null ? new() : new(from.Filter);
    }
}