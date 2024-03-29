// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.Parameters;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class Int32Int32CommunicationContractModelList : IReadOnlyList<Protocol.Models.Parameters.IInt32Int32CommunicationContractModel>
{
    public Int32Int32CommunicationContractModelList(RepeatedField<Int32Int32CommunicationContractModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.Parameters.IInt32Int32CommunicationContractModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.Parameters.IInt32Int32CommunicationContractModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator Int32Int32CommunicationContractModelList(RepeatedField<Int32Int32CommunicationContractModel> from)
    {
        var result = new Int32Int32CommunicationContractModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<Int32Int32CommunicationContractModel>(Int32Int32CommunicationContractModelList from) => from.Message;
}

public sealed partial class Int32Int32CommunicationContractModel : IInt32Int32CommunicationContractModel, ICopied<Int32Int32CommunicationContractModel>
{
    Int32Int32CommunicationContractModel ICopied<Int32Int32CommunicationContractModel>.Copy() => this.Clone();
    Protocol.Models.Parameters.IInt32Int32CommunicationContractModel ICopied<Protocol.Models.Parameters.IInt32Int32CommunicationContractModel>.Copy() => ((ICopied<Int32Int32CommunicationContractModel>)this).Copy();
}