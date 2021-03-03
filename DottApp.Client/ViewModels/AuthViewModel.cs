using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Security;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        
        public SecureString SecureInPassword { private get; set; }
        public SecureString SecureRegPassword { private get; set; }
        public SecureString SecureRegConfirmPassword { private get; set; }
        #endregion


        #region Commands

        

        public ICommand SelectTabCommand { get; }

        private void OnSelectTabCommandExecuted(object param)
        {
            if (param is null) return;
            SelectedTabNum = Convert.ToString(param);
        }

        private bool CanSelectTabCommandExecute(object param) => true;

        #endregion

        public AuthViewModel()
        {
            #region Commands initialization
            SelectTabCommand = new LambdaCommand(OnSelectTabCommandExecuted, CanSelectTabCommandExecute);

            #endregion
        }
    }
}
