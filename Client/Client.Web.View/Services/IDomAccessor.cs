using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MudBlazor;
using Newtonsoft.Json;

namespace Client.Web.View.Services
{
    public struct Size
    {
        public required double Height { get; init; }
        public required double Width { get; init; }

        public bool IsZero => Height == 0 && Width == 0;
    }

    public class JsDomAccessor : IDomAccessor
    {
        readonly IJSRuntime _js;

        public JsDomAccessor(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<int> CountItemsInsideElementAsync(string containerId)
        {
            const string funcName = "view.countItemsInsideElement";
            int result = await _js.InvokeAsync<int>(funcName, containerId);
            return result;
        }


        public async Task<bool> SubscribeForResizeAsync(string containerId, Func<Size, Task> callback)
        {
            const string function = "view.subscribeForResize";

            ToJsCallback<double, double> jsCallback = new(async (height, width) => await callback(new Size
            {
                Height = height,
                Width = width
            }));
            return await _js.InvokeAsync<bool>(function, containerId, DotNetObjectReference.Create(jsCallback));
        }

        public async Task<Size> GetContainerSizeAsync(string containerId)
        {
            const string function = "view.getItemSize";
            var resultArray = await _js.InvokeAsync<double[]>(function, containerId);
            return new Size
            {
                Height = resultArray[0],
                Width = resultArray[1]
            };
        }

        public async Task<string> GetWidestItemAsync(string containerId)
        {
            const string function = "view.getWidestItemInContainer";
            var result = await _js.InvokeAsync<string>(function, containerId);
            return result;
        }

    }

    public class ToJsCallback<TParam1, TParam2>
    {
        readonly Func<TParam1, TParam2, Task> _callback;
        public ToJsCallback(Func<TParam1, TParam2, Task> callback)
        {
            _callback = callback;
        }

        [JSInvokable]
        public async Task Call(TParam1 param1, TParam2 param2)
        {
            await _callback(param1, param2);
        }
    }

    public interface IDomAccessor
    {
        Task<int> CountItemsInsideElementAsync(string containerId);
        Task<Size> GetContainerSizeAsync(string containerId);
        Task<string> GetWidestItemAsync(string containerId);
        Task<bool> SubscribeForResizeAsync(string containerId, Func<Size, Task> callback);
    }
}
