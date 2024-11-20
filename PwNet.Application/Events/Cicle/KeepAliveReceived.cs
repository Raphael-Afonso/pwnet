using PwNet.Application.Services;

namespace PwNet.Application.Events.Cicle
{
    public class KeepAliveReceived(PlayerSession PlayerSession) : BasePlayerMessageEvent(PlayerSession)
    {
    }
}
