using System;
using System.Net.Http;
using System.Security;
using System.Security.Cryptography;
using System.Text.Json;
using DottApp.Api.Rest.Request_Response;
using DottApp.RsaAesWrapper;
using DottApp.Services.Auth;
using RestSharp;

namespace DottApp.ApiWrapper
{
    public static class DaAPIw
    {
        private static RestClient _client;

        private static string _baseUrl;
        private static string _sessionId;

        public static bool IsConnected { get; private set; }
        public static bool IsAuth { get; private set; }

        public static string BaseUrl
        {
            get => _baseUrl;
            set
            {
                _baseUrl = value;
                _client = new RestClient(_baseUrl);
                _client.RemoteCertificateValidationCallback = (sender, ser, chain, sslPolicyErrors) => true;
            }
        }


        static DaAPIw() => BaseUrl = "https://api.dottapp.nrtu.studio";

        public static bool? IsLoginFree( string login)
        {
            RestRequest request = new RestRequest("api/Auth/IsLoginFree");
            request.AddParameter("login", login);
            var response = _client.Get(request);
            if (response.IsSuccessful == false) return null;
            try
            {
                var res = Convert.ToBoolean(response.Content);
                return res;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        [SecurityCritical]
        public static bool? Connect()
        {
            RestRequest request = new RestRequest("api/Auth/Connect");
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            request.AddJsonBody(new ConnectionSessionRequest()
            {
                Key = RSAKeys.ExportPublicKey(rsa)
            });
            var response = _client.Post(request);
            try
            {
                var res = JsonSerializer.Deserialize<ConnectionSessionResponse>(response.Content);
                _sessionId = res.SessionId;
                ProtectedStorage.AesKey = rsa.Decrypt(Convert.FromBase64String(res.Key), false);
                return  IsConnected = true;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
       // [SecurityCritical]
        public static bool? Registration( string login,  string nick,  string pass)
        {
            var isValidLogin = IsLoginFree(login);
            if (isValidLogin is null) return null;
            if (isValidLogin == false) return false;
            RestRequest request = new RestRequest("api/Auth/SignUp");
            AESw aesw = new AESw(ProtectedStorage.AesKey);
            request.AddParameter("sid", _sessionId, ParameterType.QueryString);
            request.AddJsonBody(new RegistrationRequest()
            {
                LoginName = aesw.Encrypt(login), NickName = aesw.Encrypt(nick), Password = aesw.Encrypt(pass)
            });
            var response = _client.Post(request);
            try
            {
                var res = JsonSerializer.Deserialize<RegistrationResponse>(response.Content);
                ProtectedStorage.AccessToken = aesw.Decrypt(res.AccessToken);
                return IsAuth = true;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static bool? SignIn(string login, string pass)
        {
            var isLoginFree = IsLoginFree(login);
            if (isLoginFree is null) return null;
            if (isLoginFree == true) return false;
            RestRequest request = new RestRequest("api/Auth/SignIn");
            AESw aesw = new AESw(ProtectedStorage.AesKey);
            request.AddParameter("sid", _sessionId, ParameterType.QueryString);
            request.AddJsonBody(new SigninRequest()
            {
                LoginName = aesw.Encrypt(login),
                Password = aesw.Encrypt(pass)
            });
            var response = _client.Post(request);
            try
            {
                var res = JsonSerializer.Deserialize<SigninResponse>(response.Content);
                ProtectedStorage.AccessToken = aesw.Decrypt(res.AccessToken);
                return IsAuth = true;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}