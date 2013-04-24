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
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.UI.Core.Event;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.Base;
using Microsoft.Practices.Prism.Events;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base {
    [InheritedExport]
    public abstract class ListBaseEntityViewModel<T> : BaseViewModel, IListBaseEntityViewModel<T> where T : BaseEntity {
        private int _updateInterval;
        private Expression<Func<T, bool>> _searchCriteria;
        public virtual Expression<Func<T, bool>> SearchCriteria {
            get {
                try {
                    var converted = Convert.ToInt32(Search);
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
        public T Entity { get; set; }
        public IEnumerable<T> Entities { get; private set; }
        public string Search { get; set; }
        [Import] protected Lazy<Notification> Notification { get; private set; }
        [Import] protected Lazy<IRepository> Repository { get; private set; }
        [Import] protected Lazy<IEventAggregator> EventAggregator { get; private set; }
        protected NotificationEvent NotificationEvent {
            get { return EventAggregator.Value.GetEvent<NotificationEvent>(); }
        }

        protected ListBaseEntityViewModel(Lazy<IRepository> customRepository = null) {
            if(customRepository != null) Repository = customRepository;
            SearchCommand = new DelegateCommand(SearchExecute, CanSearch);
            IncludeCommand = new DelegateCommand(IncludeExecute, CanInclude);
            AddCommand = new DelegateCommand(SaveExecute, CanSave);
            UpdateCommand = new DelegateCommand(UpdateExecute, CanUpdate);
            DeleteCommand = new DelegateCommand(DeleteExecute, CanDelete);
            FetchCommand = new DelegateCommand(FetchExecute, CanFetch);
            CloseCommand = new DelegateCommand(ExitExecute, CanExit);
            Search = "";
        }

        protected virtual void SearchExecute(object obj) { if(Worker.CancellationPending) if(!Worker.IsBusy) Worker.RunWorkerAsync(); }

        protected bool CanSearch(object obj) { return IsUnlocked; }

        private void IncludeExecute(object o) {
            //EventAggregator.GetEvent<EntityIncludeEvent>().Publish(Entity);
        }
        private bool CanInclude(object obj) { return IsUnlocked; }

        private void ExitExecute(object obj) {
            //EventAggregator.GetEvent<CloseViewEvent>().Publish(ViewModelInfo);
        }
        private bool CanExit(object obj) { return IsUnlocked; }

        public int UpdateInterval {
            get { return _updateInterval == default(int) ? 1000 : _updateInterval; }
            set { _updateInterval = value; }
        }

        public override void InitializeServices() {
            ChangeState(ViewState.List);
            Worker.DoWork += WorkerUpdateList;
            Worker.RunWorkerAsync();
        }

        public override void Refresh() { Search = ""; }

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
        }

        protected virtual bool CanFetch(object obj) { return IsUnlocked; }

        private void WorkerUpdateList(object sender, DoWorkEventArgs doWorkEventArgs) {
            Lock();
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;
            //TODO: Dynamic set based on selected tab
            Repository.Value.Uow.OnError += (o, s) => {
                                                NotificationEvent.Publish(
                                                    Notification.Value.Message(s.Description)
                                                                .Detail(s.ErrorMessage)
                                                                .Progress(-1)
                                                                .State(NotificationState.Error));
                                                //Worker.CancelAsync();
                                                Lock();
                                            };
            do {
                if(Repository.Value.Uow.TestConnection())
                    using(Repository.Value.Uow.BeginTransaction())
                        if(!worker.CancellationPending) {
                            EventAggregator.Value.GetEvent<NotificationEvent>()
                                           .Publish(
                                               Notification.Value.Message(Strings.Notification_List_Updating)
                                                           .Progress(10)
                                                           .State(NotificationState.Info));
                            IList<T> localList = string.IsNullOrEmpty(Search)
                                                     ? (Repository.Value.GetAll<T>()).ToList()
                                                     : (Repository.Value.GetAll(SearchCriteria)).ToList();
                            EventAggregator.Value.GetEvent<NotificationEvent>()
                                           .Publish(Notification.Value.Message(Strings.Notification_List_Updating).Progress(70));
                            if(Entities == null || !localList.SequenceEqual(Entities)) {
                                Entities = new List<T>(localList);
                                EventAggregator.Value.GetEvent<NotificationEvent>()
                                               .Publish(Notification.Value.Message(Strings.Notification_List_Updating).Progress(100));
                            }
                            else
                                EventAggregator.Value.GetEvent<NotificationEvent>()
                                               .Publish(
                                                   Notification.Value.Message(Strings.Notification_List_Updated)
                                                               .Progress(-1)
                                                               .State(NotificationState.Ok));
                            Unlock();
                        }
                Thread.Sleep(2000);
            } while(!Worker.CancellationPending);
        }
        #region Implementation of IDisposable

        ~ListBaseEntityViewModel() { Dispose(false); }
        public override void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        private void Dispose(bool disposing) {
            if(Worker.WorkerSupportsCancellation) Worker.CancelAsync();
            if(!disposing) return;
            Worker.Dispose();
            Repository.Value.Uow.Dispose();
        }

        #endregion
    }
}