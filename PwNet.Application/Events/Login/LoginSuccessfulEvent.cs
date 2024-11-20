using PwNet.Application.Services;
using PwNet.Domain.Messages.Client;

namespace PwNet.Application.Events.Login
{
    public class LoginSuccessfulEvent(AuthenticationRequest loginRequest, PlayerSession playerSession) : BasePlayerMessageEvent(playerSession)
    {
        public AuthenticationRequest LoginRequest { get; set; } = loginRequest;
    }
}
