namespace PwNet.Application.Interfaces.Events
{
    public interface IEventHandler<T>
    {
        Task Handle(T eventArgs, CancellationToken cancellationToken);
    }
}
