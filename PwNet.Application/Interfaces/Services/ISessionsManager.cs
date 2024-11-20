using PwNet.Application.Services;

namespace PwNet.Application.Interfaces.Services
{
    public interface ISessionsManager
    {
        public bool ServerIsFull { get;}

        Guid Add(PlayerSession session);
        void Remove(Guid sessionGuid);
    }
}
