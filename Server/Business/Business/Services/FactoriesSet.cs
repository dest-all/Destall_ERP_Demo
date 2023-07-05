using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Data.Repository;

namespace Business.Services;

public class FactoriesSet : IDisposable
{
    public class FacilitySetup
    {
        public required Func<IExecutionContext, Task<IDataRepository>> GetRepositoryAsync { get; init; }
        public required Func<Task<HttpClient>> GetHttpClientAsync { get; init; }
        public required Func<Type, ILogger> CreateLogger { get; init; }
    }
    
    readonly FacilitySetup _configuration; 
    
    public FactoriesSet(FacilitySetup configuration)
    {
        _configuration = configuration;
    }

    List<Action> _objectReleases = new List<Action>();

    public Task<IDataRepository> GetRepositoryAsync(IExecutionContext executionContext) 
        => _configuration.GetRepositoryAsync(executionContext);

    public Task<HttpClient> GetHttpClientAsync()
        => _configuration.GetHttpClientAsync();


    public ILogger GetLogger(Type type) => _configuration.CreateLogger(type);
    
    public void Dispose()
    {
        foreach (var releasable in _objectReleases.Where(obj => obj != null))
        {
            releasable();
        }
    }
}
