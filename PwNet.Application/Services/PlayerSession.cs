using PwNet.Application.Dto;
using PwNet.Common.Cryptography;
using PwNet.Domain.Messages;
using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Sockets;

namespace PwNet.Application.Services
{
    public class PlayerSession(TcpClient client)
    {
        private readonly TcpClient Client = client;

        public MessageCryptography? Encryption { get; set; }

        public bool IsCompressed { get; set; }

        public bool IsConnected { get => Client.Connected; }
        public EndPoint? ClientIp { get => Client.Client?.RemoteEndPoint; }

        public async Task SendMessageAsync(IServerMessage message, CancellationToken cancellationToken) =>
            await SendMessageAsync(await message.GetBytesAsync(cancellationToken), cancellationToken);

        private async Task SendMessageAsync(byte[] message, CancellationToken cancellationToken)
        {
            Stream stream = Client.GetStream();

            if (!Client.Connected)
            {
                throw new InvalidOperationException($"Client {ClientIp} is not connected.");
            }

            await stream.WriteAsync(message, cancellationToken);
        }

        [MemberNotNull(nameof(Encryption))]
        public void AddServerToClientCryptography(Rc4Cryptography rc4, string username, byte[] passwordHash)
        {
            if (Encryption is not null)
                throw new InvalidOperationException("Connection is already encrypted!");

            Encryption = new MessageCryptography() {
                ClientToServer = rc4,
                PlayerUsername = username,
                PlayerPasswordHash = passwordHash
            };
        }

        [MemberNotNullWhen(true, nameof(Encryption))]
        public bool IsCommunicationEncrypted() => Encryption?.IsEncrypted ?? false;

        public ServerMessageContext BuildServerMessage(IServerMessage serverMessage) => 
            new(this, serverMessage);

        public void EnableCompression()
        {
            IsCompressed = true;
        }

        public Stream ReadStream() => Client.GetStream();

        public void Close()
        {
            Client.Close();
        }
    }
}
