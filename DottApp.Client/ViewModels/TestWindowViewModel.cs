using System;
using System.Collections.Generic;
using System.Text;
using DottApp.Client.ViewModels.Base;
using DottApp.Client.Views.Windows;
using DottApp.Client.Infrastructure.Commands;
using System.Windows.Input;
using System.Windows;

namespace DottApp.Client.ViewModels
{
    #region Тект
    internal class TestWindowViewModel : ViewModel
    {
        private string _Text = "Введи сюда что-нибудь";
        public string Text
        {
            get => _Text;
            set => Set(ref _Text, value);
        }
        #endregion

        #region Commands

        public ICommand ShowMessageCommand { get; }

        private void OnShowMessageCommandExecuted(object param = null)
        {
            MessageBox.Show(Text);
        }

        private bool CanShowMessageCommandExecute(object param = null) => true;


        #endregion

        public TestWindowViewModel()
        {
            #region Commands initialization
            ShowMessageCommand = new LambdaCommand(OnShowMessageCommandExecuted, CanShowMessageCommandExecute);


            #endregion
        }
    }
}
