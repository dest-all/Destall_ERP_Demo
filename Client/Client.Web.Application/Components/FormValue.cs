using DestallMaterials.WheelProtection.Extensions.Enumerables;
using DestallMaterials.WheelProtection.Extensions.Strings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Web.Application.Components
{
    class FormValue<T>
    {
        public T Value { get; set; }
        public bool Errorred => ErrorMessages.HasContent();
        public IReadOnlyList<string> ErrorMessages { get; set; }

        public string AggregatedError => Errorred ? ErrorMessages.Join(";") : "";
    }
}
