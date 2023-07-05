// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.DataHolders;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class GoodModelList : IReadOnlyList<Protocol.Models.DataHolders.IGoodReadOnlyModel>
{
    public GoodModelList(RepeatedField<GoodModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.DataHolders.IGoodReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.DataHolders.IGoodReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator GoodModelList(RepeatedField<GoodModel> from)
    {
        var result = new GoodModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<GoodModel>(GoodModelList from) => from.Message;
}

public sealed partial class GoodModel : IGoodReadOnlyModel, ICopied<GoodModel>
{
    GoodModel ICopied<GoodModel>.Copy() => this.Clone();
    Protocol.Models.DataHolders.IGoodReadOnlyModel ICopied<Protocol.Models.DataHolders.IGoodReadOnlyModel>.Copy() => ((ICopied<GoodModel>)this).Copy();
    Protocol.Models.DataHolders.IGoodReadOnlyReference IGoodReadOnlyModel.Reference { get => Reference; }
}