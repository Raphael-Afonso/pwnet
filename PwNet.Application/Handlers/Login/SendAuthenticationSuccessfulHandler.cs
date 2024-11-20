using PwNet.Application.Events.Login;
using PwNet.Application.Interfaces.Events;
using PwNet.Application.Interfaces.UseCases;
using PwNet.Domain.Messages.Server;

namespace PwNet.Application.Handlers.Login
{
    public class SendAuthenticationSuccessfulHandler(IBuildCryptographyUseCase buildCryptographyUseCase, IWriteMessageUseCase writeMessageUseCase) : IEventHandler<LoginSuccessfulEvent>
    {
        public async Task Handle(LoginSuccessfulEvent eventArgs, CancellationToken cancellationToken)
        {
            var successfulReply = new AuthenticationSuccessReply();

            var rc4 = await buildCryptographyUseCase.ExecuteAsync(eventArgs.LoginRequest, successfulReply.Key, cancellationToken);

            await writeMessageUseCase.ExecuteAsync(eventArgs.PlayerSession.BuildServerMessage(successfulReply), cancellationToken);

            eventArgs.PlayerSession.AddServerToClientCryptography(
                rc4,
                eventArgs.LoginRequest.Username,
                eventArgs.LoginRequest.PasswordHashBytes
            );
        }
    }
}
