#region Usings

using System;
using System.Windows.Input;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public class MessageToolViewModel : BaseViewModel, IMessageToolViewModel {

        private readonly IUnityContainer _container;
        private readonly IEventAggregator _eventAggregator;
        #region Props

        public string Message { get; set; }

        public bool IsRestrictive { get; set; }

        #endregion Message
        #region CloseExecute Command

        private ICommand _closeCommand;

        public ICommand CloseCommand { get { return _closeCommand ?? (_closeCommand = new DelegateCommand(CloseExecute, () => CanClose)); } set { _closeCommand = value; } }

        public bool CanClose { get; set; }

        public void CloseExecute() {
            //_eventAggregator.GetEvent<MessageHideEvent>().Publish(null);
        }

        #endregion CloseExecute Command
        [InjectionConstructor]
        public MessageToolViewModel(IUnityContainer container) {
            Message = "Please wait...";
            _container = container;
            _eventAggregator = _container.Resolve<IEventAggregator>();
        }

        private UIOperation _operation = new UIOperation {Type = UIOperationType.MessageTool, State = UIOperationState.Internal};

        public override UIOperation Operation { get { return _operation; } set { _operation = value; } }

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