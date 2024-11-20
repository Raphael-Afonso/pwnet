using PwNet.Common.Cryptography;
using PwNet.Domain.Messages.Client;

namespace PwNet.Application.Interfaces.UseCases
{
    public interface IBuildCryptographyUseCase
    {
        Task<Rc4Cryptography> ExecuteAsync(AuthenticationRequest loginRequest, byte[] key, CancellationToken cancellationToken);
    }
}