using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Input;
using DottApp.ApiWrapper;
using DottApp.Client.Infrastructure.Commands;
using DottApp.Client.Properties;
using DottApp.Client.ViewModels.Base;
using DottApp.Client.Views.Windows;
using DottApp.RsaAesWrapper;
using DottApp.Services.Auth;


namespace DottApp.Client.ViewModels
{
    internal class ProgramLoaderViewModel : ViewModel  
    {
        #region ProgressBarValue

        private double _progressBarValue;

        public double ProgressBarValue
        {
            get => _progressBarValue;
            set => Set(ref _progressBarValue, value);
        }

        #endregion

        #region LoaderWindow

        private ProgramLoaderWindow _programLoaderWindow;

        public ProgramLoaderWindow LoaderWindow
        {
            get => _programLoaderWindow;
            set => Set(ref _programLoaderWindow, value);
        }
        

        #endregion

        #region Commands

        public ICommand OpenWindowCommand { get; }

        private void OnOpenWindowCommandExecuted(object param)
        {
            if (param is null) return;


            //    DaAPIw.BaseUrl = ConfigurationManager.AppSettings["BaseDebugApiUrl"];
            //    DaAPIw.Connect();
            //    var nick = new Random().Next(0, 100).ToString();
            ////    DaAPIw.SignIn(nick = "98", "12345");
            //   DaAPIw.Registration(nick, "nick", "12345");
            //    MessageBox.Show($"{nick} {DaAPIw.IsAuth.ToString()} {ProtectedStorage.AccessToken}");



            switch (Convert.ToInt32(param))
            {
                case 1: { new MainWindow().Show(); break; }
                case 2: { new DebugWindow().Show(); break; }
                case 3: { new TestWindow().Show(); break; }
                case 4: { new AuthWindow().Show(); break; }
            }
        }

        private bool CanOpenWindowCommandExecute(object param) => true;



        #endregion

        public ProgramLoaderViewModel()
        {
            #region Commands initialization
            OpenWindowCommand = new LambdaCommand(OnOpenWindowCommandExecuted, CanOpenWindowCommandExecute);
            

            #endregion
        }

    }
}
