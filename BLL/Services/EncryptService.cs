using System;
using System.IO;
using System.Security.Cryptography;
using BLL.Interface.Services;

namespace BLL.Services.ExecuteServiceUtils
{
    public class EncryptService : IEncryptService
    {
        private const int KEY_SIZE = 32;
        private const int VECTOR_SIZE = 16;
        private static byte[] _cryptoKey;
        private readonly RNGCryptoServiceProvider _cryptoProvider;

        public EncryptService()
        {
            _cryptoProvider = new RNGCryptoServiceProvider();
            _cryptoKey = new byte[KEY_SIZE];
            _cryptoProvider.GetBytes(_cryptoKey);
        }

        public byte[] Encrypt(int data)
        {
            byte[] result;
            using (var aesAlg = Aes.Create())
            {
                aesAlg.KeySize = KEY_SIZE*8;
                aesAlg.Key = _cryptoKey;
                var vector = new byte[VECTOR_SIZE];
                _cryptoProvider.GetBytes(vector);
                aesAlg.IV = vector;

                var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                using (var msEncrypt = new MemoryStream())
                {
                    msEncrypt.Write(vector, 0, vector.Length);
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(BitConverter.GetBytes(data), 0, sizeof(int));                        
                        result = msEncrypt.ToArray();
                    }                   
                }
                
            }
            return result;
        }

        public int Decrypt(byte[] data)
        {
            int result;

            using (var aesAlg = Aes.Create())
            {
                aesAlg.KeySize = KEY_SIZE*8;
                aesAlg.Key = _cryptoKey;
                using (var msDecrypt = new MemoryStream(data))
                {
                    var vector = new byte[VECTOR_SIZE];
                    msDecrypt.Read(vector, 0, vector.Length);
                    aesAlg.IV = vector;
                    var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        var buffer = new byte[sizeof(int)];
                        csDecrypt.Read(buffer, 0, sizeof(int));
                        result = BitConverter.ToInt32(buffer, 0);
                    }
                }
                                   
            }
            return result;
        }

    }
}
