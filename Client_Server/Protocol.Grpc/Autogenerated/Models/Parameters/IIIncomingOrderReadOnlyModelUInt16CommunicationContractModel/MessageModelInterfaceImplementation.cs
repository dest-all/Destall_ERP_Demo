// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.Parameters;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class IIncomingOrderReadOnlyModelUInt16CommunicationContractModelList : IReadOnlyList<Protocol.Models.Parameters.IIIncomingOrderReadOnlyModelUInt16CommunicationContractModel>
{
    public IIncomingOrderReadOnlyModelUInt16CommunicationContractModelList(RepeatedField<IIncomingOrderReadOnlyModelUInt16CommunicationContractModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.Parameters.IIIncomingOrderReadOnlyModelUInt16CommunicationContractModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.Parameters.IIIncomingOrderReadOnlyModelUInt16CommunicationContractModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator IIncomingOrderReadOnlyModelUInt16CommunicationContractModelList(RepeatedField<IIncomingOrderReadOnlyModelUInt16CommunicationContractModel> from)
    {
        var result = new IIncomingOrderReadOnlyModelUInt16CommunicationContractModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<IIncomingOrderReadOnlyModelUInt16CommunicationContractModel>(IIncomingOrderReadOnlyModelUInt16CommunicationContractModelList from) => from.Message;
}

public sealed partial class IIncomingOrderReadOnlyModelUInt16CommunicationContractModel : IIIncomingOrderReadOnlyModelUInt16CommunicationContractModel, ICopied<IIncomingOrderReadOnlyModelUInt16CommunicationContractModel>
{
    IIncomingOrderReadOnlyModelUInt16CommunicationContractModel ICopied<IIncomingOrderReadOnlyModelUInt16CommunicationContractModel>.Copy() => this.Clone();
    Protocol.Models.Parameters.IIIncomingOrderReadOnlyModelUInt16CommunicationContractModel ICopied<Protocol.Models.Parameters.IIIncomingOrderReadOnlyModelUInt16CommunicationContractModel>.Copy() => ((ICopied<IIncomingOrderReadOnlyModelUInt16CommunicationContractModel>)this).Copy();
    System.UInt16 IIIncomingOrderReadOnlyModelUInt16CommunicationContractModel.TargetStatus { get => (ushort)TargetStatus; }

    Protocol.Models.Documents.IIncomingOrderReadOnlyModel IIIncomingOrderReadOnlyModelUInt16CommunicationContractModel.Item { get => Item; }
}