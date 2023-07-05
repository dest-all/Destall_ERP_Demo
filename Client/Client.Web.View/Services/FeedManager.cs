using Client.Communication;
using Client.Web.View.Components.Feed;
using Client.Web.View.Extensions;
using DestallMaterials.Blazor.Services;
using DestallMaterials.Blazor.Services.UI;
using DestallMaterials.WheelProtection.Extensions.Enumerables;
using DestallMaterials.WheelProtection.Extensions.Objects;
using DestallMaterials.WheelProtection.Extensions.Strings;
using DestallMaterials.WheelProtection.Extensions.Tasks;
using LocalStore;
using Protocol.Models;

namespace Client.Web.View.Services
{
    public interface IFeedManager
    {
        IEnumerable<IReferrableModel> Items { get; }
        string FeedId { get; }

        DisposableCallback SubscribeForFeedCountChange(Func<Task> onChange);

        Task FireItemChangedAsync(IReferrableModel item);

        Task RemoveAsync(IReferrableModel item);

        Task AddItemByReferenceAsync(IReference reference);

        Task AddToFeedAsync(IEnumerable<IReferrableModel> items);

        bool DrawnIn { get; set; }

        IDisposable SubscribeForDrawing(Action<bool> callback);

        IReadOnlyList<FeedItem> FeedItems { get; set; }

        int FeedWidthPx { get; }
    }

    public static class FeedManagerExtensions
    {
        public static Task AddOneToFeedAsync(this IFeedManager feedManager, IReference item)
            => feedManager.AddItemByReferenceAsync(item);

        public static Task AddOneToFeedAsync(this IFeedManager feedManager, IReferrableModel item)
            => feedManager.AddToFeedAsync(item.Yield());

    }

    public class FeedManager : IFeedManager
    {
        public const string FeedStoreKey = "Feed";

        bool _drawnIn;
        public bool DrawnIn
        {
            get => _drawnIn;
            set
            {
                bool shouldFire = _drawnIn != value;
                _drawnIn = value;
                if (shouldFire)
                {
                    FireDrawn(value);
                }

            }
        }

        private void FireDrawn(bool value)
        {
            foreach (var callback in _drawingCallbacks.ToArray())
            {
                callback(value);
            }
        }

        readonly List<Action<bool>> _drawingCallbacks = new();

        public string FeedId { get; } = Guid.NewGuid().ToString();


        readonly IBusinessServerActionInvokersNet _client;
        readonly IUiManipulator _uiManipulator;

        IOuterCollection<int, IReferrableModel> _inStorage;

        public FeedManager(IBusinessServerActionInvokersNet client, ILocalStore localStore, IUiManipulator uiManipulator)
        {
            _client = client;
            _uiManipulator = uiManipulator;
            _inStorage = localStore.CreateCollection<int, IReferrableModel>("feed", v => v.Reference.ComputeChecksum());

            _inStorage.ExtractAsync().Then(async items =>
            {
                _items.AddRange(items);
                await FireCountChangedAsync();
            }).Forget();
        }

        readonly List<Func<Task>> _itemsCountChangedSubscriptions = new();

        public IDisposable SubscribeForDrawing(Action<bool> callback)
        {
            if (_drawingCallbacks.Contains(callback))
            {
                throw new InvalidOperationException();
            }
            var result = new DisposableCallback((th) => _drawingCallbacks.Remove(callback));
            _drawingCallbacks.Add(callback);
            return result;
        }


        public async Task AddToFeedAsync(IEnumerable<IReferrableModel> items)
        {
            if (_items.Any())
            {
                await NavigateToAsync(_items[^1].Reference);
            }
            _items.AddRange(items);
            await _inStorage.AddAsync(items);

            await FireCountChangedAsync();
            DrawnIn = true;
        }

        public DisposableCallback SubscribeForFeedCountChange(Func<Task> onChange)
        {
            if (_itemsCountChangedSubscriptions.Contains(onChange))
            {
                throw new InvalidOperationException();
            }
            _itemsCountChangedSubscriptions.Add(onChange);
            return new(_ => _itemsCountChangedSubscriptions.Remove(onChange));
        }

        readonly List<IReferrableModel> _items = new();

        public IEnumerable<IReferrableModel> Items => _items;

        public IReadOnlyList<FeedItem> FeedItems { get; set; } = new List<FeedItem>();

        public int FeedWidthPx { get; set; }

        async Task FireCountChangedAsync()
        {
            foreach (var callback in _itemsCountChangedSubscriptions.ToArray())
            {
                await callback();
            }
        }

        public async Task RemoveAsync(IReferrableModel item)
        {
            if (_items.Remove(item))
            {
                await _inStorage.RemoveAsync(item.Reference.ComputeChecksum());
                await FireCountChangedAsync();

                if (_items.IsEmpty())
                {
                    DrawnIn = false;
                }
            }
        }

        public async Task FireItemChangedAsync(IReferrableModel item)
        {
            var index = _items.IndexOf(item);
            if (index != -1)
            {
                await _inStorage.PutAsync(item);
            }
        }

        public async Task AddItemByReferenceAsync(IReference reference)
        {
            if (_items.Any(i => i.Reference.Equals(reference)))
            {
                await NavigateToAsync(reference);
                return;
            }
            var item = await _client.GetByReferenceAsync(reference);
            await AddToFeedAsync(item.Yield());
        }

        async Task NavigateToAsync(IReference item)
        {
            string itemId = item.ComputeChecksum().ToString();

            var cardComponent = FeedItems.FirstOrDefault(i => i.Item.Reference is not null && i.Item.Reference.Equals(item));

            cardComponent.SetCollapsed(false);

            await Task.Delay(50).Then(async () => await _uiManipulator.ScrollToFit_Y(itemId, FeedId));
        }

        readonly List<Action> _widthChangeCallbacks = new();
    }

}
