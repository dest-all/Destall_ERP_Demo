// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.Parameters;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelList : IReadOnlyList<Protocol.Models.Parameters.IInt32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel>
{
    public Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelList(RepeatedField<Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.Parameters.IInt32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.Parameters.IInt32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelList(RepeatedField<Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel> from)
    {
        var result = new Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel>(Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModelList from) => from.Message;
}

public sealed partial class Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel : IInt32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel, ICopied<Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel>
{
    Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel ICopied<Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel>.Copy() => this.Clone();
    Protocol.Models.Parameters.IInt32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel ICopied<Protocol.Models.Parameters.IInt32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel>.Copy() => ((ICopied<Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel>)this).Copy();
    public Int32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel(RepeatedField<System.String> @orderings)
    {
        orderings_ = @orderings;
    }

    IReadOnlyList<System.String> IInt32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel.Orderings => Orderings;
    Protocol.Models.Filters.IGoodFilterReadOnlyModel IInt32Int32IGoodFilterReadOnlyModelIEnumerableOfStringCommunicationContractModel.Filter { get => Filter; }
}