using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Administration
{
    public struct ActionAccessibility
    {
        public ActionAccessibility()
        {
            MayAccess = true;
            MissingPermissions = Array.Empty<string>();
        }

        public ActionAccessibility(IEnumerable<string> missingPermissions)
        {
            MayAccess = !missingPermissions.Any();
            MissingPermissions = missingPermissions.ToArray();
        }

        public bool MayAccess { get; init; }
        public IReadOnlyList<string> MissingPermissions { get; init; }
    }
}
