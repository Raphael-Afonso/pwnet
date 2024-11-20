using PwNet.Application.Interfaces.UseCases;
using PwNet.Common.Cryptography;
using PwNet.Domain.Messages.Client;
using System.Security.Cryptography;
using System.Text;

namespace PwNet.Application.UseCases.LoginFlow
{
    public class BuildCryptographyUseCase : IBuildCryptographyUseCase
    {
        public Task<Rc4Cryptography> ExecuteAsync(AuthenticationRequest loginRequest, byte[] key, CancellationToken cancellationToken)
        {
            HMACMD5 md5 = new(Encoding.ASCII.GetBytes(loginRequest.Username));
            byte[] buffer = new byte[32];

            Array.Copy(loginRequest.PasswordHashBytes, 0, buffer, 0, Math.Min(16, loginRequest.PasswordHashBytes.Length));
            Array.Copy(key, 0, buffer, 16, Math.Min(16, key.Length));

            return Task.FromResult(
                new Rc4Cryptography(md5.ComputeHash(buffer))
            );
        }
    }
}
