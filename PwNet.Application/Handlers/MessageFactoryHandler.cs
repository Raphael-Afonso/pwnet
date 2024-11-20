using PwNet.Application.Dto;
using PwNet.Application.Events.Cicle;
using PwNet.Application.Events.Login;
using PwNet.Application.Interfaces.Events;
using PwNet.Application.Interfaces.UseCases;
using PwNet.Domain.Messages.Enums;

namespace PwNet.Application.Handlers
{
    public class MessageFactoryHandler(IReadMessageUseCase formatRawMessageUseCase, IEventDispatcher eventDispatcher) : IEventHandler<PlayerMessageContext>
    {
        public async Task Handle(PlayerMessageContext messageContext, CancellationToken cancellationToken)
        {
            var (messageType, message) = formatRawMessageUseCase.Execute(messageContext);

            dynamic messageEvent = messageType switch
            {
                ClientMessageTypes.LoginRequest => new LoginRequestedEvent(message, messageContext.PlayerSession),
                ClientMessageTypes.KeepAlive => new KeepAliveReceived(messageContext.PlayerSession),
                _ => throw new NotSupportedException("Message type not supported")
            };

            await eventDispatcher.Dispatch(messageEvent, cancellationToken);
        }
    }
}
