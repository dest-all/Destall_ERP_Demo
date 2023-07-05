// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.Filters;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class EmployeeFilterModelList : IReadOnlyList<Protocol.Models.Filters.IEmployeeFilterReadOnlyModel>
{
    public EmployeeFilterModelList(RepeatedField<EmployeeFilterModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.Filters.IEmployeeFilterReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.Filters.IEmployeeFilterReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator EmployeeFilterModelList(RepeatedField<EmployeeFilterModel> from)
    {
        var result = new EmployeeFilterModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<EmployeeFilterModel>(EmployeeFilterModelList from) => from.Message;
}

public sealed partial class EmployeeFilterModel : IEmployeeFilterReadOnlyModel, ICopied<EmployeeFilterModel>
{
    EmployeeFilterModel ICopied<EmployeeFilterModel>.Copy() => this.Clone();
    Protocol.Models.Filters.IEmployeeFilterReadOnlyModel ICopied<Protocol.Models.Filters.IEmployeeFilterReadOnlyModel>.Copy() => ((ICopied<EmployeeFilterModel>)this).Copy();
    Protocol.Models.Filters.IStringFilterReadOnlyModel IEmployeeFilterReadOnlyModel.FirstName { get => FirstName; }

    Protocol.Models.Filters.IStringFilterReadOnlyModel IEmployeeFilterReadOnlyModel.LastName { get => LastName; }

    Protocol.Models.Filters.IReferenceFilterReadOnlyModel IEmployeeFilterReadOnlyModel.Reference { get => Reference; }
}