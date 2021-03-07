using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography;
using System.Text;
using DottApp.Services.Auth;

namespace DottApp.RsaAesWrapper
{
    public class AESw
    {
        private readonly Aes _aes;
        private byte[] _key;
        public byte[] Key { get=>_key; }
        public AESw()
        {
            _aes = Aes.Create();
            _aes.GenerateKey();
            _key = _aes.Key;
        }

        public AESw(byte[] key)
        {
            _aes = Aes.Create();
            _aes.Key = _key = key;
        }

        #region Encrypt
        /// <summary>Encrypt string via AES</summary>
        /// <param name="data">Plain text</param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string data) => Encrypt(data, _key);

        /// <summary>Encrypt string via RSA  </summary>
        /// <param name="data">Plain text</param>
        /// <param name="publicKey">Public RSA key</param>
        /// <returns>Encrypted string</returns>
        public string Encrypt(string data, byte[] key)
        {
            _aes.Key = key;
            byte[] encrypted;
            ICryptoTransform crypt = _aes.CreateEncryptor(_aes.Key, new byte[16]);
            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Write))
                {
                    using (StreamWriter sw = new StreamWriter(cs))
                    {
                        sw.Write(data);
                    }
                }
                encrypted = ms.ToArray();
            }

            _aes.GenerateKey();
            //encrypted.Concat(_aes.IV).ToArray()
            return Convert.ToBase64String(encrypted.ToArray());
        }

        #endregion

        #region Decrypt
        /// <summary>Decrypt string via RSA</summary>
        /// <param name="data">Encrypted text</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string data) => Decrypt(data, _key);

        /// <summary>Decrypt string via Aes</summary>
        /// <param name="data">Encrypted text</param>
        /// <param name="privateKey">Private RSA key</param>
        /// <returns>Decrypted string</returns>
        public string Decrypt(string data, byte[] key)
        {
            _aes.Key = key;
            string text = string.Empty;
            ICryptoTransform crypt = _aes.CreateDecryptor(_aes.Key, new byte[16]);
            using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(data)))
            {
                using (CryptoStream cs = new CryptoStream(ms, crypt, CryptoStreamMode.Read))
                {
                    using (StreamReader sr = new StreamReader(cs))
                    {
                        text = sr.ReadToEnd();
                    }
                }
            }
            _aes.GenerateKey();
            return text;
        }

        #endregion

    }
}