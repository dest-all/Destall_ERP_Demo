using Business.ActionPoints;
using Protocol.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Protocol.Models.DataHolders;

namespace Business.Administration
{
    public class Session : IDisposable
    {
        public string Key { get; init; }
        public long UserId { get; init; }
        public DateTime LastTimeActive { get; internal set; }

        void IDisposable.Dispose()
            => Close();

        public Session(Action close)
        {
            Close = close ?? throw new ArgumentNullException();
        }

        Action Close { get; }
    }
}
