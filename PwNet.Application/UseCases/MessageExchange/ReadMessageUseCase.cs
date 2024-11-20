using PwNet.Application.Dto;
using PwNet.Application.Interfaces.UseCases;
using PwNet.Domain.Messages.Enums;

namespace PwNet.Application.UseCases.MessageExchange
{
    public class ReadMessageUseCase : IReadMessageUseCase
    {
        //TODO: Move cryptography to here
        public (ClientMessageTypes, byte[]) Execute(PlayerMessageContext context)
        {
            if (context.Content is null || context.Content.Length == 0)
            {
                throw new ArgumentException("Message data is null or empty");
            }

            return ParseRawData(
                context.PlayerSession.IsCommunicationEncrypted() ?
                    ParseEncryptedData(context) : context.Content
            );
        }

        private static byte[] ParseEncryptedData(PlayerMessageContext messageContext)
        {
            return messageContext.PlayerSession.Encryption!.ClientToServer.EncryptDecrypt(messageContext.Content);
        }

        private static (ClientMessageTypes, byte[]) ParseRawData(byte[] rawData)
        {
            byte messageTypeByte = rawData[0];

            EnsureMessageTypeExists(messageTypeByte);

            return ((ClientMessageTypes)messageTypeByte, rawData[1..]);
        }

        private static void EnsureMessageTypeExists(byte messageTypeByte)
        {
            if (!Enum.IsDefined(typeof(ClientMessageTypes), messageTypeByte))
            {
                throw new NotImplementedException($"Unknow message type received. {messageTypeByte}");
            }
        }
    }
}
