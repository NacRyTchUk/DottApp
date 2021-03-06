using System;
using System.Net.Http;
using DottApp.Services.Auth;
using RestSharp;

namespace DottApp.ApiWrapper
{
    public static class DaAPIw
    {
        private static string _baseUrl;
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

        private static RestClient _client;

        static DaAPIw() => BaseUrl = "http://api.dottapp.nrtu.studio";

        public static bool? IsLoginFree(ref string login)
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

        public static bool? Connect()
        {
            RestRequest request = new RestRequest("api/Auth/Connect");
            request.AddJsonBody(new ConnectionSessionRequest()
            {

            });
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

        public static bool? Registration(ref string login, ref string nick, ref string passHash)
        {
            return null;
        }
    }
}
