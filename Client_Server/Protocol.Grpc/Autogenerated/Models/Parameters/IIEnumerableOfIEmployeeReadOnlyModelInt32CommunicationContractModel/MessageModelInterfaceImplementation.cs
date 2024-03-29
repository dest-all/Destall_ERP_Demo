// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.Parameters;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModelList : IReadOnlyList<Protocol.Models.Parameters.IIEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel>
{
    public IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModelList(RepeatedField<IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.Parameters.IIEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.Parameters.IIEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModelList(RepeatedField<IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel> from)
    {
        var result = new IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel>(IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModelList from) => from.Message;
}

public sealed partial class IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel : IIEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel, ICopied<IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel>
{
    IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel ICopied<IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel>.Copy() => this.Clone();
    Protocol.Models.Parameters.IIEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel ICopied<Protocol.Models.Parameters.IIEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel>.Copy() => ((ICopied<IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel>)this).Copy();
    public IEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel(RepeatedField<EmployeeModel> @employees)
    {
        employees_ = @employees;
    }

    IReadOnlyList<Protocol.Models.People.IEmployeeReadOnlyModel> IIEnumerableOfIEmployeeReadOnlyModelInt32CommunicationContractModel.Employees => Employees;
}