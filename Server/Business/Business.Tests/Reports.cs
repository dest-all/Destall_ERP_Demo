using Data.Entities.Documents.Trade;
using Data.Samples;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tests
{
    public class Reports : ActionTests
    {
        [Test]
        public async Task GetUnsettledOrders()
        {
            var incomingsReportActionPoint = Business.Reports.Finances.GetUnsettledIncomingOrders;

            var outgoingOrderId = InitialData.Entities.OfType<OutgoingOrder>().First().Id;

            var outgoingOrderToMove = await Business.Actions.OutgoingOrder.Get.CallAsync(outgoingOrderId);

            outgoingOrderToMove = await Business.Actions.OutgoingOrder.ChangeStatus.CallAsync(outgoingOrderToMove, 1)
                .Then(oo => Business.Actions.OutgoingOrder.ChangeStatus.CallAsync(oo, 2))
                .Then(oo => Business.Actions.OutgoingOrder.ChangeStatus.CallAsync(oo, 3));

            var incomingOrderId = InitialData.Entities.OfType<IncomingOrder>().First().Id;

            var incomingOrderToMove = await Business.Actions.IncomingOrder.Get.CallAsync(incomingOrderId);

            incomingOrderToMove = await Business.Actions.IncomingOrder.ChangeStatus.CallAsync(incomingOrderToMove, 1)
                .Then(oo => Business.Actions.IncomingOrder.ChangeStatus.CallAsync(oo, 2))
                .Then(oo => Business.Actions.IncomingOrder.ChangeStatus.CallAsync(oo, 3));


            var incReport = await incomingsReportActionPoint.CallAsync();

            var outReport = await Business.Reports.Finances.

        }
    }
}
