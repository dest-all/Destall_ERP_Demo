using Data.Repository;
using Protocol.Models.ReferrableEntities;
using System.Collections.Generic;
using System.Linq;

namespace Business.RegisterEntries;

public class CashRemainderEntry : CashRemainders
{
    public CurrencyReference Currency { get; set; }
}

public class CashRemainders
{
    public double Present { get; set; }
    public double Reserved { get; set; }
    public double Reservable { get; set; }

    public static CashRemainders operator -(CashRemainders first, CashRemainders second)
        => new CashRemainders
        {
            Present = first.Present - second.Present,
            Reservable = first.Reservable - second.Reservable,
            Reserved = first.Reserved - second.Reserved
        };

    public bool IsNegative => Present < 0 || Reservable < 0;
}


public static class CashRegistryRemaindersExtensions
{ 
    public static IQueryable<CashRemainderEntry> GetCashRemainders(this IDataRepository repo, IEnumerable<long> currencies)
    {
        var result = repo.CashEntries
            .Where(se => currencies.Contains(se.CurrencyId ?? 0))
            .GroupBy(se => se.CurrencyId)
            .Select(group => new CashRemainderEntry
            {
                Currency = new CurrencyReference
                {
                    Id = group.Key ?? 0,
                    Representation = group.First().Currency.Name
                },
                Present = group.Sum(g => g.Added),
                Reserved = group.Sum(g => g.Reserved),
            })
            .Select(gr => new CashRemainderEntry
            {
                Currency = gr.Currency,
                Present = gr.Present,
                Reserved = gr.Reserved,
                Reservable = gr.Present - gr.Reserved
            });

        return result;
    }
}
