#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Domain.Logic;
using LOB.UI.Contract.Command;
using LOB.UI.Contract.ViewModel.Controls.Main;
using LOB.UI.Core.Event;
using LOB.UI.Core.ViewModel.Base;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    [Export(typeof(INotificationToolViewModel)), PartCreationPolicy(CreationPolicy.Shared)]
    public class NotificationToolViewModel : BaseViewModel, INotificationToolViewModel, IPartImportsSatisfiedNotification {
        public Notification Entity { get; set; }
        public IList<Notification> Entitys { get; set; }
        public bool IsVisible {
            get { return Visibility == Visibility.Visible; }
            set { Visibility = value ? Visibility.Visible : Visibility.Collapsed; }
        }
        public ICommand DismissCommand { get; set; }
        public Visibility Visibility { get; private set; }
        public string Status {
            get { return string.Format("{0} {1}", Entitys.Count, Strings.UI_ToolTip_Notifications); }
        }
        [Import] public IEventAggregator EventAggregator {
            set { value.GetEvent<NotificationEvent>().Subscribe(NotificationListener); }
        }

        public void OnImportsSatisfied() {
            DismissCommand = new DelegateCommand(DismissNotification);
            Entitys = new List<Notification>();
            IsVisible = false;
            InitWorker();
        }

        [Export("NotificationSystem", typeof(Action<Notification>))]
        private void NotificationListener(Notification notification) {
            notification.Time = DateTime.Now;
            if(Entitys.Contains(notification)) Entitys[Entitys.IndexOf(notification)] = notification;
            else Entitys.Add(notification);
            IsVisible = true;
        }

        private void DismissNotification(object o) {
            if(Equals(o, "All")) Entitys.Clear();
            else if(Entity != null) Entitys.Remove(Entity);
            if(Entitys.Count == 0) IsVisible = false;
        }

        private void InitWorker() {
            Worker.DoWork += AutoCleanEntititys;
            Worker.RunWorkerAsync();
        }

        private void AutoCleanEntititys(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;
            do {
                Thread.Sleep(5000);
                var currentStack = Entitys.ToList(); //Thread Safe
                foreach(var notification in currentStack) if(notification.Type == NotificationType.Ok) if(notification.Time.AddSeconds(10) < DateTime.Now) Entitys.Remove(notification);
            } while(!Worker.CancellationPending);
        }
    }
}