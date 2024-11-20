using PwNet.Domain.Messages.Enums;

namespace PwNet.Domain.Messages.Server
{
    public class AuthenticationSuccessReply : BaseServerMessage, IServerMessage
    {
        public override ServerMessageTypes PacketType { get; protected set; } = ServerMessageTypes.AuthenticationExchange;
        public override byte PacketLength { get; protected set; } = 0x12;
        public byte KeyLength { get; private set; } = 16;
        public byte[] Key { get; private set; }
        public byte Unknown { get; private set; } = 0x00;

        public AuthenticationSuccessReply()
        {
            Random random = new();

            Key = new byte[KeyLength];
            random.NextBytes(Key);
        }

        public async Task<byte[]> GetBytesAsync(CancellationToken cancellationToken)
        {
            return await BuildByteArray(cancellationToken,
                PacketType,
                PacketLength,
                KeyLength,
                Key,
                Unknown
                );
        }
    }
}
