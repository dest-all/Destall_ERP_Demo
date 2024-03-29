// Autogenerated. Will be overwritten on build. Remove this comment to cancel overwriting.

using Protocol.Models.Documents;
using System.Linq;
using Google.Protobuf.Collections;
using System.Collections.Generic;
using System.Collections;
using Protocol.Grpc.ProtoModels.Conversion;
using DestallMaterials.WheelProtection.Copying;

namespace Protocol.Grpc.ProtoModels;
public sealed partial class IncomingOrderModelList : IReadOnlyList<Protocol.Models.Documents.IIncomingOrderReadOnlyModel>
{
    public IncomingOrderModelList(RepeatedField<IncomingOrderModel> itemsParameter)
    {
        this.message_ = itemsParameter;
        OnConstruction();
    }

    public Protocol.Models.Documents.IIncomingOrderReadOnlyModel this[int index] => message_[index];
    public int Count => message_.Count;
    public IEnumerator<Protocol.Models.Documents.IIncomingOrderReadOnlyModel> GetEnumerator() => message_.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => message_.GetEnumerator();
    public static implicit operator IncomingOrderModelList(RepeatedField<IncomingOrderModel> from)
    {
        var result = new IncomingOrderModelList(from);
        return result;
    }

    public static implicit operator RepeatedField<IncomingOrderModel>(IncomingOrderModelList from) => from.Message;
}

public sealed partial class IncomingOrderModel : IIncomingOrderReadOnlyModel, ICopied<IncomingOrderModel>
{
    IncomingOrderModel ICopied<IncomingOrderModel>.Copy() => this.Clone();
    Protocol.Models.Documents.IIncomingOrderReadOnlyModel ICopied<Protocol.Models.Documents.IIncomingOrderReadOnlyModel>.Copy() => ((ICopied<IncomingOrderModel>)this).Copy();
    public IncomingOrderModel(RepeatedField<IncomingOrderLineModel> @goodsSold)
    {
        goodsSold_ = @goodsSold;
    }

    System.UInt16 IIncomingOrderReadOnlyModel.Status { get => (ushort)Status; }

    Protocol.Models.Documents.IIncomingOrderReadOnlyReference IIncomingOrderReadOnlyModel.Reference { get => Reference; }

    IReadOnlyList<Protocol.Models.GoodTransactionLines.IIncomingOrderLineReadOnlyModel> IIncomingOrderReadOnlyModel.GoodsSold => GoodsSold;
    Protocol.Models.Counterparties.ICustomerReadOnlyReference IIncomingOrderReadOnlyModel.Customer { get => Customer; }

    Protocol.Models.ReferrableEntities.ICurrencyReadOnlyReference IIncomingOrderReadOnlyModel.Currency { get => Currency; }

    Protocol.Models.People.IEmployeeReadOnlyReference IIncomingOrderReadOnlyModel.Accountable { get => Accountable; }
}