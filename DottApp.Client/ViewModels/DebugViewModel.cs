using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;
using DottApp.Client.Infrastructure.Commands;
using DottApp.Client.ViewModels.Base;
using DottApp.Client.Views.Windows;
using DottApp.RsaAesWrapper;
using DottApp.Services.Auth;
using Flurl.Http;
using RestSharp;

namespace DottApp.Client.ViewModels
{
    class DebugViewModel : ViewModel    
    {

        #region Fields

        #region Api

        #region Connect



        #region Api.Connect.RequestText : string
        private string _apiConnectRequestText;

        public string ApiConnectRequestText
        {
            get => _apiConnectRequestText;
            set => Set(ref _apiConnectRequestText, value);
        }
        #endregion




        #region Api.Connect.ResponseText : string
        private string _apiConnectResponseText;

        public string ApiConnectResponseText
        {
            get => _apiConnectResponseText;
            set => Set(ref _apiConnectResponseText, value);
        }
        #endregion


        #endregion

        #endregion


        #endregion


        #region Commands

        #region Api.Connect

        public ICommand GenerateRequestCommand { get; }

        private void OnGenerateRequestCommandExecuted(object param)
        {
            RSAw rsaw = new RSAw();
            ConnectionSessionRequest csr = new ConnectionSessionRequest()
            {
                IsFirstTime = true,
                PublicKey = new RSAByteKey().setKeyFromParameters(rsaw.PublicKey)
            };

            ApiConnectRequestText = JsonSerializer.Serialize(csr);
        }

        private bool CanGenerateRequestCommandExecute(object param) => true;



        public ICommand SendRequestCommand { get; }

        private void OnSendRequestCommandExecuted(object param)
        {
             HttpClient clientaHttpClient = new HttpClient();
             RestClient client = new RestClient("http://api.dottapp.nrtu.studio");
            var req = new RestRequest("/Weatherforecast/Connect");
            req.AddJsonBody(JsonSerializer.Deserialize<ConnectionSessionRequest>(ApiConnectRequestText));
            
            var resp = client.Post(req);
            
            ApiConnectResponseText =   resp.Content;
            MessageBox.Show("done");
        }

        private bool CanSendRequestCommandExecute(object param) => true;


        #endregion

        #endregion

        public DebugViewModel()
        {
            #region command init

            GenerateRequestCommand = new LambdaCommand(OnGenerateRequestCommandExecuted, CanGenerateRequestCommandExecute);
            SendRequestCommand = new LambdaCommand(OnSendRequestCommandExecuted, CanSendRequestCommandExecute);

            #endregion
        }
    }
}
