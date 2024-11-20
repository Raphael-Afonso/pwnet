namespace PwNet.Common.Configurations
{
    public class ServerConfig
    {
        public required string ClientHashSignature { get; set; }
        public required double ExpMultiplier { get; set; }
        public required int MaxSessions { get; set; }
    }
}
