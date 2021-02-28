using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Security;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows;
using System.Windows.Input;
using DottApp.Client.Infrastructure.Commands;
using DottApp.Client.Properties;
using DottApp.Client.ViewModels.Base;
using DottApp.Client.Views.Windows;
using DottApp.RestWrapper;
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
            RSACryptoServiceProvider rsaw = new RSACryptoServiceProvider();

            Settings.Default["prKey"] = RSAKeys.ExportPrivateKey(rsaw);

            ConnectionSessionRequest csr = new ConnectionSessionRequest()
            {
                IsFirstTime = true,
                PublicKey = RSAKeys.ExportPublicKey(rsaw)
            };

            ApiConnectRequestText = JsonSerializer.Serialize(csr);
        }

        private bool CanGenerateRequestCommandExecute(object param) => true;



        public ICommand SendRequestCommand { get; }

        private void OnSendRequestCommandExecuted(object param)
        {


            string baseUrl = ConfigurationManager.AppSettings["BaseApiUrl"];
            #if DEBUG
                baseUrl = ConfigurationManager.AppSettings["BaseDebugApiUrl"];
            #endif


            RESTw restw = new RESTw(baseUrl);
            var response = restw.Post<ConnectionSessionRequest>
            ("api/Auth/NewConnectSession",
                JsonSerializer.Deserialize<ConnectionSessionRequest>(ApiConnectRequestText));
            ApiConnectResponseText = response.Content;



            var respVal = JsonSerializer.Deserialize<ConnectionSessionResponse>(response.Content);
           // var reqVal = JsonSerializer.Deserialize<ConnectionSessionRequest>(ApiConnectRequestText);
           var sraw = RSAKeys.ImportPrivateKey(Settings.Default["prKey"].ToString());
           MessageBox.Show(Encoding.UTF8.GetString(sraw.Decrypt(Convert.FromBase64String(respVal.AccessToken), false)));

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
