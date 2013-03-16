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

        private string _message = string.Empty;

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
            get { return this._closeCommand ?? (this._closeCommand = new DelegateCommand(this.Close)); }

            set { this._closeCommand = value; }
        }

        public void Close()
        {
            this.eventAggregator.GetEvent<MessageHideEvent>().Publish(null);
        }

        #endregion Close Command

        public MessageToolsViewModel(IUnityContainer container, string message = "Please wait...")
        {
            this.container = container;
            Message = message;
            this.eventAggregator = this.container.Resolve<IEventAggregator>();
        }

        public void Initialize(string message)
        {
            this.Message = message;
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