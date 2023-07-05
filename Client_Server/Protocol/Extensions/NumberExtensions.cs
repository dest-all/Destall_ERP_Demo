using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protocol.Extensions
{
    public static class NumberExtensions
    {
        public static int SumUnchecked(this IEnumerable<int> ints)
        {
            var result = 0;
            foreach (var i in ints)
            {
                unchecked
                {
                    result += i;
                }
            }
            return result;
        }
    }
}
