using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;


namespace LocalStore
{
    public interface IOuterCollection<TKey, TItem>
    {
        Task AddAsync(TItem item);
        Task RemoveAsync(TKey key);
        Task<TValue> GetAsync<TValue>(TKey key);
        Task<ICollection<TItem>> ExtractAsync();

        Task<TValue> GetAtAsync<TValue>(int index);
        
        /// <summary>
        /// Pasted element substitutes element at the designated position.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        Task PutAsync(TItem item);

        Task RemoveAtAsync(int index); 
    }

    public interface ILocalStore
    {
        Task PutAsync<TItem>(string key, TItem value);
        Task<TItem> GetAsync<TItem>(string key);
        Task DeleteAsync(string key);
        IOuterCollection<TKey, TValue> CreateCollection<TKey, TValue>(string key, Func<TValue, TKey> getKey);
        Task<bool> DropCollectionAsync(string key);
        Task<IEnumerable<string>> GetKeysAsync();
    }
}
