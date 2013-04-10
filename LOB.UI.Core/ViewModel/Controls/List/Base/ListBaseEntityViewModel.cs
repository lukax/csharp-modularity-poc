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

        private readonly IEventAggregator _eventAggregator;
        private readonly BackgroundWorker _worker = new BackgroundWorker();
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
        protected IRepository Repository { get; set; }
        public override UIOperation Operation { get; set; }

        [InjectionConstructor]
        protected ListBaseEntityViewModel(T entity, IRepository repository, IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            Repository = repository;
            Entity = entity;
            SearchCommand = new DelegateCommand(SearchExecute);
            IncludeCommand = new DelegateCommand(Include);
            AddCommand = new DelegateCommand(Save, CanSave);
            UpdateCommand = new DelegateCommand(Update, CanUpdate);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
            FetchCommand = new DelegateCommand(Fetch);
            CloseCommand = new DelegateCommand(Exit);
            Search = "";
        }

        private void Include(object o) { _eventAggregator.GetEvent<IncludeEvent>().Publish(Entity); }

        protected virtual void SearchExecute(object obj) { throw new NotImplementedException(); }

        private void Exit(object obj) { _eventAggregator.GetEvent<CloseViewEvent>().Publish(Operation); }

        public int UpdateInterval {
            get { return _updateInterval == default(int) ? 1000 : _updateInterval; }
            set { _updateInterval = value; }
        }

        public override void InitializeServices() {
            _worker.DoWork += WorkerUpdateList;
            _worker.RunWorkerAsync();
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
                _eventAggregator.GetEvent<NotificationEvent>().Publish(notification.Message(Strings.Notification_List_Updating).Progress(0));
                IList<T> localList = string.IsNullOrEmpty(Search) ? (Repository.GetList<T>()).ToList() : (Repository.GetList(SearchCriteria)).ToList();
                if(Entitys == null || !localList.SequenceEqual(Entitys)) {
                    Entitys = new ObservableCollection<T>(localList);
                    _eventAggregator.GetEvent<NotificationEvent>().Publish(notification.Message(Strings.Notification_List_Updating).Progress(100));
                }
                else _eventAggregator.GetEvent<NotificationEvent>().Publish(notification.Message(Strings.Notification_List_Updated).Progress(-1));
            } while(!worker.CancellationPending);
        }

        protected virtual void Save(object arg) { }

        protected virtual bool CanSave(object arg) { return Entity != null; }

        protected virtual void Update(object arg) { }

        protected virtual bool CanUpdate(object arg) { return Entity != null; }

        protected virtual void Delete(object arg) { Repository.Delete(Entity); }

        protected virtual bool CanDelete(object arg) { return Entity != null; }

        protected virtual void Fetch(object arg = null) { Entitys = new ObservableCollection<T>(Repository.GetList<T>().ToList()); }
        #region Implementation of IDisposable

        ~ListBaseEntityViewModel() { Dispose(false); }

        public override void Dispose() {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing) {
            if(_worker.WorkerSupportsCancellation) _worker.CancelAsync();
            if(disposing) _worker.Dispose();
        }

        #endregion
    }
}