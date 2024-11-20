using PwNet.Domain.Messages.Enums;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;

namespace PwNet.Domain.Messages.Server
{
    public abstract class BaseServerMessage
    {
        public virtual ServerMessageTypes PacketType { get; protected set; }
        public abstract byte PacketLength { get; protected set; }

        protected static async Task<byte[]> BuildByteArray(CancellationToken cancellationToken, params object[] properties)
        {
            var pipe = new Pipe();
            var writer = pipe.Writer;

            foreach (var property in properties)
            {
                byte[] formattedByte = property switch
                {
                    byte[] bytes => bytes,
                    byte @byte => [@byte],
                    int intValue => [(byte)intValue],
                    double doubleValue => [(byte)doubleValue],

                    uint intValue => BitConverter.GetBytes(intValue),
                    short shortValue => BitConverter.GetBytes(shortValue),
                    string stringValue => Encoding.ASCII.GetBytes(stringValue),

                    ServerMessageTypes serverMessageTypes => [(byte)serverMessageTypes],
                    AuthenticationMethod authenticationMethod => [(byte)authenticationMethod],
                    ErrorCodes errorCodes => BitConverter.GetBytes((int)errorCodes).Reverse().ToArray(),

                    _ => throw new InvalidOperationException($"Unsupported property type: {property.GetType()}"),
                };

                await writer.WriteAsync(formattedByte, cancellationToken);
            }

            await writer.CompleteAsync();
            pipe.Reader.TryRead(out var result);

            var buffer = result.Buffer;
            byte[] data = buffer.ToArray();

            pipe.Reader.AdvanceTo(buffer.End);
            return data;
        }
    }
}
