// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.PaginationRequests;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class EmployeePaginationRequestModelList : IReadOnlyList<Protocol.Models.PaginationRequests.IEmployeePaginationRequestReadOnlyModel>
{
    public EmployeePaginationRequestModelList(RepeatedField<EmployeePaginationRequestModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.PaginationRequests.IEmployeePaginationRequestReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.PaginationRequests.IEmployeePaginationRequestReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator EmployeePaginationRequestModelList(RepeatedField<EmployeePaginationRequestModel> from)
    {
        var result = new EmployeePaginationRequestModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<EmployeePaginationRequestModel>(EmployeePaginationRequestModelList from) => from.Message;
}

public sealed partial class EmployeePaginationRequestModel : IEmployeePaginationRequestReadOnlyModel, ICopied<EmployeePaginationRequestModel>
{
    EmployeePaginationRequestModel ICopied<EmployeePaginationRequestModel>.Copy() => this.Clone();
    Protocol.Models.PaginationRequests.IEmployeePaginationRequestReadOnlyModel ICopied<Protocol.Models.PaginationRequests.IEmployeePaginationRequestReadOnlyModel>.Copy() => ((ICopied<EmployeePaginationRequestModel>)this).Copy();
    public EmployeePaginationRequestModel(RepeatedField<System.String> @orderings)
    {
        orderings_ = @orderings;
    }

    IReadOnlyList<System.String> IEmployeePaginationRequestReadOnlyModel.Orderings => Orderings;
    Protocol.Models.Filters.IEmployeeFilterReadOnlyModel IEmployeePaginationRequestReadOnlyModel.Filter { get => Filter; }
}