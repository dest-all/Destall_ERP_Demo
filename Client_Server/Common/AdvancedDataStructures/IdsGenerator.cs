using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.AdvancedDataStructures
{
    public class IdsGenerator
    {
        readonly DateTime _from;
        long _last;
        readonly object _locker = new object();
        public IdsGenerator(DateTime from)
        {
            _from = from;
        }

        public IdsGenerator()
        {
            _from = DateTime.UtcNow;
        }

        public long Generate()
        {
            var result = (DateTime.UtcNow - _from).Ticks;

            lock (_locker)
            {
                while (result <= _last)
                {
                    result = _last + 1;
                }
                _last = result;
            }
            return result;
        }
    }
}
