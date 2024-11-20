using PwNet.Application.Dto;
using PwNet.Domain.Messages.Enums;

namespace PwNet.Application.Interfaces.UseCases
{
    public interface IReadMessageUseCase
    {
        (ClientMessageTypes, byte[]) Execute(PlayerMessageContext message);
    }
}