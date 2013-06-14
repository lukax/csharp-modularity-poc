#region Usings

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

#endregion

namespace LOB.UI.Contract.Command {
    public class RelayDelegateCommand : ICommand {
        private readonly bool _sharedExecute;
        private readonly bool _sharedCanExecute;
        private static readonly IList<RelayDelegate> SharedDelegates = new List<RelayDelegate>();
        private readonly RelayDelegate _thisRelayDelegate = new RelayDelegate();

        public RelayDelegateCommand(Guid id, Action<Object> execute, Predicate<Object> canExecute = null, bool sharedExecute = false,
            bool sharedCanExecute = false) {
            _sharedExecute = sharedExecute;
            _sharedCanExecute = sharedCanExecute;
            _thisRelayDelegate.Id = id;
            _thisRelayDelegate.Execute = execute;
            _thisRelayDelegate.CanExecute = canExecute;
            SharedDelegates.Add(_thisRelayDelegate);
        }

        public void Execute(object parameter) {
            if(_sharedExecute) foreach(var source in SharedDelegates.Where(x => x.Id == _thisRelayDelegate.Id)) source.Execute(parameter);
            else _thisRelayDelegate.Execute(parameter);
        }

        public bool CanExecute(object parameter) {
            return _sharedCanExecute
                       ? SharedDelegates.Where(x => x.Id == _thisRelayDelegate.Id).All(result => result.CanExecute(parameter))
                       : _thisRelayDelegate.CanExecute(parameter);
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        private class RelayDelegate {
            public Guid Id { get; set; }
            public Predicate<object> CanExecute { get; set; }
            public Action<object> Execute { get; set; }
        }
    }
}