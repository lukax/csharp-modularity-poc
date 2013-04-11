#region Usings

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.Domain.Logic;
using LOB.UI.Core.Events;
using LOB.UI.Core.Events.View;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.Infrastructure;
using LOB.UI.Interface.ViewModel.Controls.List.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using NullGuard;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base {
    public abstract class ListBaseEntityViewModel<T> : BaseViewModel, IListBaseEntityViewModel where T : BaseEntity {

        private int _updateInterval;
        private Expression<Func<T, bool>> _searchCriteria;
        //private UIOperation _previousOperation;
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
        public ICommand SearchCommand { get; set; }
        public ICommand IncludeCommand { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand FetchCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        [AllowNull]
        public T Entity { get; set; }
        [AllowNull]
        public ObservableCollection<T> Entitys { get; set; }
        public string Search { get; set; }
        protected IRepository Repository { get; private set; }
        protected IEventAggregator EventAggregator { get; private set; }
        protected BackgroundWorker Worker { get; private set; }
        public override UIOperation Operation { get; set; }

        [InjectionConstructor]
        protected ListBaseEntityViewModel(T entity, IRepository repository, IEventAggregator eventAggregator) {
            EventAggregator = eventAggregator;
            Repository = repository;
            Entity = entity;
            Worker= new BackgroundWorker();
            SearchCommand = new DelegateCommand(SearchExecute);
            IncludeCommand = new DelegateCommand(Include);
            AddCommand = new DelegateCommand(Save, CanSave);
            UpdateCommand = new DelegateCommand(Update, CanUpdate);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
            FetchCommand = new DelegateCommand(Fetch);
            CloseCommand = new DelegateCommand(Exit);
            Search = "";
        }

        private void Include(object o) { EventAggregator.GetEvent<IncludeEvent>().Publish(Entity); }

        protected virtual void SearchExecute(object obj) { throw new NotImplementedException(); }

        private void Exit(object obj) { EventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        public int UpdateInterval {
            get { return _updateInterval == default(int) ? 1000 : _updateInterval; }
            set { _updateInterval = value; }
        }

        public override void InitializeServices() {
            Worker.DoWork += WorkerUpdateList;
            Worker.RunWorkerAsync();
        }

        /// <summary>
        ///     Constantly update the list async every 1000 miliseconds
        /// </summary>
        private void WorkerUpdateList(object sender, DoWorkEventArgs doWorkEventArgs) {
            var worker = sender as BackgroundWorker;
            if(worker == null) return;
            worker.WorkerSupportsCancellation = true;
            //worker.WorkerReportsProgress = true;
            //TODO: Dynamic set based on selected tab

            var notification = new Notification();
            do {
                Thread.Sleep(2000);
                EventAggregator.GetEvent<NotificationEvent>().Publish(notification.Message(Strings.Notification_List_Updating).Progress(0));
                IList<T> localList = string.IsNullOrEmpty(Search) ? (Repository.GetAll<T>()).ToList() : (Repository.GetAll(SearchCriteria)).ToList();
                if(Entitys == null || !localList.SequenceEqual(Entitys)) {
                    Entitys = new ObservableCollection<T>(localList);
                    EventAggregator.GetEvent<NotificationEvent>().Publish(notification.Message(Strings.Notification_List_Updating).Progress(100));
                }
                else EventAggregator.GetEvent<NotificationEvent>().Publish(notification.Message(Strings.Notification_List_Updated).Progress(-1));
            } while(!worker.CancellationPending);
        }

        protected virtual void Save(object arg) { }

        protected virtual bool CanSave(object arg) { return Entity != null; }

        protected virtual void Update(object arg) { }

        protected virtual bool CanUpdate(object arg) { return Entity != null; }

        protected virtual void Delete(object arg) { Repository.Delete(Entity); }

        protected virtual bool CanDelete(object arg) { return Entity != null; }

        protected virtual void Fetch(object arg = null) { Entitys = new ObservableCollection<T>(Repository.GetAll<T>().ToList()); }
        
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
        }

        #endregion
    }
}