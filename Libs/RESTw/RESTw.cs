using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using DottApp.Services.Auth;
using RestSharp;

namespace DottApp.RestWrapper
{
    
    public class RESTw
    {
        private string baseUrl;
        private RestClient client;
        
        public RESTw(string baseUrl)
        {
            this.baseUrl = baseUrl;
            client = new RestClient(baseUrl);
            client.RemoteCertificateValidationCallback = (sender, ser, chain, sslPolicyErrors) => true;
        }


        public IRestResponse Post<TRequestType>(string resoure, TRequestType body)
        {
            var req = new RestRequest(resoure); //"WeatherForecast/Connect"
            req.AddJsonBody(body);
            var resp = client.Post(req);
            
            return resp;
        }

        public TResponse Post<TRequest, TResponse>(string resoure, TRequest body) 
        {
            var req = new RestRequest(resoure); //"WeatherForecast/Connect"
            req.AddJsonBody(body);
            var resp = client.Post(req);
            return resp.IsSuccessful ? JsonSerializer.Deserialize<TResponse>(resp.Content) : 
                    throw new DAException(DAExceptionType.UnknownError, resp.ErrorMessage);
        }
    }
}
