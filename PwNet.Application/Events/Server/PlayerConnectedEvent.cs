using PwNet.Application.Services;

namespace PwNet.Application.Events.Server
{
    public class PlayerConnectedEvent(PlayerSession PlayerSession) : BasePlayerMessageEvent(PlayerSession)
    {
    }
}
