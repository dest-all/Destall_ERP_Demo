using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.View.Components.Entities
{
    public abstract class InputComponent : ComponentBase
    {
        [Parameter]
        public bool Disabled { get; set; }
    }

    public abstract class EntityProperty<TModel> : InputComponent
    {
        [Parameter]
        public TModel Item { get; set; }
    }
}
