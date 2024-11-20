using Microsoft.Extensions.Logging;
using PwNet.Application.Dto;
using PwNet.Application.Events.Server;
using PwNet.Application.Interfaces.Events;
using PwNet.Application.Interfaces.Services;
using System.Buffers;
using System.IO.Pipelines;

namespace PwNet.Application.Services
{
    public class PlayerMessageListener(IEventDispatcher eventDispatcher, ILogger<PlayerMessageListener> logger) : IPlayerMessageListener
    {
        public async Task HandleClientAsync(PlayerSession playerSession, CancellationToken cancellationToken)
        {
            var stream = playerSession.ReadStream();
            var reader = PipeReader.Create(stream);

            try
            {
                var clientConnectedEvent = new PlayerConnectedEvent(playerSession);
                await eventDispatcher.Dispatch(clientConnectedEvent, cancellationToken);

                while (playerSession.IsConnected)
                {
                    if (cancellationToken.IsCancellationRequested) break;

                    var result = await reader.ReadAsync(cancellationToken);
                    var buffer = result.Buffer;

                    if (buffer.IsEmpty && result.IsCompleted)
                    {
                        logger.LogWarning("[PlayerMessageHandler] Received empty packet from player {ip}.", playerSession.ClientIp);
                        break;
                    }

                    byte[] rawData = buffer.ToArray();
                    reader.AdvanceTo(buffer.End);

                    var context = new PlayerMessageContext(playerSession, rawData);
                    await eventDispatcher.Dispatch(context, cancellationToken);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "[PlayerMessageHandler] Player {ip} handler error.", playerSession.ClientIp);
            }
            finally
            {
                logger.LogInformation("[PlayerMessageHandler] Player {ip} disconnected.", playerSession.ClientIp);

                playerSession.Close();
            }
        }
    }
}
