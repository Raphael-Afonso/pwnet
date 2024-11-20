namespace PwNet.Common.Cryptography
{
    public class Rc4Cryptography
    {
        private readonly byte[] S = new byte[256];
        private int x = 0;
        private int y = 0;

        public Rc4Cryptography(byte[] key)
        {
            Initialize(key);
        }

        private void Initialize(byte[] key)
        {
            int keyLength = key.Length;
            for (int i = 0; i < 256; i++)
            {
                S[i] = (byte)i;
            }

            int j = 0;
            for (int i = 0; i < 256; i++)
            {
                j = (j + S[i] + key[i % keyLength]) % 256;
                Swap(i, j);
            }
        }

        private void Swap(int i, int j)
        {
            (S[j], S[i]) = (S[i], S[j]);
        }

        public byte[] EncryptDecrypt(byte[] data)
        {
            byte[] result = new byte[data.Length];
            for (int m = 0; m < data.Length; m++)
            {
                x = (x + 1) % 256;
                y = (y + S[x]) % 256;
                Swap(x, y);
                byte k = S[(S[x] + S[y]) % 256];
                result[m] = (byte)(data[m] ^ k);
            }
            return result;
        }
    }
}
