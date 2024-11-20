namespace PwNet.Domain.Messages.Enums
{
    public enum ClientMessageTypes : byte
    {
        LoginRequest = 0x03,
        ClientKeyExchange = 0x02,
        RoleList = 0x52,
        KeepAlive = 0x5A,
    }
}
