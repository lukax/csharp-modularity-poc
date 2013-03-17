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

        #region Props

        private bool _isRestrictive;
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

        public bool IsRestrictive
        {
            get { return _isRestrictive; }
            set
            {
                _isRestrictive = value;
                OnPropertyChanged();
            }
        }

        #endregion Message

        #region Close Command

        private bool _canClose;
        private ICommand _closeCommand;

        public ICommand CloseCommand
        {
            get { return this._closeCommand ?? (this._closeCommand = new DelegateCommand(Close, () => CanClose)); }

            set { this._closeCommand = value; }
        }

        public bool CanClose
        {
            get { return _canClose; }
            set
            {
                _canClose = value;
                OnPropertyChanged();
            }
        }

        public void Close()
        {
            this.eventAggregator.GetEvent<MessageHideEvent>().Publish(null);
        }

        #endregion Close Command

        [InjectionConstructor]
        public MessageToolsViewModel(IUnityContainer container)
        {
            this.container = container;
            this.eventAggregator = this.container.Resolve<IEventAggregator>();
        }

        public void Initialize(string message, bool canClose, bool isRestrictive)
        {
            Message = message;
            CanClose = canClose;
            IsRestrictive = isRestrictive;
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