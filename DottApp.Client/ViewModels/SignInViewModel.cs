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
    internal class SignInViewModel : ViewModel
    {
        #region Authorization

        private string _Login;
        public string Login
        {
            get => _Login;
            set => Set(ref _Login, value);
        }
        //Что SecureString, что обычная строка - та же ошибка
        private SecureString _Password;
        public SecureString Password
        {
            get => _Password;
            set => Set(ref _Password, value);
        }
        private string _Password2;
        public string Password2
        {
            get => _Password2;
            set => Set(ref _Password2, value);
        }

        #endregion

        #region LoaderWindow

        private SignInWindow _programLoaderWindow;

        public SignInWindow LoaderWindow
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
            switch (Convert.ToInt32(param))
            {
                case 1: { new MainWindow().Show(); break; }
            }
        }

        private bool CanOpenWindowCommandExecute(object param) => true;

        public ICommand SigninCommand { get; set; }
        private void Signin(object param)
        {
            var passwordBox = param as PasswordBox;
            if (passwordBox == null)
                return;
            var password = passwordBox.Password;
            Console.WriteLine(password);//_Password = password;
}

        #endregion

        public SignInViewModel()
        {
            #region Commands initialization
            OpenWindowCommand = new LambdaCommand(OnOpenWindowCommandExecuted, CanOpenWindowCommandExecute);


            #endregion
        }
    }
}
