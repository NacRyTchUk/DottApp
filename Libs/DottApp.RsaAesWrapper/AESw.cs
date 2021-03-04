using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;

namespace DottApp.RsaAesWrapper
{
    public class AESw
    {
        private readonly Aes _aes;
        public string Key { get; }
        public AESw()
        {
           
        }

        #region Encrypt
        /// <summary>Encrypt string via AES</summary>
        /// <param name="data">Plain text</param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string data) => Encrypt(data, Key);

        /// <summary>Encrypt string via RSA  </summary>
        /// <param name="data">Plain text</param>
        /// <param name="publicKey">Public RSA key</param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string data, string publicKey)
        {
            return string.Empty;
        }

        #endregion

        #region Decrypt
        /// <summary>Decrypt string via RSA</summary>
        /// <param name="data">Encrypted text</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string data) => Decrypt(data, Key);

        /// <summary>Decrypt string via Aes</summary>
        /// <param name="data">Encrypted text</param>
        /// <param name="privateKey">Private RSA key</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string data, string privateKey)
        {
            return string.Empty;
        }

        #endregion

    }
}