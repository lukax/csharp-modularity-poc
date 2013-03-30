#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using LOB.Core.Localization;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.Events;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Controls.List.Base;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base {
    public abstract class ListBaseEntityViewModel<T> : BaseViewModel, IListBaseEntityViewModel where T : BaseEntity {

        private readonly IEventAggregator _eventAggregator;
        private readonly BackgroundWorker _worker = new BackgroundWorker();
        private int _updateInterval;
        private Expression<Func<T, bool>> _searchCriteria;
        public virtual Expression<Func<T, bool>> SearchCriteria {
            get {
                try {
                    var converted = Convert.ToInt32(Search);
                    return _searchCriteria ?? (arg => arg.Code == converted);
                }
                catch(FormatException) {
                    return arg => false;
                }
            }
            set { _searchCriteria = value; }
        }
        public ICommand SaveCommand { get; set; }
        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand FetchCommand { get; set; }
        public T Entity { get; set; }
        public IList<T> Entitys { get; set; }
        public virtual string Search { get; set; }
        protected IRepository Repository { get; set; }

        [InjectionConstructor] protected ListBaseEntityViewModel(T entity, IRepository repository,
            IEventAggregator eventAggregator) {
            _eventAggregator = eventAggregator;
            Repository = repository;
            Entity = entity;
            SaveCommand = new DelegateCommand(Save, CanSave);
            UpdateCommand = new DelegateCommand(Update, CanUpdate);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
            FetchCommand = new DelegateCommand(Fetch);
        }

        public int UpdateInterval {
            get { return _updateInterval == default(int) ? 1000 : _updateInterval; }
            set { _updateInterval = value; }
        }

        public override void InitializeServices() {
            _worker.DoWork += UpdateList;
            _worker.WorkerSupportsCancellation = true;
            _worker.WorkerReportsProgress = true;
            _worker.RunWorkerAsync();
        }

        /// <summary>
        ///     Constantly update the list async every 1000 miliseconds
        /// </summary>
        private async void UpdateList(object sender, DoWorkEventArgs doWorkEventArgs) {
            //TODO: Dynamic set based on selected tab
            while(!_worker.CancellationPending) {
                await Task.Delay(UpdateInterval);
                IList<T> localList;
                _eventAggregator.GetEvent<ReportProgressEvent>()
                                .Publish(new Progress {Message = Strings.Progress_List_Updating, Percentage = 0});
                if(string.IsNullOrEmpty(Search)) localList = (Repository.GetList<T>()).ToList();
                else localList = (Repository.GetList(SearchCriteria)).ToList();
                if(Entitys == null || !localList.SequenceEqual(Entitys)) {
                    Entitys = localList;
                    _eventAggregator.GetEvent<ReportProgressEvent>()
                                    .Publish(new Progress {Message = Strings.Progress_List_Updating, Percentage = 100});
                }
                else
                    _eventAggregator.GetEvent<ReportProgressEvent>()
                                    .Publish(new Progress {Message = Strings.Progress_List_Updated});
            }
        }

        protected virtual void Save(object arg) {}

        protected virtual bool CanSave(object arg) {
            return Entity != null;
        }

        protected virtual void Update(object arg) {}

        protected virtual bool CanUpdate(object arg) {
            return Entity != null;
        }

        protected virtual void Delete(object arg) {
            Repository.Delete(Entity);
        }

        protected virtual bool CanDelete(object arg) {
            return Entity != null;
        }

        protected virtual void Fetch(object arg = null) {
            Entitys = Repository.GetList<T>().ToList();
        }

    }
}