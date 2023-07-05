using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.AdvancedDataStructures
{
    /// <summary>
    /// For Blazor virtualization. 
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class DynamicCollection<TItem> : ICollection<TItem>
    {
        private List<TItem> _itemsLoaded = new();
        private IEnumerator<TItem> _innerEnumerator;
        private readonly IEnumerable<TItem> _source;


        public DynamicCollection(IEnumerable<TItem> source)
        {
            _source = source;
            _innerEnumerator = new DynamicCollectionEnumerator(_itemsLoaded, source);
        }

        int ICollection<TItem>.Count => _itemsLoaded.Count;

        bool ICollection<TItem>.IsReadOnly => false;

        void ICollection<TItem>.Add(TItem item)
        {
            _itemsLoaded.Add(item);
        }

        void ICollection<TItem>.Clear()
        {
            _innerEnumerator.Reset();
            _itemsLoaded.Clear();
        }

        bool ICollection<TItem>.Contains(TItem item)
        {
            return _itemsLoaded.Contains(item);
        }

        void ICollection<TItem>.CopyTo(TItem[] array, int arrayIndex)
        {
            _itemsLoaded.CopyTo(array, arrayIndex);
        }

        IEnumerator<TItem> IEnumerable<TItem>.GetEnumerator()
        {
            return _innerEnumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _innerEnumerator;
        }

        bool ICollection<TItem>.Remove(TItem item)
        {
            return _itemsLoaded.Remove(item);
        }

        private class DynamicCollectionEnumerator : IEnumerator<TItem>
        {
            private readonly ICollection<TItem> _stackingCollection;
            private readonly IEnumerable<TItem> _source;

            private IEnumerator<TItem> _sourceEnumerator;

            public DynamicCollectionEnumerator(ICollection<TItem> stackingCollection, IEnumerable<TItem> source)
            {
                _stackingCollection = stackingCollection;
                _source = source;

                _sourceEnumerator = _source.GetEnumerator();
            }

            public TItem Current => Current;

            object IEnumerator.Current => _sourceEnumerator.Current;

            public void Dispose()
            {
                _sourceEnumerator?.Dispose();
            }

            public bool MoveNext()
            {
                if (_sourceEnumerator.MoveNext())
                {
                    _stackingCollection.Add(Current);
                    return true;
                }
                return false;
            }

            public void Reset()
            {
                _stackingCollection.Clear(); // ?
                _sourceEnumerator.Reset();
            }
        }
    }

    public class DynamicEnumerable<TItem> : IEnumerable<TItem>
    {

        protected readonly Func<uint, IEnumerable<TItem>> _getPortion;
        public DynamicEnumerable(Func<uint, IEnumerable<TItem>> getPortion)
        {
            _getPortion = getPortion;
        }

        protected DynamicEnumerator _enumerator;

        protected uint Count => _enumerator?.Count ?? 0;

        public IEnumerator<TItem> GetEnumerator()
        {
            _enumerator = new DynamicEnumerator(_getPortion);
            return _enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        protected class DynamicEnumerator : IEnumerator<TItem>
        {
            private readonly Func<uint, IEnumerable<TItem>> _getPortion;
            public ICollection<TItem> CurrentPortion = new TItem[0];
            private IEnumerator<TItem> _portionEnumerator;
            private uint _portionNumber = 0;
            private bool _justCreated = true;

            public uint Count { get; private set; } = 0;
            public DynamicEnumerator(Func<uint, IEnumerable<TItem>> getPortion, Action<IEnumerable<TItem>> onLoadMore = null)
            {
                _getPortion = getPortion ?? throw new ArgumentException("GetPortion function must not be null");
            }

            public TItem Current { get; set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                _portionEnumerator.Dispose();
            }

            public bool MoveNext()
            {
                if (!CurrentPortion.Any())
                {
                    if (_justCreated)
                    {
                        return LoadMore();
                    }
                    else
                    {
                        return false;
                    }
                }
                if (_portionEnumerator.MoveNext())
                {
                    Current = _portionEnumerator.Current;
                    Count++;
                    return true;
                }
                else
                {
                    LoadMore();
                    return MoveNext();
                }
            }

            protected bool LoadMore()
            {
                _portionNumber++;
                CurrentPortion = _getPortion(_portionNumber).ToArray();
                if (!CurrentPortion.Any())
                {
                    return false;
                }
                _portionEnumerator = CurrentPortion.GetEnumerator();
                return true;
            }

            public void Reset()
            {
                _portionNumber = 0;
                _justCreated = true;
                CurrentPortion = new TItem[0];
                Count = 0;
            }
        }


    }
}
