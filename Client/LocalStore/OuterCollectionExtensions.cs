using System.Collections.Generic;
using System.Threading.Tasks;


namespace LocalStore
{
    public static class OuterCollectionExtensions
    {
        public static async Task AddAsync<TKey, TItem>(this IOuterCollection<TKey, TItem> source, IEnumerable<TItem> items)
        {
            foreach (var item in items)
            {
                await source.AddAsync(item);
            }
        }
    }
}
