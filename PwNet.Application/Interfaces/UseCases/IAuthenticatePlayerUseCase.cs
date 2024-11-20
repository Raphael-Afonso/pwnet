using PwNet.Domain.Entities;
using PwNet.Domain.Messages.Client;

namespace PwNet.Application.Interfaces.UseCases
{
    public interface IAuthenticatePlayerUseCase
    {
        Task<Player?> ExecuteAsync(AuthenticationRequest request, CancellationToken cancellationToken);
    }
}