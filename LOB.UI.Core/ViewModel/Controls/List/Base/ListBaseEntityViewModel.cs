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
                    var converted = Convert.ToInt32(this.Search);
                    return this._searchCriteria ?? (arg => arg.Code == converted);
                }
                catch(FormatException) {
                    return arg => false;
                }
            }
            set { this._searchCriteria = value; }
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
            this._eventAggregator = eventAggregator;
            this.Repository = repository;
            this.Entity = entity;
            this.SaveCommand = new DelegateCommand(this.Save, this.CanSave);
            this.UpdateCommand = new DelegateCommand(this.Update, this.CanUpdate);
            this.DeleteCommand = new DelegateCommand(this.Delete, this.CanDelete);
            this.FetchCommand = new DelegateCommand(this.Fetch);
        }

        public int UpdateInterval {
            get { return this._updateInterval == default(int) ? 1000 : this._updateInterval; }
            set { this._updateInterval = value; }
        }

        public override void InitializeServices() {
            this._worker.DoWork += this.UpdateList;
            this._worker.WorkerSupportsCancellation = true;
            this._worker.WorkerReportsProgress = true;
            this._worker.RunWorkerAsync();
        }

        /// <summary>
        ///     Constantly update the list async every 1000 miliseconds
        /// </summary>
        private async void UpdateList(object sender, DoWorkEventArgs doWorkEventArgs) {
            //TODO: Dynamic set based on selected tab
            while(!this._worker.CancellationPending) {
                await Task.Delay(this.UpdateInterval);
                IList<T> localList;
                this._eventAggregator.GetEvent<ReportProgressEvent>()
                    .Publish(new Progress {Message = Strings.Progress_List_Updating, Percentage = 0});
                if(string.IsNullOrEmpty(this.Search)) localList = (this.Repository.GetList<T>()).ToList();
                else localList = (this.Repository.GetList(this.SearchCriteria)).ToList();
                if(this.Entitys == null || !localList.SequenceEqual(this.Entitys)) {
                    this.Entitys = localList;
                    this._eventAggregator.GetEvent<ReportProgressEvent>()
                        .Publish(new Progress {Message = Strings.Progress_List_Updating, Percentage = 100});
                }
                else
                    this._eventAggregator.GetEvent<ReportProgressEvent>()
                        .Publish(new Progress {Message = Strings.Progress_List_Updated});
            }
        }

        protected virtual void Save(object arg) {}

        protected virtual bool CanSave(object arg) {
            return this.Entity != null;
        }

        protected virtual void Update(object arg) {}

        protected virtual bool CanUpdate(object arg) {
            return this.Entity != null;
        }

        protected virtual void Delete(object arg) {
            this.Repository.Delete(this.Entity);
        }

        protected virtual bool CanDelete(object arg) {
            return this.Entity != null;
        }

        protected virtual void Fetch(object arg = null) {
            this.Entitys = this.Repository.GetList<T>().ToList();
        }

    }
}