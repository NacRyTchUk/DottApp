using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using DottApp.ApiWrapper;
using DottApp.Client.Infrastructure.Commands;
using DottApp.Client.ViewModels.Base;
using DottApp.Client.Views.Windows;

namespace DottApp.Client.ViewModels
{
    internal class AuthViewModel : ViewModel
    {
        #region Authorization

        private string _Login;
        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }

        private string _RegLogin;
        public string RegLogin
        {
            get => _RegLogin;
            set => Set(ref _RegLogin, value);
        }

        private string _NickName;
        public string NickName
        {
            get => _NickName;
            set => Set(ref _NickName, value);
        }

        private string _SelectedTabNum;
        public string SelectedTabNum
        {
            get => _SelectedTabNum;
            set => Set(ref _SelectedTabNum, value);
        }

        private string _InPassword;
        public string InPassword
        {
            get => _InPassword;
            set => Set(ref _InPassword, value);
        }
        private string _InRegPassword;
        public string InRegPassword
        {
            get
            {
                EqualPasswordsColor = Equals(_InRegPassword, _InRegConfirmPassword) ? System.Windows.Media.Brushes.Green :
                   System.Windows.Media.Brushes.Red;
                return _InRegPassword;
            }
            set => Set(ref _InRegPassword, value);
        }

        private string _InRegConfirmPassword;
        public string InRegConfirmPassword
        {
            get
            {
                EqualPasswordsColor = Equals(_InRegPassword, _InRegConfirmPassword) ? System.Windows.Media.Brushes.Green :
                   System.Windows.Media.Brushes.Red;
                return _InRegConfirmPassword;
            }
            set =>
                Set(ref _InRegConfirmPassword, value);
        }

        private System.Windows.Media.Brush _EqualPasswordsColor = System.Windows.Media.Brushes.Red;

        public System.Windows.Media.Brush EqualPasswordsColor
        {
            get => _EqualPasswordsColor;
            set => Set(ref _EqualPasswordsColor, value);
        }
        #endregion


        #region Commands

        public ICommand SelectTabCommand { get; }

        private void OnSelectTabCommandExecuted(object param)
        {
            if (param is null) return;
            SelectedTabNum = Convert.ToString(param);
        }

        private bool CanSelectTabCommandExecute(object param) => true;


        public ICommand SignUpCommand { get; }

        private void OnSignUpCommandExecuted(object param)
        {
            DaAPIw.BaseUrl = ConfigurationManager.AppSettings["BaseDebugApiUrl"];
            DaAPIw.Connect();
            DaAPIw.Registration(RegLogin, NickName, InRegPassword);
            MessageBox.Show(DaAPIw.IsAuth ? "Success!" : "Oh oh....");
        }

        private bool CanSignUpCommandExecute(object param) => true;


        public ICommand SignInCommand { get; }

        private void OnSignInCommandExecuted(object param)
        {
            DaAPIw.BaseUrl = ConfigurationManager.AppSettings["BaseDebugApiUrl"];
            DaAPIw.Connect();
            DaAPIw.SignIn(Login, InPassword);
            MessageBox.Show(DaAPIw.IsAuth ? "Success!" : "Oh oh....");
        }

        private bool CanSignInCommandExecute(object param) => true;

        #endregion

        public AuthViewModel()
        {
            #region Commands initialization
            SelectTabCommand = new LambdaCommand(OnSelectTabCommandExecuted, CanSelectTabCommandExecute);
            SignUpCommand = new LambdaCommand(OnSignUpCommandExecuted, CanSignUpCommandExecute);
            SignInCommand = new LambdaCommand(OnSignInCommandExecuted, CanSignInCommandExecute);
            #endregion
        }
    }
}
