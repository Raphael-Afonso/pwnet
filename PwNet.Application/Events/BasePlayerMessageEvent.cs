using PwNet.Application.Services;

namespace PwNet.Application.Events
{
    public abstract class BasePlayerMessageEvent(PlayerSession PlayerSession)
    {
        public PlayerSession PlayerSession { get; private set; } = PlayerSession;
    }
}
