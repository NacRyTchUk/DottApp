using System;
using System.Security.Cryptography;
using System.Text.Json;
using System.Text.Json.Serialization;
using Org.BouncyCastle.Security;

namespace DottApp.Services.Auth
{
    //public class RSAByteKey
    //{
    //    [JsonPropertyName("exponent")]
    //    public string Exponent { get; set; } 
    //    [JsonPropertyName("module")]
    //    public string Module { get; set; }

    //    public RSAByteKey SetKeyFromParameters(RSAParameters key)
    //    {
    //        Exponent = Convert.ToBase64String(key.Exponent);
    //        Module = Convert.ToBase64String(key.Modulus);
    //        return this;
    //    }

    //    public RSAParameters GetRSAParameters() => new RSAParameters()
    //    {
    //        Exponent = Convert.FromBase64String(this.Exponent),
    //        Modulus = Convert.FromBase64String(this.Module)
    //    };
    //}

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
            var rnd = new SecureRandom();
            for (int i = 0; i < TokenLength; i++) 
                _accessToken += SymbolTable[rnd.Next(0, SymbolTable.Length)];
            return _accessToken;
        }
    }


    public class ConnectionSessionRequest
    {
        [JsonPropertyName("publicKey")]
        public string PublicKey { get; set;  }
        [JsonPropertyName("isFirstTime")]
        public bool IsFirstTime { get; set; } 
    }

    public class ConnectionSessionResponse : ConnectionSessionRequest
    {
        [JsonPropertyName("accessToken")]
        public string AccessToken { get; set; }
        [JsonPropertyName("sessionId")]
        public string SessionId { get; set; }
    }
}
