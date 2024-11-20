using PwNet.Common.Configurations;
using PwNet.Domain.Messages.Enums;

namespace PwNet.Domain.Messages.Server
{
    public class InitialChallenge : BaseServerMessage, IServerMessage
    {
        public override ServerMessageTypes PacketType { get; protected set; } = ServerMessageTypes.Challenge;
        public override byte PacketLength { get; protected set; } = 0x3A;

        public byte KeyLength { get; private set; } = 16;
        public byte ServerLoad { get; private set; } = 255; // 0 - 255
        public short Unknown1 { get; private set; } = 0;    // 0
        public byte ServerFlags { get; private set; }
        public uint Unknown2 { get; private set; } = 0;     // 0
        public byte[] Key { get; private set; }
        public byte[] GameVersion { get; private set; } = [0, 1, 5, 9];
        public AuthenticationMethod AuthMethod { get; private set; } = AuthenticationMethod.SHA256;
        public int ClientSignatureLength { get; private set; }
        public byte[] ClientSignature { get; private set; }
        public double ExpMultiplier { get; private set; }

        public InitialChallenge(ServerConfig serverConfig)
        {
            //TODO: Move to config
            ServerFlags = 0x50;
            ServerFlags |= 0x80; // pvp
            ServerFlags |= 0x08; // spiritBonus
            ServerFlags |= 0x04; // dropbonus
            ServerFlags |= 0x02; // moneyBonus

            Random random = new();
            Key = new byte[8];
            random.NextBytes(Key);

            ClientSignature = Convert.FromHexString(serverConfig.ClientHashSignature);
            ClientSignatureLength = ClientSignature.Length;

            ExpMultiplier = serverConfig.ExpMultiplier;
        }

        public async Task<byte[]> GetBytesAsync(CancellationToken cancellationToken)
        {
            return await BuildByteArray(
                cancellationToken: cancellationToken,
                PacketType,
                PacketLength,
                KeyLength,
                ServerLoad,
                Unknown1,
                ServerFlags,
                Unknown2,
                Key,
                GameVersion,
                AuthMethod,
                ClientSignatureLength,
                ClientSignature,
                ExpMultiplier
            );
        }
    }
}
