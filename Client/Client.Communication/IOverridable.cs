using Protocol;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Communication
{
    public interface IOverridable<TParameter, TResponse>
        where TResponse : IProtocolMessage
        where TParameter : IProtocolMessage
    {
        Func<TParameter, TResponse> Override(Func<TParameter, TResponse> basicImplementation);
    }

    public interface IOverridable<TResponse>
        where TResponse : IProtocolMessage
    {
        Func<TResponse> Override(Func<TResponse> basicImplementation);
    }

    public class OverridesAccounter
    {
        readonly Dictionary<Type, object> _overrides = new();
        
        public void Override<T, TResponse>(Func<Func<TResponse>, TResponse> func)
            where T : IOverridable<TResponse>
            where TResponse : IProtocolMessage
        {
            _overrides[typeof(T)] = func;
        }

        public void Override<T, TParameter, TResponse>(Func<Func<TParameter, TResponse>, TParameter, TResponse> func)
            where T : IOverridable<TParameter, TResponse>
            where TResponse : IProtocolMessage
            where TParameter : IProtocolMessage
            => _overrides[typeof(TParameter)] = func;

        public Func<Func<TResponse>, TResponse> FindOverride<TResponse>(Type t)
            where TResponse : IProtocolMessage
        {

            if (_overrides.TryGetValue(t, out var result))
            {
                return result as Func<Func<TResponse>, TResponse> ?? throw new InvalidOperationException("Wrong stored func type.");
            }
            return given => given();
        }

        public Func<Func<TResponse>, TResponse> FindOverride<T, TResponse>()
            where T : IOverridable<TResponse>
            where TResponse : IProtocolMessage
            => FindOverride<TResponse>(typeof(T));

        public Func<Func<TParameter, TResponse>, TParameter, TResponse> FindOverride<T, TParameter, TResponse>()
            where T : IOverridable<TResponse>
            where TResponse : IProtocolMessage
            where TParameter : IProtocolMessage
            => FindOverride<TParameter, TResponse>(typeof(T));



        public Func<Func<TParameter, TResponse>, TParameter, TResponse> FindOverride<TParameter, TResponse>(Type t)
        {
            if (_overrides.TryGetValue(t, out var found))
            {
                return found as Func<Func<TParameter, TResponse>, TParameter, TResponse> ?? throw new InvalidOperationException("Wrong stored func type.");
            }
            return (given, param) => given(param);
        }
    }
}
