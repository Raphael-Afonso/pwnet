namespace PwNet.Common.Extensions
{
    public static class ByteExtensions
    {
        public static byte[] UnMarshalByte(this byte[] data, out byte result, int count = 0)
        {
            result = data[count];

            return data[1..];
        }

        public static byte[] UnMarshalBytes(this byte[] data, out byte[] result, int count = 0)
        {
            result = data[..count];

            return data[count..];
        }
    }
}
