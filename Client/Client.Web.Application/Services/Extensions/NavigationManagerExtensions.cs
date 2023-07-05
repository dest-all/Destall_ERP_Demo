using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.Application.Services.Extensions
{
    public static class NavigationManagerExtensions
    {
        public static void RedirectToLogin(this NavigationManager navMan)
        {
            navMan.NavigateTo($"authentication/login?returnUrl={Uri.EscapeDataString(navMan.Uri)}");
        }
    }
}
