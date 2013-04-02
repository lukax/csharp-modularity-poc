#region Usings

using System.Windows.Input;
using LOB.UI.Core.Events;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Infrastructure;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public class MessageToolViewModel : BaseViewModel {

        private readonly IUnityContainer container;
        private readonly IEventAggregator eventAggregator;
        #region Props

        public string Message { get; set; }

        public bool IsRestrictive { get; set; }

        #endregion Message
        #region CloseExecute Command

        private ICommand _closeCommand;

        public ICommand CloseCommand {
            get { return _closeCommand ?? (_closeCommand = new DelegateCommand(CloseExecute, () => CanClose)); }

            set { _closeCommand = value; }
        }

        public bool CanClose { get; set; }

        public void CloseExecute() { eventAggregator.GetEvent<MessageHideEvent>().Publish(null); }

        #endregion CloseExecute Command
        [InjectionConstructor]
        public MessageToolViewModel(IUnityContainer container) {
            Message = "Please wait...";
            this.container = container;
            eventAggregator = this.container.Resolve<IEventAggregator>();
        }

        private readonly UIOperation _operation = new UIOperation {Type = UIOperationType.MessageTool};
        public override UIOperation UIOperation {
            get { return _operation; }
        }

        public void Initialize(string message, bool canClose, bool isRestrictive) {
            Message = message;
            CanClose = canClose;
            IsRestrictive = isRestrictive;
        }

        public override void InitializeServices() { }

        public override void Refresh() { }

    }
}