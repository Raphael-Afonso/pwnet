using PwNet.Application.Services;

namespace PwNet.Application.Interfaces.Services
{
    public interface IPlayerMessageListener
    {
        Task HandleClientAsync(PlayerSession playerSession, CancellationToken cancellationToken);
    }
}