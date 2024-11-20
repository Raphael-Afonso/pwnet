using PwNet.Application.Services;

namespace PwNet.Application.Dto
{
    public record PlayerMessageContext(PlayerSession PlayerSession, byte[] Content) : BaseMessageContext(PlayerSession);
}
