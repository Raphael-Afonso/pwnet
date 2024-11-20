using PwNet.Application.Dto;
using PwNet.Application.Interfaces.UseCases;

namespace PwNet.Application.UseCases.MessageExchange
{
    public class WriteMessageUseCase : IWriteMessageUseCase
    {
        public async Task ExecuteAsync(ServerMessageContext context, CancellationToken cancellationToken)
        {
            if (!context.PlayerSession.IsCommunicationEncrypted())
            {
                await context.PlayerSession.SendMessageAsync(context.ServerMessage, cancellationToken);
                return;
            }

            // Encrypt message
        }
    }
}
