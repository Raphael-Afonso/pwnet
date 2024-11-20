using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PwNet.Application.Interfaces.Events;

namespace PwNet.Application.Events
{
    /// <summary>
    /// Represents an event dispatcher that dispatches domain events to their respective event handlers.
    /// </summary>
    public class EventDispatcher(IServiceProvider serviceProvider, ILogger<EventDispatcher> logger) : IEventDispatcher
    {
        /// <summary>
        /// Dispatches the specified event arguments to its event handlers.
        /// </summary>
        /// <typeparam name="T">The type of the event arguments.</typeparam>
        /// <param name="eventArgs">The event arguments to dispatch.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task Dispatch<T>(T eventArgs, CancellationToken cancellationToken) where T : class
        {
            var handlers = serviceProvider.GetServices<IEventHandler<T>>();

            var tasks = handlers.Select(async handler =>
            {
                try
                {
                    logger.LogInformation("Dispatching event {event} with handler {handler}. ", typeof(T).Name, handler.GetType().Name);

                    await handler.Handle(eventArgs, cancellationToken);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error handling event of type {event} with handler {handler}", typeof(T).Name, handler.GetType().Name);
                }
            });

            await Task.WhenAll(tasks);
        }
    }
}