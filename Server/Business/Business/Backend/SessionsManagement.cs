using System;
using System.Collections.Generic;
using System.Linq;
using Business.Actions;
using Common.AdvancedDataStructures;

namespace Business.Administration
{
    public partial class SessionsManagement : SingletonActionContainer
    {
        static readonly IdsGenerator _idsGenerator = new IdsGenerator();
        readonly Dictionary<string, Session> _sessions = new();
        public Session GetOpenSessionByKey(string key)
        { 
            var result = _sessions.ContainsKey(key) ? _sessions[key] : null;
            return result;
        }

        public Session EnsureCreatedSessionForUser(long userId)
        {
            var existingSession = _sessions.FirstOrDefault(s => s.Value.UserId == userId).Value;
            if (existingSession is not null)
            {
                return existingSession;
            }

            var sessionId = Guid.NewGuid().ToString();
            var result = new Session(() => _sessions.Remove(sessionId))
            {
                Key = sessionId,
                LastTimeActive = DateTime.UtcNow,
                UserId = userId
            };

            _sessions.Add(sessionId, result);

            return result;
        }

        public bool CloseSession(string sessionKey)
            => _sessions.Remove(sessionKey);

        public bool OpenFullAccessSessionForTesting(string sessionId)
        {
            const long userId = 1;
            var result = new Session(() => _sessions.Remove(sessionId))
            {
                Key = sessionId,
                LastTimeActive = DateTime.UtcNow,
                UserId = userId
            };

            _sessions.Add(sessionId, result);

            return true;
        }
    }
}
