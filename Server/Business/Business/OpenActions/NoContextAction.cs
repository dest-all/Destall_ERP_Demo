using System;
using System.Threading.Tasks;
using Business.Actions;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;

namespace Business.OpenActions
{
    public partial class NoContextAction : SingletonActionContainer
    {
        public async Task<bool> ThrowException(string exceptionMessage) 
            => throw new Exception(exceptionMessage);
    }
}