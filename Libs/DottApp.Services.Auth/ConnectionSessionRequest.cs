using System;
using System.Security.Cryptography;
using System.Text.Json;

namespace DottApp.Services.Auth
{
    public class RSAByteKey
    {
        public byte[] Exponent { get; set; }
        public byte[] Module { get; set; }
        public RSAByteKey(RSAParameters key)
        {
            Exponent = key.Exponent;
            Module = key.Modulus;
        }

        public RSAParameters getRSAParameters() => new RSAParameters()
        {
            Exponent = this.Exponent,
            Modulus = this.Module
        };
    }

    public class AccessToken
    {
        private const int TokenLength = 64;
        private const string  SymbolTable = 
            "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private string _accessToken;

        public AccessToken() => GenNew();
        public string Get() => _accessToken ?? GenNew();

        public string GenNew()
        {
            _accessToken = string.Empty;
            var rnd = new Random(DateTime.Now.Millisecond);
            for (int i = 0; i < TokenLength; i++) 
                _accessToken += SymbolTable[rnd.Next(0, SymbolTable.Length)];
            return _accessToken;
        }
    }


    public class ConnectionSessionRequest
    {
        public RSAByteKey PublicKey { get; set;  }
        public bool IsFirstTime { get; set; } 
    }

    public class ConnectionSessionResponse : ConnectionSessionRequest
    {
        public string AccessToken { get; set; }
        public string SessionId { get; set; }
    }
}
