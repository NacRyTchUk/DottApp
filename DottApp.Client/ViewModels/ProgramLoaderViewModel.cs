using System;
using System.Collections.Generic;
using System.Text;
using DottApp.Client.ViewModels.Base;

namespace DottApp.Client.ViewModels
{
    internal class ProgramLoaderViewModel : ViewModel  
    {
        #region ProgressBarValue

        private string _progressBarValue;

        public string ProgressBarValue
        {
            get => _progressBarValue;
            set => Set(ref _progressBarValue, value);
        }

        #endregion

    }
}
