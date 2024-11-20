using Microsoft.Extensions.Options;
using PwNet.Application.Dto;
using PwNet.Application.Events.Server;
using PwNet.Application.Interfaces.Events;
using PwNet.Application.Interfaces.UseCases;
using PwNet.Common.Configurations;
using PwNet.Domain.Messages.Server;

namespace PwNet.Application.Handlers.Server
{
    public class SendServerChallengeHandler(IWriteMessageUseCase writeMessageUseCase, IOptions<ServerConfig> options) : IEventHandler<PlayerConnectedEvent>
    {
        public async Task Handle(PlayerConnectedEvent eventArgs, CancellationToken cancellationToken)
        {
            var message = new ServerMessageContext(eventArgs.PlayerSession, new InitialChallenge(options.Value));

            await writeMessageUseCase.ExecuteAsync(message, cancellationToken);
        }
    }
}
