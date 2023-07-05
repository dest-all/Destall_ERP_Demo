using Data.Entities.Documents.Finances;
using Data.Entities.Documents.Trade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Messages.CustomCommunicationContracts.Reports;

public class UnsettledOrder : Transportable
{
    public OutgoingOrder OutgoingOrder { get; set; }
    public IncomingOrder IncomingOrder { get; set; }

    public double ReceivedPayment { get; set; }
    public double TotalValue { get; set; }
    public double LeftToReceive { get; set; }

    public IReadOnlyList<OutgoingPayment> OutgoingPayments { get; set; }
    public IReadOnlyList<IncomingPayment> IncomingPayments { get; set; }
}
