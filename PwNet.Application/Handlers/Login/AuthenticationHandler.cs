using Microsoft.Extensions.Logging;
using PwNet.Application.Events.Login;
using PwNet.Application.Interfaces.Events;
using PwNet.Application.Interfaces.Services;
using PwNet.Application.Interfaces.UseCases;
using PwNet.Domain.Entities;
using PwNet.Domain.Messages.Enums;

namespace PwNet.Application.Handlers.Login
{
    public class AuthenticationHandler(IAuthenticatePlayerUseCase authenticatePlayerUseCase, ICheckIpBlockUseCase checkIpBlockUseCase, ISessionsManager sessionsManager, IEventDispatcher eventDispatcher, ILogger<AuthenticationHandler> logger) : IEventHandler<LoginRequestedEvent>
    {
        public async Task Handle(LoginRequestedEvent loginRequestedEvent, CancellationToken cancellationToken)
        {
            //TODO: Force Login feature
            try
            {
                logger.LogInformation("[AuthenticationHandler] User {username} trying to login.", loginRequestedEvent.LoginRequest.Username);

                bool hasIpBlocked = await checkIpBlockUseCase.ExecuteAsync(loginRequestedEvent.PlayerSession.ClientIp?.ToString(), cancellationToken);
                if (hasIpBlocked)
                {
                    await eventDispatcher.Dispatch(new LoginFailedEvent(loginRequestedEvent.PlayerSession, ErrorCodes.UnauthorizedIPConnection), cancellationToken);
                    return;
                }

                if (sessionsManager.ServerIsFull)
                {
                    await eventDispatcher.Dispatch(new LoginFailedEvent(loginRequestedEvent.PlayerSession, ErrorCodes.ServerIsFull), cancellationToken);
                    return;
                }

                Player? player = await authenticatePlayerUseCase.ExecuteAsync(loginRequestedEvent.LoginRequest, cancellationToken);

                if (player is null)
                {
                    await eventDispatcher.Dispatch(new LoginFailedEvent(loginRequestedEvent.PlayerSession, ErrorCodes.InvalidLogin), cancellationToken);
                    return;
                }

                if (!player.Active)
                {
                    await eventDispatcher.Dispatch(new LoginFailedEvent(loginRequestedEvent.PlayerSession, ErrorCodes.AccountBlocked), cancellationToken);
                    return;
                }

                await eventDispatcher.Dispatch(new LoginSuccessfulEvent(loginRequestedEvent.LoginRequest, loginRequestedEvent.PlayerSession), cancellationToken);
            }
            catch (Exception ex)
            {
                await eventDispatcher.Dispatch(new LoginFailedEvent(loginRequestedEvent.PlayerSession, ErrorCodes.LoginFailed), cancellationToken);

                logger.LogError(ex, "[AuthenticationHandler] Fail while autenticating.");
            }
        }
    }
}
