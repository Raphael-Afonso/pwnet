using PwNet.Application.Services;
using PwNet.Domain.Messages;

namespace PwNet.Application.Dto
{
    public record ServerMessageContext(PlayerSession PlayerSession, IServerMessage ServerMessage) : BaseMessageContext(PlayerSession);
}
