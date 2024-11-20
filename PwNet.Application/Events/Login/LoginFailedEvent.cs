using PwNet.Application.Services;
using PwNet.Domain.Messages.Enums;

namespace PwNet.Application.Events.Login
{
    public class LoginFailedEvent(PlayerSession playerSession, ErrorCodes errorCodes) : BasePlayerMessageEvent(playerSession)
    {
        public ErrorCodes ErrorCode { get; set; } = errorCodes;
    }
}
