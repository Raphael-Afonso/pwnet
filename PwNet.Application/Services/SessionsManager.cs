using Microsoft.Extensions.Options;
using PwNet.Application.Interfaces.Services;
using PwNet.Common.Configurations;
using System.Collections.Concurrent;

namespace PwNet.Application.Services
{
    public class SessionsManager(IOptions<ServerConfig> options) : ISessionsManager
    {
        private readonly ConcurrentDictionary<Guid, PlayerSession> _currentSessions = [];

        private readonly int _maxSessions = options.Value.MaxSessions;

        public bool ServerIsFull { get => _currentSessions.Count >= _maxSessions; }

        public Guid Add(PlayerSession session)
        {
            var newGuid = Guid.NewGuid();

            _currentSessions.TryAdd(newGuid, session);

            return newGuid;
        }

        public void Remove(Guid sessionGuid)
        {
            _currentSessions.TryRemove(sessionGuid, out _);
        }
    }
}
