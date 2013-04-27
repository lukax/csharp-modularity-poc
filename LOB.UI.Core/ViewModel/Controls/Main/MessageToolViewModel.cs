#region Usings

using System.ComponentModel.Composition;
using System.Windows.Input;
using LOB.UI.Contract.ViewModel.Controls.Main;
using LOB.UI.Core.ViewModel.Base;
using Microsoft.Practices.Prism.Commands;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    [Export(typeof(IMessageToolViewModel))]
    public class MessageToolViewModel : BaseViewModel, IMessageToolViewModel {
        //[Import] private IEventAggregator EventAggregator { get; set; }
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

        public void Initialize(string message, bool canClose, bool isRestrictive) {
            Message = message;
            CanClose = canClose;
            IsRestrictive = isRestrictive;
        }
    }
}