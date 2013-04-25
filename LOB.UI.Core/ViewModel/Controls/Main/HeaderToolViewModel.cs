#region Usings

using System;
using System.ComponentModel.Composition;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using LOB.Domain.Logic;
using LOB.UI.Contract.Command;
using LOB.UI.Contract.ViewModel.Controls.Main;
using LOB.UI.Core.Event;
using LOB.UI.Core.ViewModel.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    [Export(typeof(IHeaderToolViewModel))]
    public class HeaderToolViewModel : BaseViewModel, IHeaderToolViewModel {
        public ICommand DbTestConnectionCommand { get; set; }
        public ICommand OpenTabCommand { get; set; }
        [Import] private Lazy<IServiceLocator> LazyServiceLocator { get; set; }
        [Import] private IEventAggregator EventAggregator {
            set { _notificationEvent = value.GetEvent<NotificationEvent>(); }
        }

        public HeaderToolViewModel() {
            DbTestConnectionCommand = new DelegateCommand(DbTestConnectionExecute);
            OpenTabCommand = new DelegateCommand(OpenTabExecute);
        }

        private void OpenTabExecute(object o) { _notificationEvent.Publish(new Notification(Strings.Notification_Implemented, state: NotificationState.Ok)); }

        private async void DbTestConnectionExecute(object arg) {
            var notification = new Notification();
            _notificationEvent.Publish(notification.Message(Strings.Notification_Dao_Connecting).Detail("").State(NotificationState.Info).Progress(-2));
            var uow = LazyServiceLocator.Value.GetInstance<IUnityOfWork>();
            await Task.Run(() => {
                               uow.OnError +=
                                   (sender, args) =>
                                   notification.Message(args.Description).Detail(args.ErrorMessage).State(NotificationState.Error).Progress(-1);
                               if(uow.TestConnection())
                                   _notificationEvent.Publish(
                                       notification.Message(Strings.Notification_Dao_ConnectionSucessful).State(NotificationState.Ok).Progress(-1));
                           });
            _notificationEvent.Publish(notification);
        }

        public override void InitializeServices() { }

        public override void Refresh() { }

        private NotificationEvent _notificationEvent;
        #region Implementation of IDisposable

        public override void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}