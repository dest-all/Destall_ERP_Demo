using Business.Actions;
using Business.Tests.Setup;
using Data.Entities.Documents.Trade;
using Data.Samples;
using Protocol.Models;

namespace Business.Tests
{
    [TestFixture]
    public class ActionTests : ActionBase
    {
        [Test]
        public async Task SaveIncomingOrder_TabulalPartsMustMatch()
        {
            var incomingOrder = InitialData.Entities.OfType<IncomingOrder>().First();

            var incomingOrderModel = await Business.Actions.IncomingOrder.Get.CallAsync(incomingOrder.Id);

            incomingOrderModel = incomingOrderModel.Mutate(io => io.GoodsSold = io.GoodsSold.Take(1).ToList());

            var savingResult = await Business.Actions.IncomingOrder.SaveAsync(incomingOrderModel);

            Assert.AreEqual(incomingOrderModel.GoodsSold.Count, savingResult.GoodsSold.Count);
        }
    }
}
