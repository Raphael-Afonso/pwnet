using PwNet.Application.Dto;

namespace PwNet.Application.Interfaces.UseCases
{
    public interface IWriteMessageUseCase
    {
        Task ExecuteAsync(ServerMessageContext context, CancellationToken cancellationToken);
    }
}