using PwNet.Common.Cryptography;
using System.Diagnostics.CodeAnalysis;

namespace PwNet.Application.Dto
{
    public class MessageCryptography
    {
        public required Rc4Cryptography ClientToServer { get; set; }
        public Rc4Cryptography? ServerToClient { get; set; }

        public bool IsEncrypted { get => ClientToServer is not null; }

        public string? PlayerUsername { get; set; }
        public byte[]? PlayerPasswordHash { get; set; }

        [MemberNotNull(nameof(ServerToClient))]
        public void AddServerToClientKey(Rc4Cryptography key)
        {
            ServerToClient = key;

            PlayerUsername = null;
            PlayerPasswordHash = null;
        }
    }
}
