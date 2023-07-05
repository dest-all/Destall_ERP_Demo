using Business.Actions;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.OpenActions
{
    [ProcessServerRequest]
    [AllowUnauthorised]
    public partial class TestMethodContainer : ActionContainer
    {
        public uint Method(uint i)
        {
            unchecked
            {
                return i * i;
            }
        }

        public async Task<bool> HoldDbContext(int secondsToHoldFor)
        {
            using var heldThing = await this.GetRepositoryAsync();
            await Task.Delay(TimeSpan.FromSeconds(secondsToHoldFor));
            return true;
        }
    }
}
