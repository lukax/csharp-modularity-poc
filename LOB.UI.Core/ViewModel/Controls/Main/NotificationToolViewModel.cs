﻿#region Usings

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Threading;
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

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Main {
    [Export(typeof(INotificationToolViewModel))]
    public class NotificationToolViewModel : BaseViewModel, INotificationToolViewModel {
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        //[AllowNull]
        public Notification Entity { get; set; }
        public MThreadObservableCollection<Notification> Entitys { get; set; }
        private readonly IEventAggregator _eventAggregator;
        public bool IsVisible {
            get { return _isVisible; }
            set {
                _isVisible = value;
                //if(Entities == null || Entities.Count == 0) Visibility = Visibility.Collapsed;
                //else 
                Visibility = value ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        public ICommand DismissCommand { get; set; }
        public Visibility Visibility { get; set; }
        public string Status {
            get { return string.Format("{0} {1}", Entitys.Count, Strings.UI_ToolTip_Notifications); }
        }

        [ImportingConstructor]
        public NotificationToolViewModel(IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            DismissCommand = new DelegateCommand(DismissNotification);
            Entitys = new MThreadObservableCollection<Notification>();
            IsVisible = false;
            _eventAggregator.GetEvent<NotificationEvent>().Subscribe(NotificationListener);
            InitWorker();
        }

        private void DismissNotification(object o) {
            if(Equals(o, "All")) Entitys.Clear();
            else if(Entity != null) Entitys.Remove(Entity);
            if(Entitys.Count == 0) IsVisible = false;
        }

        public override void InitializeServices() { }
        private void NotificationListener(Notification notification) {
            notification.Time = DateTime.Now.ToShortTimeString();
            if(Entitys.Contains(notification)) Entitys[Entitys.IndexOf(notification)] = notification;
            else Entitys.Add(notification);
            IsVisible = true;
        }

        private void InitWorker() {
            _worker.DoWork += AutoCleanEntititys;
            _worker.RunWorkerAsync();
        }

        private void AutoCleanEntititys(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;
            do {
                Thread.Sleep(10000);
                var currentStack = Entitys.ToList(); //Thread Safe
                foreach(var notification in currentStack) if(notification.State == NotificationState.Ok) Entitys.Remove(notification);
            } while(!_worker.CancellationPending);
        }

        public override void Refresh() { }

        public override ViewModelState ViewModelState {
            get { return _viewModelState; }
            set { _viewModelState = value; }
        }

        private ViewModelState _viewModelState = new ViewModelState {ViewState = ViewState.Other};
        private bool _isVisible;
        #region Implementation of IDisposable

        ~NotificationToolViewModel() { Dispose(false); }

        private void Dispose(bool disposing) {
            _worker.CancelAsync();
            if(disposing) _worker.Dispose();
        }

        public override void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}