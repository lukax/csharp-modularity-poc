#region Usings

using System;
using System.Windows.Input;

#endregion

namespace LOB.UI.Core.Command
{
    public class DelegateCommand : ICommand
    {
        private readonly Predicate<Object> _canExecute;
        private readonly Action<Object> _execute;

        public DelegateCommand(Action<Object> execute, Predicate<Object> canExecute = null) {
            _execute = execute;
            _canExecute = canExecute;
        }

        public void Execute(object parameter) {
            _execute(parameter);
        }

        public bool CanExecute(object parameter) {
            if (_canExecute != null)
                return _canExecute(parameter);
            return true;
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}