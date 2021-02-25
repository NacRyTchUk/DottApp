using System;
using System.Security.Cryptography;
using System.Text;

namespace DottApp.RsaAesWrapper
{
    public class RSAw
    {
        private readonly RSACryptoServiceProvider _rsa;
        public  RSAParameters PublicKey { get; }
        public  RSAParameters PrivateKey { get; }
        public RSAw(Int32 keyLength)
        {
            _rsa = new RSACryptoServiceProvider(keyLength);
            PublicKey = _rsa.ExportParameters(false);
            PrivateKey = _rsa.ExportParameters(true);
        }

        #region Encrypt
        /// <summary>Encrypt string via RSA</summary>
        /// <param name="data">Plain text</param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string data) => Encrypt(data, PublicKey);

        /// <summary>Encrypt string via RSA  </summary>
        /// <param name="data">Plain text</param>
        /// <param name="publicKey">Public RSA key</param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string data, RSAParameters publicKey)
        {
            var encode = new UnicodeEncoding();
            _rsa.ImportParameters(publicKey);
            var encryptedData = _rsa.Encrypt(encode.GetBytes(data), false);
            return encode.GetString(encryptedData);
        }

        #endregion

        #region Decrypt
        /// <summary>Decrypt string via RSA</summary>
        /// <param name="data">Encrypted text</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string data) => Decrypt(data, PrivateKey);

        /// <summary>Decrypt string via RSA</summary>
        /// <param name="data">Encrypted text</param>
        /// <param name="privateKey">Private RSA key</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string data, RSAParameters privateKey)
        {
            var encode = new UnicodeEncoding();
            _rsa.ImportParameters(privateKey);
            var encryptedData = _rsa.Encrypt(encode.GetBytes(data), false);
            return encode.GetString(encryptedData);
        }

        #endregion

    }
}
