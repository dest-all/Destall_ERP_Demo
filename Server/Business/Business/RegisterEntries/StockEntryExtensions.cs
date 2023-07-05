using Business.Selectors;
using Data.Repository;
using Protocol.Models.DataHolders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.RegisterEntries;

public static class StockEntryExtensions
{
    public class GoodRemainderStockEntry : GoodRemainders
    { 
        public GoodReference Good { get; set; }
    }

    public class GoodRemainders
    {
        public double Present { get; set; }
        public double Reserved { get; set; }
        public double Reservable { get; set; }

        public static GoodRemainders operator -(GoodRemainders first, GoodRemainders second)
            => new GoodRemainders
            {
                Present = first.Present - second.Present,
                Reservable = first.Reservable - second.Reservable,
                Reserved = first.Reserved - second.Reserved
            };

        public bool IsNegative => Present < 0 || Reservable < 0;
    }

    public static IQueryable<GoodRemainderStockEntry> GetGoodRemainders(this IDataRepository repo, IEnumerable<long> goods)
    {
        var result = repo.StockEntries
            .Where(se => goods.Contains(se.GoodId ?? 0))
            .GroupBy(se => se.GoodId)
            .Select(group => new GoodRemainderStockEntry
            { 
                Good = new GoodReference
                { 
                    Id = group.Key ?? 0,
                    Representation = group.First().Good.Name
                },
                Present = group.Sum(g => g.Added),
                Reserved = group.Sum(g=>g.Reserved),
            })
            .Select(gr => new GoodRemainderStockEntry
            {
                Good = gr.Good,
                Present = gr.Present,
                Reserved = gr.Reserved,
                Reservable = gr.Present - gr.Reserved
            });

        return result;
    }

}
