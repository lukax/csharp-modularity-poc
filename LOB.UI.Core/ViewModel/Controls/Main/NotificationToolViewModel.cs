#region Usings

using System.Windows;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Core.Util;
using LOB.Domain.Logic;
using LOB.UI.Core.Events;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.Main;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using NullGuard;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    public class NotificationToolViewModel : BaseViewModel, INotificationToolViewModel {

        [AllowNull]
        public Notification Entity { get; set; }
        public MThreadObservableCollection<Notification> Entitys { get; set; }
        private readonly IEventAggregator _eventAggregator;
        public bool IsVisible {
            get { return _isVisible; }
            set {
                _isVisible = value;
                if(Entitys == null || Entitys.Count == 0) Visibility = Visibility.Collapsed;
                else Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public ICommand DismissCommand { get; set; }
        public Visibility Visibility { get; set; }
        public string Status {
            get { return string.Format("{0} {1}", Entitys.Count, Strings.ToolTip_Notifications); }
        }

        [InjectionConstructor]
        public NotificationToolViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            DismissCommand = new DelegateCommand(Dismiss);
            Entitys = new MThreadObservableCollection<Notification>();
            IsVisible = false;
            _eventAggregator.GetEvent<NotificationEvent>().Subscribe(NotificationListener);
        }
        private void Dismiss(object o) {
            if(Entity != null) Entitys.Remove(Entity);
            if(Entitys.Count == 0) IsVisible = false;
        }

        public override void InitializeServices() { }
        private void NotificationListener(Notification notification) {
            if(Entitys.Contains(notification)) Entitys[Entitys.IndexOf(notification)] = notification;
            else Entitys.Add(notification);
            IsVisible = true;
        }

        public override void Refresh() { }

        public override UIOperation Operation {
            get { return _operation; }
            set { _operation = value; }
        }

        private UIOperation _operation = new UIOperation {
            Type = UIOperationType.NotificationTool,
            State = UIOperationState.Tool
        };
        private bool _isVisible;

    }
}