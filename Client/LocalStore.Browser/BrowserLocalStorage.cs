using Microsoft.JSInterop;
using Newtonsoft.Json;

namespace LocalStore.Browser
{
    static class Serialization
    {
        public static string ToJson(this object obj) => JsonConvert.SerializeObject(obj);
    }

    public class BrowserLocalStorage : ILocalStore
    {
        readonly IJSRuntime jsRuntime;

        static class CommandNames
        {
            const string objectAccess = "localStore.";

            public const string AddToStore = objectAccess + "AddToStore";
            public const string GetFromStore = objectAccess + "GetFromStore";
            public const string DeleteFromStore = objectAccess + "DeleteFromStore";
            public const string GetAllKeys = objectAccess + "GetAllKeys";
        }

        public BrowserLocalStorage(IJSRuntime jSRuntime)
        {
            this.jsRuntime = jSRuntime;
        }

        public async Task DeleteAsync(string key)
        {
            await jsRuntime.InvokeVoidAsync(CommandNames.DeleteFromStore, key);
        }

        public async Task<TItem> GetAsync<TItem>(string key)
        {
            try
            {
                var stringResult = await jsRuntime.InvokeAsync<string>(CommandNames.GetFromStore, key);
                var result = JsonConvert.DeserializeObject<TItem>(stringResult);
                return result;
            }
            catch (Exception ex)
            {
                return default;
            }
        }

        public async Task PutAsync<TItem>(string key, TItem value)
        {
            var valuesString = value.ToJson();
            await jsRuntime.InvokeVoidAsync(CommandNames.AddToStore, key, valuesString);
        }
        

        public IOuterCollection<TKey, TValue> CreateCollection<TKey, TValue>(string key, Func<TValue, TKey> getKey)
            => new BrowserLocalStorageCollection<TKey, TValue>(this, getKey, key);

        public async Task<bool> DropCollectionAsync(string key)
        {
            var allKeys = await GetKeysAsync();
            var relevantKeys = allKeys.Where(key => key.StartsWith(key));

            await Task.WhenAll(relevantKeys.Select(DeleteAsync).ToArray());

            return true;
        }

        public async Task<IEnumerable<string>> GetKeysAsync()
        {
            var result = await jsRuntime.InvokeAsync<string[]>(CommandNames.GetAllKeys, null);
            return result;
        }
    }
}