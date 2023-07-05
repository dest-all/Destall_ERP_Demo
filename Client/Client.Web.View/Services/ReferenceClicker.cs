using Protocol.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Services
{
    public interface IReferenceClickHandler
    {
        Task HandleAsync(IReference reference);
    }
    public class FeedReferenceClicker : IReferenceClickHandler
    {
        readonly IFeedManager _feedManager;

        public FeedReferenceClicker(IFeedManager feedManager)
        {
            _feedManager = feedManager;
        }

        public async Task HandleAsync(IReference reference)
        {
            _feedManager.DrawnIn = true;
            await _feedManager.AddOneToFeedAsync(reference);
        }
    }
}
