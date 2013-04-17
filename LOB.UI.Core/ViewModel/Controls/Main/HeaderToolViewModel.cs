#region Usings

using System;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain.Logic;
using LOB.UI.Core.Events;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public class HeaderToolViewModel : BaseViewModel, IHeaderToolsViewModel {
        private readonly IServiceLocator _serviceLocator;
        private readonly IEventAggregator _eventAggregator;

        public ICommand DbTestConnectionCommand { get; set; }
        public ICommand OpenTabCommand { get; set; }

        public HeaderToolViewModel(IServiceLocator serviceLocator, IEventAggregator eventAggregator) {
            _serviceLocator = serviceLocator;
            _eventAggregator = eventAggregator;
            _notificationEvent = _eventAggregator.GetEvent<NotificationEvent>();
            DbTestConnectionCommand = new DelegateCommand(DbTestConnectionExecute);
            OpenTabCommand = new DelegateCommand(OpenTabExecute);
        }

        private void OpenTabExecute(object o) { _notificationEvent.Publish(new Notification(message: Strings.Notification_Implemented, state: NotificationState.Ok)); }

        private async void DbTestConnectionExecute(object arg) {
            var notification = new Notification();
            _notificationEvent.Publish(notification.Message(Strings.Notification_Dao_Connecting).Detail("").State(NotificationState.Info).Progress(-2));
            var uow = _serviceLocator.GetInstance<IUnityOfWork>();
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

        private ViewID _viewID = new ViewID {Type = ViewType.HeaderTool, State = ViewState.Internal};
        private readonly NotificationEvent _notificationEvent;
        public override ViewID ViewID {
            get { return _viewID; }
            set { _viewID = value; }
        }
        #region Implementation of IDisposable

        public override void Dispose() { GC.SuppressFinalize(this); }

        #endregion
    }
}