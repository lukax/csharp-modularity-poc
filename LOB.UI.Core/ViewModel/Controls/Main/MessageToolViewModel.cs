#region Usings

using System;
using System.ComponentModel.Composition;
using System.Windows.Input;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Prism.Commands;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    [Export(typeof(IMessageToolViewModel))]
    public class MessageToolViewModel : BaseViewModel, IMessageToolViewModel {
        //[Import] private IEventAggregator EventAggregatorLazy { get; set; }
        public string Message { get; set; }
        public bool IsRestrictive { get; set; }
        private ICommand _closeCommand;
        public bool CanClose { get; set; }

        public MessageToolViewModel() { Message = "Please wait..."; }

        public void CloseExecute() {
            //_eventAggregator.GetEvent<MessageHideEvent>().Publish(null);
        }
        public ICommand CloseCommand {
            get { return _closeCommand ?? (_closeCommand = new DelegateCommand(CloseExecute, () => CanClose)); }
            set { _closeCommand = value; }
        }

        private ViewModelInfo _viewModelInfo = new ViewModelInfo {ViewState = ViewState.Other};

        public override ViewModelInfo Info {
            get { return _viewModelInfo; }
            set { _viewModelInfo = value; }
        }

        public void Initialize(string message, bool canClose, bool isRestrictive) {
            Message = message;
            CanClose = canClose;
            IsRestrictive = isRestrictive;
        }

        public override void InitializeServices() { }

        public override void Refresh() { }
        #region Implementation of IDisposable

        public override void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}