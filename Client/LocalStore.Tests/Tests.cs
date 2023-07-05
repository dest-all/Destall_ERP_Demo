using LocalStore.LiteDb;

namespace LocalStore.Tests
{
    public class Tests : IDisposable
    {

        LiteDbLocalStore _localStore = new LiteDbLocalStore();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task PutAndGetSimpleValues_SameValues()
        {
            var str = "str";
            var number = 55;

            await _localStore.PutAsync(nameof(str), str);
            await _localStore.PutAsync(nameof(number), number);

            var storedKeys = (await _localStore.GetKeysAsync()).ToArray();

            Assert.Contains(nameof(str), storedKeys);
            Assert.Contains(nameof(number), storedKeys);

            var values = await Task.WhenAll(storedKeys.Select(_localStore.GetAsync<object>));

            Assert.Contains(number, values);
            Assert.Contains(str, values);
        }

        [Test]
        public async Task PutAndGetCollections_SameValues()
        {
            var str = "str";
            var number = 55;

            var collection = _localStore.CreateCollection<object, object>("test", obj => obj.GetHashCode());

            var strHash = str.GetHashCode();
            var numberHash = number.GetHashCode();

            await collection.AddAsync(str);
            await collection.AddAsync(number);

            var strInCollection = await collection.GetAsync<object>(strHash);
            var numberInCollection = await collection.GetAsync<object>(numberHash);

            Assert.AreEqual(str, strInCollection);
            Assert.AreEqual(number, numberInCollection);
        }


        public void Dispose() => _localStore.Dispose();
    }
}