using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Communication
{
    public interface IServerActionPoint<TPayload, TResponse>
    {
        Task<TResponse> CallAsync(TPayload payload);
    }

    public interface IParameterlessServerActionPoint<TResponse>
    {
        Task<TResponse> CallAsync();
    }

    public interface IParameterlessServerActionPointWithCache<TResponse> : IParameterlessServerActionPoint<TResponse>
    {
        Task<TResponse> CallDirectlyAsync();
        Task InvalidateCacheAsync();
    }

    public interface IServerActionPointWithCache<TPayload, TResponse> : IServerActionPoint<TPayload, TResponse>
    {
        Task<TResponse> CallDirectlyAsync(TPayload payload);
        Task InvalidateCacheAsync(TPayload payload);
        Task InvalidateAllCacheAsync();
    }

}
