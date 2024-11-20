using PwNet.Application.Events.Login;
using PwNet.Application.Interfaces.Events;
using PwNet.Application.Interfaces.UseCases;
using PwNet.Domain.Messages.Server;

namespace PwNet.Application.Handlers.Login
{
    public class SendAuthenticationErrorHandler(IWriteMessageUseCase writeMessageUseCase) : IEventHandler<LoginFailedEvent>
    {
        public async Task Handle(LoginFailedEvent eventArgs, CancellationToken cancellationToken)
        {
            var errorMessage = new ServerError(eventArgs.ErrorCode);

            await writeMessageUseCase.ExecuteAsync(eventArgs.PlayerSession.BuildServerMessage(errorMessage), cancellationToken);
        }
    }
}
