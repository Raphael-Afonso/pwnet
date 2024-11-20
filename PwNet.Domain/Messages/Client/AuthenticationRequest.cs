using PwNet.Common.Extensions;
using System.Text;

namespace PwNet.Domain.Messages.Client
{
    public class AuthenticationRequest
    {
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }
        public byte[] PasswordHashBytes { get; private set; }

        public AuthenticationRequest(byte[] loginMessage)
        {
            loginMessage
                .UnMarshalByte(out var _)
                .UnMarshalByte(out var usernameLength)
                .UnMarshalBytes(out var usernameBytes, usernameLength)
                .UnMarshalByte(out var passwordHashLength)
                .UnMarshalBytes(out var passwordBytes, passwordHashLength);

            Username = Encoding.ASCII.GetString(usernameBytes);
            PasswordHash = BitConverter.ToString(passwordBytes).Replace("-", string.Empty);
            PasswordHashBytes = passwordBytes;
        }
    }
}
