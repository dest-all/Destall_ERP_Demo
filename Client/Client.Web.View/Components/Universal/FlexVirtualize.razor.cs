using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Components.Universal
{
    public partial class FlexVirtualize<T> : ComponentBase, IDisposable
    {
        bool _isDisposed;
        public void Dispose()
        {
            _isDisposed = true;
        }
    }
}
