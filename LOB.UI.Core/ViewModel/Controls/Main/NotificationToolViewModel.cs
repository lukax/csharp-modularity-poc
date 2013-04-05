#region Usings

using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public class NotificationToolViewModel : INotificationToolViewModel {

        public string Header { get; set; }

        public void InitializeServices() { }

        public void Refresh() { }

        private readonly UIOperation _operation = new UIOperation {
            Type = UIOperationType.NotificationTool,
            State = UIOperationState.Tool
        };
        public UIOperation Operation {
            get { return _operation; }
        }

    }
}