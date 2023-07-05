using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AdvancedDataStructures
{
    public class DisposableCollection : IDisposable
    {
        ICollection<IDisposable> Items = new List<IDisposable>();
        public void Add(params IDisposable[] items)
        {
            foreach (var item in items)
            {
                if (Items.Contains(item))
                {
                    return;
                }
                Items.Add(item);
            }
        }

        public void Dispose()
        {
            foreach (var item in Items)
            {
                item.Dispose();
            }
        }

        public bool Remove(IDisposable item)
        {
            return Items.Remove(item);
        }
    }
}
