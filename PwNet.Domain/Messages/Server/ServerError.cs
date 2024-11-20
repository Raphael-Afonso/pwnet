using PwNet.Domain.Messages.Enums;

namespace PwNet.Domain.Messages.Server
{
    public class ServerError : BaseServerMessage, IServerMessage
    {
        public override byte PacketLength { get; protected set; }
        public ErrorCodes ErrorCode { get; set; }
        public int MessageLength { get; set; }
        public string Message { get; set; }
        public byte Unknown { get; set; } = 0x2E;

        public ServerError(ErrorCodes errorCode)
        {
            PacketType = ServerMessageTypes.Error;

            ErrorCode = errorCode;

            Message = "Login error"; // TODO: ???
            MessageLength = Message.Length;

            PacketLength = (byte)(6 + Message.Length);
        }

        public async Task<byte[]> GetBytesAsync(CancellationToken cancellationToken)
        {
            return await BuildByteArray(
                cancellationToken: cancellationToken,
                PacketType,
                PacketLength,
                ErrorCode,
                MessageLength,
                Message,
                Unknown
                );
        }
    }
}
