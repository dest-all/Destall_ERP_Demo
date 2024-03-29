// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.Parameters;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class IEmployeeReadOnlyModelInt32CommunicationContractModelList : IReadOnlyList<Protocol.Models.Parameters.IIEmployeeReadOnlyModelInt32CommunicationContractModel>
{
    public IEmployeeReadOnlyModelInt32CommunicationContractModelList(RepeatedField<IEmployeeReadOnlyModelInt32CommunicationContractModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.Parameters.IIEmployeeReadOnlyModelInt32CommunicationContractModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.Parameters.IIEmployeeReadOnlyModelInt32CommunicationContractModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator IEmployeeReadOnlyModelInt32CommunicationContractModelList(RepeatedField<IEmployeeReadOnlyModelInt32CommunicationContractModel> from)
    {
        var result = new IEmployeeReadOnlyModelInt32CommunicationContractModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<IEmployeeReadOnlyModelInt32CommunicationContractModel>(IEmployeeReadOnlyModelInt32CommunicationContractModelList from) => from.Message;
}

public sealed partial class IEmployeeReadOnlyModelInt32CommunicationContractModel : IIEmployeeReadOnlyModelInt32CommunicationContractModel, ICopied<IEmployeeReadOnlyModelInt32CommunicationContractModel>
{
    IEmployeeReadOnlyModelInt32CommunicationContractModel ICopied<IEmployeeReadOnlyModelInt32CommunicationContractModel>.Copy() => this.Clone();
    Protocol.Models.Parameters.IIEmployeeReadOnlyModelInt32CommunicationContractModel ICopied<Protocol.Models.Parameters.IIEmployeeReadOnlyModelInt32CommunicationContractModel>.Copy() => ((ICopied<IEmployeeReadOnlyModelInt32CommunicationContractModel>)this).Copy();
    Protocol.Models.People.IEmployeeReadOnlyModel IIEmployeeReadOnlyModelInt32CommunicationContractModel.Employee { get => Employee; }
}