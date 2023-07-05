using DestallMaterials.WheelProtection.Extensions.Enumerables;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace LocalStore.Browser;

class BrowserLocalStorageCollection<TKey, TItem> : IOuterCollection<TKey, TItem>
{
    readonly BrowserLocalStorage _browserLocalStorage;
    readonly Func<TItem, TKey> _getKey;

    List<TItem> _storedItems = new();
    readonly string _prefix;

    readonly Task _loadingTask;

    public BrowserLocalStorageCollection(BrowserLocalStorage browserLocalStorage, Func<TItem, TKey> getKey, string prefix)
    {
        _browserLocalStorage = browserLocalStorage;
        _getKey = getKey;
        _prefix = prefix;

        _loadingTask = LoadFromStorageAsync();
    }

    string ComposeStorageKey(TKey key) => $"--{_prefix}--{JsonSerializer.Serialize(key)}";
    string ComposeStorageKey(TItem item) => $"--{_prefix}--{JsonSerializer.Serialize(_getKey(item))}";

    public async Task AddAsync(TItem item)
    {
        await _loadingTask;

        var key = ComposeStorageKey(item);

        if (_storedItems.Any(i => _getKey(i).Equals(key)))
        {
            throw new InvalidOperationException($"Item with key {key} already added.");
        }

        _storedItems.Add(item);
        await _browserLocalStorage.PutAsync(key, new StoredItem<TItem>
        {
            Order = _storedItems.Count - 1,
            Value = item
        });
    }

    public async Task<ICollection<TItem>> ExtractAsync()
    {
        await _loadingTask;
        return _storedItems.ToList();
    }

    public async Task<TValue> GetAsync<TValue>(TKey key)
    {
        await _loadingTask;

        var result = _storedItems.FirstOrDefault(si => _getKey(si).Equals(key));

        if (result is TValue ready)
        {
            return ready;
        }
        throw new KeyNotFoundException();
    }

    public async Task RemoveAsync(TKey key)
    {
        await _loadingTask;

        var keyJson = ComposeStorageKey(key);
        await _browserLocalStorage.DeleteAsync(keyJson);
    }


    public async Task<TValue> GetAtAsync<TValue>(int index)
    {
        await _loadingTask;

        if (_storedItems[index] is TValue result)
        {
            return result;
        }

        throw new InvalidCastException();
    }

    public async Task PutAsync(TItem item)
    {
        var key = _getKey(item);
        var index = _storedItems.MetAt(i => _getKey(i).Equals(key));

        if (index == -1)
        {
            index = _storedItems.Count;
        }

        _storedItems[index] = item;
        var keyToSubstitute = ComposeStorageKey(key);

        await _browserLocalStorage.PutAsync(keyToSubstitute, new StoredItem<TItem>
        {
            Order = index,
            Value = item
        });

    }

    public async Task RemoveAtAsync(int index)
    {
        var key = ComposeStorageKey(_storedItems[index]);
        await _browserLocalStorage.DeleteAsync(key);
    }


    async Task LoadFromStorageAsync()
    {
        var key = _prefix;
        var keys = await _browserLocalStorage.GetKeysAsync();

        var relevantKeys = keys.Where(k => k.StartsWith($"--{_prefix}--"));

        var values = await relevantKeys.Select(async i =>
        {
            var itemUnknown = await _browserLocalStorage.GetAsync<StoredItem<JObject>>(i);

            if (itemUnknown is null)
            {
                await _browserLocalStorage.DeleteAsync(i);
                return null;
            }

            var type = typeof(TItem).Assembly.GetType(itemUnknown.Type);
            var item = (TItem)itemUnknown.Value.ToObject(type);
            return new
            {
                item,
                itemUnknown.Order
            };
        }).ToArray().OrderByAsync(i => i?.Order);

        var result = values.Where(v => v is not null).Select(v => v.item);

        _storedItems = result.ToList();
    }
}