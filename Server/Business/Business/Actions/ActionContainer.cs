using Business.ActionPoints;
using Business.Services;
using Protocol;
using DestallMaterials.CodeGeneration.ERP.ClientDependency;
using System;
using Microsoft.Extensions.Logging;
using Business.Administration;
using System.Threading.Tasks;
using Data.Repository;

namespace Business.Actions
{
    public struct FactorySeed
    {
        public Business.Services.FactoriesSet.FacilitySetup FactoriesConfiguration { get; init; }
    }

    [CreateDispatcher]
    public abstract class SingletonActionContainer : IDisposable
    {
        protected IBusinessActionsNetSingleton _business { get; }
        readonly FactoriesSet _factoriesSet;

        protected ILogger Logger
        {
            get
            {
                if (_logger == null)
                {
                    _logger = _factoriesSet.GetLogger(this.GetType());
                }
                return _logger;

            }
        }
        ILogger _logger;

        protected FactoriesSet Factories => _factoriesSet;
        protected SingletonActionContainer(IBusinessActionsNetSingleton dispatchersAccessor, FactoriesSet factoriesSet)
        {
            _business = dispatchersAccessor;
            _factoriesSet = factoriesSet;
        }

        [NotOpen]
        public void Dispose()
        {
            Factories?.Dispose();
        }
    }

    public abstract class ActionContainer : SingletonActionContainer
    {
        protected ActionContainer(IBusinessActionsNet business, FactoriesSet factoriesSet) : base(business, factoriesSet)
        {
            ExecutionContext = business.ExecutionContext;
            _business = business;
        }

        protected readonly new IBusinessActionsNet _business;

        protected IExecutionContext ExecutionContext { get; }

        protected Session CurrentSession => _business.Administration.SessionsManagement.GetOpenSessionByKey(ExecutionContext.SessionKey);

        protected Task<IDataRepository> GetRepositoryAsync() => Factories.GetRepositoryAsync(ExecutionContext);
    }

}
