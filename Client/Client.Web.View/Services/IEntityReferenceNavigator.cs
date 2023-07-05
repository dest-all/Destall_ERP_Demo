using Protocol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Services
{
    public interface IEntityReferenceNavigator
    {
        Task NavigateToReferenceAsync(IReference reference);
    }

    public class FeedReferenceNavigator : IEntityReferenceNavigator
    {
        public async Task NavigateToReferenceAsync(IReference reference)
        {

        }
    }
}
