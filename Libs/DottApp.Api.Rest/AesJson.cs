using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using DottApp.RsaAesWrapper;

namespace DottApp.Api.Rest
{
    public static class AesJson
    {
        public static string Serialize<TObj >(TObj obj, AESw aesw) => aesw.Encrypt(JsonSerializer.Serialize(obj));
        public static TObj Deserialize<TObj>(string aesText, AESw aesw)  where TObj : class
        {
            var jsonText = aesw.Decrypt(aesText);
            try
            {
                var jsonBody = JsonSerializer.Deserialize<TObj>(jsonText);
                return jsonBody;
            }
            catch (Exception)
            {
                return null;
            }
        } 
    }
}
