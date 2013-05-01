#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Dao.Contract;
using LOB.Dao.Contract.Exception.Database;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.UI.Contract.Command;
using LOB.UI.Contract.Infrastructure;
using LOB.UI.Contract.ViewModel.Controls.List.Base;
using LOB.UI.Core.Event;
using LOB.UI.Core.Event.Infrastructure;
using LOB.UI.Core.Event.View;
using LOB.UI.Core.ViewModel.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Logging;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base {
    [InheritedExport, PartCreationPolicy(CreationPolicy.NonShared)]
    public abstract class ListBaseEntityViewModel<TEntity> : BaseViewModel, IListBaseEntityViewModel<TEntity>, IPartImportsSatisfiedNotification
        where TEntity : BaseEntity {
        private int _updateInterval;
        private Expression<Func<TEntity, bool>> _searchCriteria;
        private int _retrys;
        private bool _isInitialized;
        public virtual Expression<Func<TEntity, bool>> SearchCriteria {
            get {
                try {
                    var converted = Convert.ToInt32(SearchString);
                    return _searchCriteria ?? (arg => arg.Code == converted);
                } catch(FormatException) {
                    return arg => false;
                }
            }
            set { _searchCriteria = value; }
        }
        public ICommand SearchCommand { get; private set; }
        public ICommand IncludeCommand { get; private set; }
        public ICommand AddCommand { get; private set; }
        public ICommand UpdateCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand FetchCommand { get; private set; }
        public ICommand CloseCommand { get; private set; }
        public TEntity Entity { get; set; }
        public IEnumerable<TEntity> Entities { get; private set; }
        public string SearchString { get; set; }
        [Import] protected Lazy<Notification> Notification { get; private set; }
        [Import] protected Lazy<ILoggerFacade> Logger { get; private set; }
        [Import] protected Lazy<IRepository> Repository { get; private set; }
        [Import] protected Lazy<IEventAggregator> EventAggregator { get; private set; }
        protected NotificationEvent NotificationEvent {
            get { return EventAggregator.Value.GetEvent<NotificationEvent>(); }
        }

        protected ListBaseEntityViewModel(Lazy<IRepository> customRepository = null) { if(customRepository != null) Repository = customRepository; }

        public void OnImportsSatisfied() {
            SearchCommand = new DelegateCommand(SearchExecute, CanSearch);
            IncludeCommand = new DelegateCommand(IncludeExecute, CanInclude);
            AddCommand = new DelegateCommand(SaveExecute, CanSave);
            UpdateCommand = new DelegateCommand(UpdateExecute, CanUpdate);
            DeleteCommand = new DelegateCommand(DeleteExecute, CanDelete);
            FetchCommand = new DelegateCommand(FetchExecute, CanFetch);
            CloseCommand = new DelegateCommand(ExitExecute, CanExit);
            SearchString = "";
            ChangeState(ViewState.List);
            Lock();
        }
        public override void InitializeServices() {
            if(_isInitialized) return;
            Worker.DoWork += WorkerUpdateList;
            Worker.RunWorkerAsync();
            _isInitialized = true;
            Unlock();
        }

        protected virtual void SearchExecute(object obj) { if(Worker.CancellationPending) if(!Worker.IsBusy) Worker.RunWorkerAsync(); }
        protected bool CanSearch(object obj) { return IsUnlocked; }

        private void IncludeExecute(object o) { EventAggregator.Value.GetEvent<EntityIncludeEvent<TEntity>>().Publish(new EntityIncludePayload<TEntity>(Id, Entity)); }
        private bool CanInclude(object obj) { return IsUnlocked & !ReferenceEquals(Entity, null); }

        private void ExitExecute(object obj) { EventAggregator.Value.GetEvent<CloseViewEvent>().Publish(new CloseViewPayload(Id)); }
        private bool CanExit(object obj) { return IsUnlocked; }

        public int UpdateInterval {
            get { return _updateInterval == default(int) ? 1000 : _updateInterval; }
            set { _updateInterval = value; }
        }

        public override void Refresh() { SearchString = ""; }

        protected virtual void SaveExecute(object arg) { }

        protected virtual bool CanSave(object arg) { return Entity != null & IsUnlocked; }

        protected virtual void UpdateExecute(object arg) { }

        protected virtual bool CanUpdate(object arg) { return Entity != null & IsUnlocked; }

        protected virtual void DeleteExecute(object arg) { Repository.Value.Delete(Entity); }

        protected virtual bool CanDelete(object arg) { return Entity != null & IsUnlocked; }

        protected virtual void FetchExecute(object arg = null) {
            Entities = null;
            Refresh();
            if(!Worker.IsBusy) Worker.RunWorkerAsync();
            else {
                Worker.CancelAsync();
                Worker.RunWorkerAsync();
            }
        }

        protected virtual bool CanFetch(object obj) { return IsUnlocked; }

        private void WorkerUpdateList(object sender, DoWorkEventArgs doWorkEventArgs) {
            Lock();
            if(_retrys > 3) return;
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;
            //TODO: Dynamic set based on selected tab

            try {
                if(!worker.CancellationPending) {
                    NotificationEvent.Publish(Notification.Value.Message(Strings.Notification_List_Updating).Progress(10).State(NotificationType.Info));
                    IList<TEntity> localList = string.IsNullOrEmpty(SearchString)
                                                   ? (Repository.Value.GetAll<TEntity>()).ToList()
                                                   : (Repository.Value.GetAll(SearchCriteria)).ToList();
                    NotificationEvent.Publish(Notification.Value.Message(Strings.Notification_List_Updating).Progress(70));
                    if(Entities == null || !localList.SequenceEqual(Entities)) {
                        Entities = localList.AsEnumerable();
                        NotificationEvent.Publish(Notification.Value.Message(Strings.Notification_List_Updating).Progress(100));
                    }
                    else
                        NotificationEvent.Publish(Notification.Value.Message(Strings.Notification_List_Updated)
                                                              .Progress(-1)
                                                              .State(NotificationType.Ok));
                }
                _retrys = 0;
            } catch(DatabaseConnectionException ex) {
                Logger.Value.Log(ex.Message, Category.Exception, Priority.High);
                _retrys++;
                NotificationEvent.Publish(
                    Notification.Value.Message(Strings.Notification_Dao_ConnectionFailed).Detail(ex.Message).Progress(-2).Type(NotificationType.Error));
                Thread.Sleep(2000);
                WorkerUpdateList(sender, doWorkEventArgs);
            } catch(Exception e) {
                Logger.Value.Log(e.Message, Category.Exception, Priority.High);
                NotificationEvent.Publish(
                    Notification.Value.Message(Strings.Notification_RequisitionFailed).Progress(-1).Detail(e.Message).Type(NotificationType.Error));
            }
            Unlock();
        }

        protected override void Dispose(bool disposing) {
            base.Dispose(disposing);
            if(disposing) Repository.Value.Uow.Dispose();
        }
    }
}