using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Components.Extensions;

public static class SortingExtensions
{
    public static SortDirection Orders(this IEnumerable<string> orderings, string orderingProperty)
    {
        orderingProperty = orderingProperty.ToLower();
        foreach (var ordering in orderings)
        {
            string orderingLower = ordering.ToLower();
            if (orderingLower == $"-{orderingProperty}")
            {
                return SortDirection.Descending;
            }
            else if (orderingLower == orderingProperty)
            {
                return SortDirection.Ascending;
            }
        }
        return SortDirection.None;
    }
}
