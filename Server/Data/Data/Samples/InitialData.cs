using Data.Entities;
using Data.Entities.DataHolders;
using Data.Entities.DataHolders.Actors;
using Data.Entities.Documents.Finances;
using Data.Entities.Documents.Trade;
using DestallMaterials.WheelProtection.Extensions.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Data.Samples;

public static class InitialData
{
    public static IEnumerable<Entity> Entities
    {
        get
        {
            var goods = new Good[]
            {
                new Good
                {
                    Name = "Paper list",
                    Id = 450
                },
                new Good
                {
                    Id = 123,
                    Name = "Printer"
                }
            };

            var employees = new Employee[]
            {
                new()
                {
                    FirstName = "Michael",
                    LastName = "Scott",
                    Id = 180
                },
                new()
                {
                    FirstName = "James",
                    LastName = "Halpert",
                    Id = 182
                }
                    };

            var suppliers = new Supplier[]
            {
                new Supplier
                {
                    Name = "D&M Facility",
                    Id = 890
                }
            };

            var customers = new Customer[]
            {
                new Customer
                {
                    Id = 15,
                    Name = "Finger Lakes CO"
                }
            };

            var currencies = new Currency
            {
                Id = 333,
                Name = "USD",
                Primary = true
            }.ToArrayOfOne();


            long accountableId = employees.First().Id;
            long currencyId = currencies.First().Id;


            var deposits = new Deposit
            {
                CurrencyId = currencyId,
                Number = "90009",
                Sum = 5000,
                AccountableId = accountableId,
                Id = 340
            }.ToArrayOfOne();

            var outgoingOrders = new OutgoingOrder[]
            {
                new OutgoingOrder
                {
                    Id = 2347,
                    Number = "45",
                    SupplierId = suppliers[0].Id,
                    GoodsBought = new List<OutgoingOrderLine>
                    {
                        new()
                        {
                            GoodId = goods[0].Id,
                            Price = 0.5,
                            Quantity = 700
                        },
                        new()
                        {
                            GoodId = goods[1].Id,
                            Price = 180,
                            Quantity = 2
                        }
                    },
                    AccountableId = accountableId,
                    CurrencyId = currencyId
                }
            };

            var incomingOrders = new IncomingOrder[]
            {
                new IncomingOrder
                {
                    CustomerId = customers[0].Id,
                    Number = "101",
                    Id = 15,
                    GoodsSold = new List<IncomingOrderLine>
                    {
                        new IncomingOrderLine
                        {
                            GoodId = goods[0].Id,
                            Price = 2,
                            Quantity = 500
                        },
                        new IncomingOrderLine
                        {
                            GoodId = goods[1].Id,
                            Price = 300,
                            Quantity = 1
                        }
                    },
                    AccountableId = accountableId,
                    CurrencyId= currencyId
                }
            };

            var items = goods.Cast<Entity>()
                             .Concat(employees)
                             .Concat(outgoingOrders)
                             .Concat(customers)
                             .Concat(suppliers)
                             .Concat(outgoingOrders)
                             .Concat(incomingOrders)
                             .Concat(currencies)
                             .Concat(deposits);

            return items;
        }
    }
}
