using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DottApp.Client.Infrastructure.Commands.Base
{
    abstract class  BaseCommand : ICommand
    {
        public abstract bool CanExecute(object parameter);

        public abstract void Execute(object parameter);

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
