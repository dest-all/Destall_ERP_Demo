// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using System.Collections.Generic;
using DestallMaterials.WheelProtection.Copying;
using System.Linq;
using MemoryPack;

namespace Protocol.Models.PaginationRequests;
[MemoryPackable]
public partial class EmployeePaginationRequestModel : Protocol.Models.ModelBase, Protocol.Models.PaginationRequests.IEmployeePaginationRequestReadOnlyModel, ICopied<EmployeePaginationRequestModel>, IPackable<EmployeePaginationRequestModel>
{
    public byte[] Pack() => MemoryPackSerializer.Serialize(this);
    public static EmployeePaginationRequestModel Unpack(byte[] bytes) => MemoryPackSerializer.Deserialize<EmployeePaginationRequestModel>(bytes);
    public EmployeePaginationRequestModel Copy() => new(this);
    Protocol.Models.PaginationRequests.IEmployeePaginationRequestReadOnlyModel ICopied<Protocol.Models.PaginationRequests.IEmployeePaginationRequestReadOnlyModel>.Copy() => ((ICopied<EmployeePaginationRequestModel>)this).Copy();
    System.UInt32 IEmployeePaginationRequestReadOnlyModel.PageNumber => this.PageNumber;
    public System.UInt32 PageNumber { get; set; }

    System.UInt32 IEmployeePaginationRequestReadOnlyModel.Limit => this.Limit;
    public System.UInt32 Limit { get; set; }

    IReadOnlyList<string> IEmployeePaginationRequestReadOnlyModel.Orderings => this.Orderings;
    public List<string> Orderings { get; set; } = new();
    Protocol.Models.Filters.IEmployeeFilterReadOnlyModel IEmployeePaginationRequestReadOnlyModel.Filter => this.Filter;
    public Protocol.Models.Filters.EmployeeFilterModel Filter { get; set; } = new();
    public EmployeePaginationRequestModel()
    {
    }

    [JsonConstructor]
    [System.Text.Json.Serialization.JsonConstructor]
    [MemoryPackConstructor]
    public EmployeePaginationRequestModel(System.UInt32 @pageNumber, //
 System.UInt32 @limit, //
 List<string> @orderings, //
 Protocol.Models.Filters.EmployeeFilterModel @filter//
    )
    {
        PageNumber = @pageNumber;
        Limit = @limit;
        Filter = @filter;
        Orderings = @orderings;
    }

    public EmployeePaginationRequestModel(IEmployeePaginationRequestReadOnlyModel from)
    {
        this.PageNumber = from.PageNumber;
        this.Limit = from.Limit;
        Orderings = from?.Orderings.ToList();
        Filter = from.Filter is null ? new() : new(from.Filter);
    }
}