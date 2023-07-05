using LiteDB;

namespace LocalStore.LiteDb;

abstract class KeyValue<TId>
{
    public int Order { get; init; }
    public TId Id { get; init; }
}

class StoredItem<TId, TItem> : KeyValue<TId>
{
    public TItem? Value { get; init; }
}

class LiteDbOuterCollection<TId, TValue> : IOuterCollection<TId, TValue>
{
    readonly ILiteCollection<StoredItem<TId, TValue>> _liteCollection;
    readonly Func<TValue, TId> _getKey;

    public LiteDbOuterCollection(ILiteCollection<StoredItem<TId, TValue>> liteCollection, Func<TValue, TId> getKey)
    {
        _liteCollection = liteCollection;
        _getKey = getKey;
    }

    public async Task AddAsync(TValue item)
    {
        var valueToInsert = new StoredItem<TId, TValue>
        {
            Value = item,
            Id = _getKey(item),
            Order = _liteCollection.Count() - 1
        };

        _liteCollection.Insert(valueToInsert);
    }


    public async Task<ICollection<TValue>> ExtractAsync()
        => _liteCollection.Query().Select(kv => kv.Value).ToList();

    public async Task<TItem> GetAsync<TItem>(TId key)
    {
        var bsonKey = new BsonValue(key);

        var obj = _liteCollection.FindById(bsonKey);

        if (obj is TItem ready)
        {
            return ready;
        }

        return default;
    }

    public Task<TValue1> GetAtAsync<TValue1>(int index)
    {
        throw new NotImplementedException();
    }

    public Task InsertAsync(TValue value, int index)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveAsync(TId key)
    {
        var bsonKey = new BsonValue(key);
        _liteCollection.Delete(bsonKey);
    }
}

public class LiteDbLocalStore : ILocalStore, IDisposable
{
    const string defaultDbPath = "db.ldb";

    readonly LiteDatabase _db;

    ILiteCollection<StoredItem<string, object>> Items => _db.GetCollection<StoredItem<string, object>>("items");

    public LiteDbLocalStore(string dbName = defaultDbPath)
    {
        _db = new LiteDatabase($@"{AppContext.BaseDirectory}{dbName}");
    }

    public async Task DeleteAsync(string key)
        => Items.DeleteMany(i => i.Id == key);

    public async Task<TItem> GetAsync<TItem>(string key)
        => (Items.Find(i => i.Id == key).FirstOrDefault() as StoredItem<string, TItem> ?? new()).Value;

    public async Task PutAsync<TItem>(string key, TItem value)
    {
        var item = new StoredItem<string, object>
        {
            Id = key,
            Value = value,
            Order = 0
        };
        Items.Insert(item);
    }

    public IOuterCollection<TKey, TValue> CreateCollection<TKey, TValue>(string key, Func<TValue, TKey> getKey)
    {
        var lCollection = _db.GetCollection<StoredItem<TKey, TValue>>(key);
        lCollection.EntityMapper.Id.FieldName = nameof(StoredItem<TKey, TValue>.Id);

        var result = new LiteDbOuterCollection<TKey, TValue>(lCollection, getKey);

        return result;
    }


    public async Task<bool> DropCollectionAsync(string key)
        => _db.DropCollection(key);

    public async Task<IEnumerable<string>> GetKeysAsync()
        => Items.Query().Select(i => i.Id).ToArray();


    public void Dispose()
    {
        _db.TryDelete();
    }
}