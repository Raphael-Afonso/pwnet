namespace PwNet.Application.Interfaces.Events
{
    public interface IEventDispatcher
    {
        Task Dispatch<T>(T eventArgs, CancellationToken cancellationToken) where T : class;
    }
}
