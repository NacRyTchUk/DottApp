﻿using System;
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
        RSAw rsaw = new RSAw();

        public ICommand GenerateRequestCommand { get; }

        private void OnGenerateRequestCommandExecuted(object param)
        {
            ConnectionSessionRequest csr = new ConnectionSessionRequest()
            {
                IsFirstTime = true,
                PublicKey = new RSAByteKey().SetKeyFromParameters(rsaw.PublicKey)
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


            RestClient client = new RestClient(baseUrl);
            client.RemoteCertificateValidationCallback = (sender, ser, chain, sslPolicyErrors) => true;
            var req = new RestRequest("api/Auth/NewConnectSession");
            req.AddHeader("Content-type", "application/json");
            req.AddJsonBody(JsonSerializer.Deserialize<ConnectionSessionRequest>(ApiConnectRequestText));
            var resp = client.Post(req);
            ApiConnectResponseText = resp.Content;

            //RESTw restw = new RESTw(baseUrl);
            //var response = restw.Post<ConnectionSessionRequest>
            //("/api/Auth/NewConnectSession",
            //    JsonSerializer.Deserialize<ConnectionSessionRequest>(ApiConnectRequestText));
            //ApiConnectResponseText = response.Content;



            var respVal = JsonSerializer.Deserialize<ConnectionSessionResponse>(resp.Content);
            var reqVal = JsonSerializer.Deserialize<ConnectionSessionRequest>(ApiConnectRequestText);

            MessageBox.Show(rsaw.Decrypt(respVal.SessionId));




            //var jSonData = JsonConvert.SerializeObject(customObj);

            //using (var httpClient = new HttpClient())
            //{
            //    httpClient.BaseAddress = new Uri("https://www.testapi.com");

            //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //    using (var response =
            //        await httpClient.PostAsync(url, new StringContent(jSonData, Encoding.UTF8, "application/json")))
            //    {
            //        using (var content = response.Content)
            //        {
            //            var result = await content.ReadAsStringAsync();
            //        }
            //    }
            //}
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
