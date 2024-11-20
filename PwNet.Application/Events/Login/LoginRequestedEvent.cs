using PwNet.Application.Services;
using PwNet.Domain.Messages.Client;

namespace PwNet.Application.Events.Login
{
    public class LoginRequestedEvent(byte[] loginMessage, PlayerSession playerSession) : BasePlayerMessageEvent(playerSession)
    {
        public AuthenticationRequest LoginRequest { get; set; } = new AuthenticationRequest(loginMessage);
    }
}
