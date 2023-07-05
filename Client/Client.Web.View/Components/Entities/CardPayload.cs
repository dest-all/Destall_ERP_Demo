using Protocol.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Components.Entities
{
    public struct CardPayload
    {
        public CardPayload()
        {
        }

        public object Item { get; init; }
        public Func<Task> OnChange { get; init; }
        public IPermissionsReadOnlyModel Permissions { get; init; }
        public bool ShowHeader { get; init; }
        public bool Collapsed { get; init; } = true;
        public Action<bool> OnCollapsedChanged { get; init; } = c => { };
        public Func<Task> OnDeleted { get; set; } = async () => { };
    }
}
