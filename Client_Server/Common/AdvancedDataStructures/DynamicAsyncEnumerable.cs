using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Common.AdvancedDataStructures
{
    public class DynamicAsyncEnumerable<TItem> : IAsyncEnumerable<TItem>
    {

        private readonly Func<uint, Task<IEnumerable<TItem>>> _getPortionAsync;
        public DynamicAsyncEnumerable(Func<uint, Task<IEnumerable<TItem>>> getPortion)
        {
            _getPortionAsync = getPortion;
        }

        public ushort BatchSize;

        public IAsyncEnumerator<TItem> GetAsyncEnumerator(CancellationToken ct) => new DynamicAsyncEnumerator(_getPortionAsync);

        private class DynamicAsyncEnumerator : IAsyncEnumerator<TItem>
        {
            private readonly Func<uint, Task<IEnumerable<TItem>>> _getPortionAsync;
            private IEnumerable<TItem> _currentPortion = new TItem[0];
            private IEnumerator<TItem> _portionEnumerator;
            private uint _portionNumber = 0;
            private bool _justCreated = true;

            public DynamicAsyncEnumerator(Func<uint, Task<IEnumerable<TItem>>> getPortion)
            {
                _getPortionAsync = getPortion ?? throw new ArgumentException("GetPortion function must not be null");
            }

            public TItem Current { get; set; }

            public async ValueTask<bool> MoveNextAsync()
            {
                if (!_currentPortion.Any())
                {
                    if (_justCreated)
                    {
                        return await LoadMoreAsync();
                    }
                    else
                    {
                        return false;
                    }
                }
                if (_portionEnumerator.MoveNext())
                {
                    Current = _portionEnumerator.Current;
                    return true;
                }
                else
                {
                    LoadMoreAsync();
                    return await MoveNextAsync();
                }
            }

            private async Task<bool> LoadMoreAsync()
            {
                _portionNumber++;
                _currentPortion = await _getPortionAsync(_portionNumber);
                if (!_currentPortion.Any())
                {
                    return false;
                }
                _portionEnumerator = _currentPortion.GetEnumerator();
                return true;
            }

            public void Reset()
            {
                _portionNumber = 0;
                _justCreated = true;
                _currentPortion = new TItem[0];
            }


            public async ValueTask DisposeAsync()
            {
                _portionEnumerator.Dispose();
            }
        }


    }
}
