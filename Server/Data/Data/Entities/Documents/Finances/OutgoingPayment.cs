using Data.Entities.DataHolders.Actors;
using Data.Entities.Documents.Trade;

namespace Data.Entities.Documents.Finances;

public partial class OutgoingPayment : FinancialDocument
{
    public OutgoingOrder Order { get; set; }

    public Supplier Receiver { get; set; }
}
