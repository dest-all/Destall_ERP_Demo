using Data.Entities.DataHolders.Actors;
using Data.Entities.Documents.Trade;

namespace Data.Entities.Documents.Finances;

public partial class IncomingPayment : FinancialDocument
{
    public IncomingOrder Order { get; set; }

    public Customer Payer { get; set; }
}
