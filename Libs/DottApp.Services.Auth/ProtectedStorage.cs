using System;
using System.Collections.Generic;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace DottApp.Services.Auth
{
    [SecurityCritical]
    public static class ProtectedStorage
    {
        private static byte[] _p_data;
        private static readonly byte[] _enthropy = Encoding.UTF8.GetBytes("api.dottapp.nrtu.studio");
        public static PrivateData Get() => JsonSerializer.Deserialize<PrivateData>(
            Encoding.UTF8.GetString(ProtectedData.
                Unprotect(_p_data, _enthropy, DataProtectionScope.CurrentUser)));

        public static void Set(PrivateData protectedData) => _p_data = ProtectedData.Protect(
            Encoding.UTF8.GetBytes(
                JsonSerializer.Serialize(protectedData)), _enthropy, DataProtectionScope.CurrentUser);

        public static byte[] AesKey
        {
            get => Get().AesKey;
            set => Set(new PrivateData { AesKey = value, AccessToken = Get().AccessToken });
        }

        public static string AccessToken
        {
            get => Get().AccessToken;
            set => Set(new PrivateData { AesKey = Get().AesKey, AccessToken = value });
        }
    }

    [SecurityCritical]
    public class PrivateData
    {
        public string AccessToken { get; set; }
        public byte[] AesKey { get; set; }
    };



}
