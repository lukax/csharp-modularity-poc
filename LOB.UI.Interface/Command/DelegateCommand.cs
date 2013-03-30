#region Usings
using System;
using System.Windows.Input;

#endregion

namespace LOB.UI.Interface.Command {
    public class DelegateCommand : ICommand {

        private readonly Predicate<object> _canExecute;
        private readonly Action<Object> _execute;

        public DelegateCommand(Action<Object> execute, Predicate<Object> canExecute = null) {
            this._execute = execute;
            this._canExecute = canExecute;
        }

        public void Execute(object parameter) {
            this._execute(parameter);
        }

        public bool CanExecute(object parameter) {
            if(this._canExecute != null) return this._canExecute(parameter);
            return true;
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

    }
}