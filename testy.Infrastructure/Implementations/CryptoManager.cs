using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using testy.Application.Common.Interfaces;

namespace testy.Infrastructure.Implementations
{
    public class CryptoManager : ICryptoManager
    {
        private readonly byte[] _secretKey;
        private readonly byte[] _initializationVector;
        public CryptoManager(string secretKey, string initializationVector)
            => (_secretKey, _initializationVector) = GetKeys(secretKey, initializationVector);

        public string EncryptText(string PlainText)
        {
            using (Aes aes = Aes.Create())
            {
                aes.Key = _secretKey;
                aes.IV = _initializationVector;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        using (var streamWriter = new StreamWriter(cryptoStream))
                        {
                            streamWriter.Write(PlainText);
                        }
                    }
                    return Convert.ToBase64String(memoryStream.ToArray());
                }
            }
        }

        public string DecryptText(string EncryptedText)
        {
            byte[] buffer = Convert.FromBase64String(EncryptedText);

            using (Aes aes = Aes.Create())
            {
                aes.Key = _secretKey;
                aes.IV = _initializationVector;
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var memoryStream = new MemoryStream(buffer))
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read))
                    {
                        using (var streamReader = new StreamReader(cryptoStream))
                        {
                            return streamReader.ReadToEnd();
                        }
                    }
                }
            }
        }

        private static (byte[] secretKey, byte[] initializationVector) GetKeys(string secretKey, string initializationVector)
        {
            _ = secretKey ?? throw new ArgumentNullException(nameof(secretKey));

            if (string.IsNullOrEmpty(secretKey))
                throw new ArgumentNullException(nameof(secretKey));

            _ = initializationVector ?? throw new ArgumentNullException(nameof(initializationVector));

            if (string.IsNullOrEmpty(initializationVector))
                throw new ArgumentNullException(nameof(initializationVector));

            var tmpSecretKey = Encoding.UTF8.GetBytes(secretKey);

            if (tmpSecretKey.Length % 2 != 0)
                throw new Exception($"String length of {nameof(secretKey)} must be multiple of 2.");

            var tmpInitializationVector = Encoding.UTF8.GetBytes(initializationVector);

            if (tmpInitializationVector.Length != 16)
                throw new Exception($"String length of {nameof(initializationVector)} must be 16.");

            return (tmpSecretKey, tmpInitializationVector);
        }
    }
}
