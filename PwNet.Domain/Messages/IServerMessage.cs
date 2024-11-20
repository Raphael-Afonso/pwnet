namespace PwNet.Domain.Messages
{
    public interface IServerMessage
    {
        Task <byte[]> GetBytesAsync(CancellationToken cancellationToken);
    }
}
