using DestallMaterials.WheelProtection.Extensions.Enumerables;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Functions
{
    public static class DynamicLoading
    {

        /// <summary>
        /// Algorythm to load exact items range,based on page loading function (pages numbering starts from 1).
        /// </summary>
        /// <typeparam name="TItem"></typeparam>
        /// <param name="startIndex"></param>
        /// <param name="countRequested"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageLoadingFunction">Function that calculates pages content from page number, which starts from 1.</param>
        /// <returns></returns>
        public static async Task<List<TItem>> LoadForVirtualizationInPagesAsync<TItem>(int startIndex, int countRequested, int pageSize, Func<int, Task<IEnumerable<TItem>>> pageLoadingFunction)
        {
            var pagesNeeded = (int)Math.Ceiling((decimal)(countRequested + startIndex) / pageSize);
            var result = new List<TItem>();
            int startPageNumber = (int)(startIndex / pageSize + 0.5) + 1;
            var offset = startIndex % pageSize;

            var loadingTasks = new Task<IEnumerable<TItem>>[pagesNeeded];
            for (int p = 0; p < pagesNeeded; p++)
            {
                var pageNumberForTask = p;
                var itemsTask = pageLoadingFunction(startPageNumber + pageNumberForTask);
                loadingTasks[p] = itemsTask;
            }
            for (uint p = 0; p < pagesNeeded; p++)
            {
                var itemsTask = loadingTasks[p];
                var items = (await itemsTask).EnsureMaterialized();

                if (p == 0)
                {
                    result.AddRange(items.Skip(offset));
                }
                else if (p == pagesNeeded - 1)
                {
                    result.AddRange(items.Take(countRequested - result.Count));
                }
                else
                {
                    result.AddRange(items);
                }
                if (items.Count < pageSize)
                {
                    break;
                }
            }

            return result;
        }
    }
}
