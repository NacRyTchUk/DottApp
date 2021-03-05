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

        private bool _EqualPasswords;
        public bool EqualPasswords
        {
            get => Equals(_InRegPassword, _InRegConfirmPassword);
            set => Set(ref _EqualPasswords, value);
        }

        //private MaterialDesignColors.MaterialDesignColor _EqualPasswordsColor = MaterialDesignColors.MaterialDesignColor.Green;

        //public MaterialDesignColors.MaterialDesignColor EqualPasswordsColor
        //{
        //    get => _EqualPasswordsColor;
        //    set => Set(ref _EqualPasswordsColor, value);
        //}

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

        public ICommand PasswordTextChanged { get; }

        //private void OnPasswordTextChangedExecuted(object param = null)
        //{
        //    _EqualPasswordsColor = Equals(_InRegPassword, _InRegConfirmPassword) ? MaterialDesignColors.MaterialDesignColor.Green :
        //        MaterialDesignColors.MaterialDesignColor.Red;
        //}

        //private bool CanPasswordTextChangedExecute(object param = null) => true;

        #endregion

        public AuthViewModel()
        {
            #region Commands initialization
            SelectTabCommand = new LambdaCommand(OnSelectTabCommandExecuted, CanSelectTabCommandExecute);
            //SelectTabCommand = new LambdaCommand(OnPasswordTextChangedExecuted, CanPasswordTextChangedExecute);
            #endregion
        }
    }
}
