#region Usings

using System;
using System.Windows.Input;
using LOB.UI.Core.Event;
using LOB.UI.Core.ViewModel.Base;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Main
{
    public class MessageToolsViewModel : BaseViewModel
    {
        private IUnityContainer container = null;
        private IEventAggregator eventAggregator = null;

        #region Message

        private string _message = "Please wait...";

        public string Message
        {
            get { return _message; }

            set
            {
                _message = value;
                OnPropertyChanged();
            }
        }

        #endregion Message

        #region Close Command

        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get { return this._closeCommand ?? (this._closeCommand = new DelegateCommand(Close, CanClose)); }

            set { this._closeCommand = value; }
        }

        public void Close()
        {
            this.eventAggregator.GetEvent<MessageHideEvent>().Publish(null);
        }

        public bool CanClose()
        {
            return _canClose;
        }

        private bool _canClose { get; set; }

        #endregion Close Command
        
        [InjectionConstructor]
        public MessageToolsViewModel(IUnityContainer container)
        {
            this.container = container;
            this.eventAggregator = this.container.Resolve<IEventAggregator>();
        }

        public void Initialize(string message, bool canClose)
        {
            this.Message = message;
            this._canClose = canClose;
        }

        public override void InitializeServices()
        {
            throw new NotImplementedException();
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}