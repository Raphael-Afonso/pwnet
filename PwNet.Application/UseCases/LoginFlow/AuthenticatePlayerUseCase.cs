using Microsoft.Extensions.Logging;
using PwNet.Application.Interfaces.Infra;
using PwNet.Application.Interfaces.Infra.Persistence;
using PwNet.Application.Interfaces.UseCases;
using PwNet.Domain.Entities;
using PwNet.Domain.Messages.Client;

namespace PwNet.Application.UseCases.PlayerFeatures
{
    public class AuthenticatePlayerUseCase(IGenericRepository<Player> playerRepository, IPasswordCryptography passwordCryptography, ILogger<AuthenticatePlayerUseCase> logger) : IAuthenticatePlayerUseCase
    {
        public async Task<Player?> ExecuteAsync(AuthenticationRequest request, CancellationToken cancellationToken)
        {
            try
            {
                string passwordHash = passwordCryptography.HashPassword(request.PasswordHash);

                var player = await playerRepository.GetFirstByAction(player => player.Username == request.Username, cancellationToken);

                if (player is null) return default;

                bool isCredentialsValid = passwordCryptography.VerifyPassword(player.Password, passwordHash);

                if (isCredentialsValid) return player;

                return default;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[AuthenticatePlayerUseCase] Authentication for user {user} failed.", request.Username);

                throw;
            }
        }
    }
}
