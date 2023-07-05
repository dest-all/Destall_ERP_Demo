using Business.ModelsComposition;
using Business.Tests.Setup;
using Data.Entities;
using Data.Entities.DataHolders;
using Data.Entities.DataHolders.Actors;
using Data.Samples;
using DestallMaterials.WheelProtection.Extensions.Enumerables;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using Microsoft.EntityFrameworkCore;
using Protocol.Models;
using Protocol.Models.DataHolders;
using Protocol.Models.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Tests
{
    public class StatusMovers : ActionBase
    {
        [Test]
        public async Task IncomingOrderStatusMover_NewlyCreatedOrder_ShouldMoveSuccessfully()
        {
            var doc = new IncomingOrderModel
            {
                Number = "1245",
                Reference = null
            };

            var result = await Business.Actions.IncomingOrder.ChangeStatus.CallAsync(doc, 1);

            Assert.AreEqual(doc.Number, result.Number);
            Assert.AreEqual(1, result.Status);

        }

        [Test]
        public async Task IncomingOrderStatusMover_ExistingOrder_ShouldMoveSuccessfully()
        {

            var goodModel1 = await Business.Actions.Good.Save.CallAsync(new GoodModel
            {
                Name = "Test good 1"
            });
            var goodRef1 = goodModel1.Reference.ToMutable();

            var goodModel2 = await Business.Actions.Good.Save.CallAsync(new GoodModel
            {
                Name = "Test good 2"
            });
            var goodRef2 = goodModel2.Reference.ToMutable();

            const string number = "1245";

            var accountable = InitialData.Entities.OfType<Employee>().First().ComposeReference();


            var outgoingOrder = new OutgoingOrderModel
            {
                Number = "132",
                Currency = InitialData.Entities.OfType<Currency>().First().ComposeReference(),
                Supplier = InitialData.Entities.OfType<Supplier>().First().ComposeReference(),
                GoodsBought = new List<Protocol.Models.GoodTransactionLines.OutgoingOrderLineModel>
                {
                    new Protocol.Models.GoodTransactionLines.OutgoingOrderLineModel
                    {
                        Good = goodRef1,
                         Quantity = 10,
                         Price = 120
                    },
                    new Protocol.Models.GoodTransactionLines.OutgoingOrderLineModel
                    {
                        Good = goodRef2,
                        Quantity = 45,
                        Price = 3
                    }
                },
                Accountable = accountable
            };



            var incomingOrder = new IncomingOrderModel
            {
                Number = number,
                Reference = null,
                Status = 1,
                GoodsSold = new List<Protocol.Models.GoodTransactionLines.IncomingOrderLineModel>
                {
                    new Protocol.Models.GoodTransactionLines.IncomingOrderLineModel
                    {
                        Good = goodRef1,
                         Quantity = 10,
                         Price = 120
                    },
                    new Protocol.Models.GoodTransactionLines.IncomingOrderLineModel
                    {
                        Good = goodRef2,
                        Quantity = 45,
                        Price = 3
                    }
                },
                Accountable = accountable,
                Customer = InitialData.Entities.OfType<Customer>().First().ComposeReference(),
                Currency = InitialData.Entities.OfType<Currency>().First().ComposeReference()
            } as IIncomingOrderReadOnlyModel;

            var outgoingOrderModel = await Business.Actions.OutgoingOrder.ChangeStatus.CallAsync(outgoingOrder, 1)
                .Then(od => Business.Actions.OutgoingOrder.ChangeStatus.CallAsync(od, 2))
                .Then(od => Business.Actions.OutgoingOrder.ChangeStatus.CallAsync(od, 3));

            incomingOrder = await Business.Actions.IncomingOrder.Save.CallAsync(incomingOrder)
                .Then(d => Business.Actions.IncomingOrder.ChangeStatus.CallAsync(d, 2))
                .Then(d => Business.Actions.IncomingOrder.ChangeStatus.CallAsync(d, 3))
                .Then(d => Business.Actions.IncomingOrder.ChangeStatus.CallAsync(d, 4));

            using var repo = await GetRepositoryAsync();

            var receivableAccountEntries = await repo.AccountEntries.Where(ae => ae.ActorId == incomingOrder.Reference.Id).ToArrayAsync();
            var paymentEntries = await repo.AccountEntries.Where(ae => ae.ActorId == outgoingOrderModel.Reference.Id).ToArrayAsync();

            Assert.AreEqual(incomingOrder.Number, number);
            Assert.AreEqual(4, incomingOrder.Status);
            Assert.AreEqual(2, incomingOrder.GoodsSold.Count);

            Assert.AreEqual(1, paymentEntries.Length);
            Assert.AreEqual(1, receivableAccountEntries.Length);

            Assert.AreEqual(incomingOrder.GoodsSold.Sum(gs => gs.Price * gs.Quantity), receivableAccountEntries.First().SumReceivable);
            Assert.AreEqual(outgoingOrder.GoodsBought.Sum(gs => gs.Price * gs.Quantity), paymentEntries.First().SumPayable);
        }
    }
}
