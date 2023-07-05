// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.Transportables;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class StartSessionResponseModelList : IReadOnlyList<Protocol.Models.Transportables.IStartSessionResponseReadOnlyModel>
{
    public StartSessionResponseModelList(RepeatedField<StartSessionResponseModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.Transportables.IStartSessionResponseReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.Transportables.IStartSessionResponseReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator StartSessionResponseModelList(RepeatedField<StartSessionResponseModel> from)
    {
        var result = new StartSessionResponseModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<StartSessionResponseModel>(StartSessionResponseModelList from) => from.Message;
}

public sealed partial class StartSessionResponseModel : IStartSessionResponseReadOnlyModel, ICopied<StartSessionResponseModel>
{
    StartSessionResponseModel ICopied<StartSessionResponseModel>.Copy() => this.Clone();
    Protocol.Models.Transportables.IStartSessionResponseReadOnlyModel ICopied<Protocol.Models.Transportables.IStartSessionResponseReadOnlyModel>.Copy() => ((ICopied<StartSessionResponseModel>)this).Copy();
    Protocol.Models.DataHolders.IUserReadOnlyModel IStartSessionResponseReadOnlyModel.User { get => User; }
}