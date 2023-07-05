using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data.Extensions
{
    public static class ExpressionsFormingExtensions
    {
        public static Expression<Func<TReferrable, bool>> Express<TReferrable>(this IQueryable<OwnedString> possiblePatterns, string patternToSeek)
            where TReferrable : ReferrableEntity
            => referrable => possiblePatterns.Any(pp => pp.Id == referrable.Id && pp.Value == patternToSeek);
    }
}
