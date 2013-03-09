#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface.Command;
using LOB.UI.Interface.ViewModel.Controls.List.Base;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base
{
    [InheritedExport]
    public abstract class ListBaseEntityViewModel<T> : BaseViewModel, IListBaseEntityViewModel where T : BaseEntity
    {
        #region Props

        protected IRepository Repository;
        private T _entity;
        private IList<T> _list;

        private string _search;

        private Expression<Func<T, bool>> _searchCriteria;

        public virtual string Search
        {
            get { return _search; }
            set
            {
                if (_search == value) return;
                _search = value;
                OnPropertyChanged();
            }
        }

        public virtual Expression<Func<T, bool>> SearchCriteria
        {
            get
            {
                try
                {
                    var converted = Convert.ToInt32(Search);
                    return _searchCriteria ?? (arg => arg.Code == converted);
                }
                catch (FormatException)
                {
                    return arg => false;
                }
            }
            set { _searchCriteria = value; }
        }

        public IList<T> Entitys
        {
            get { return _list; }
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }

        public T Entity
        {
            get { return _entity; }
            set
            {
                _entity = value;
                OnPropertyChanged();
            }
        }

        #endregion

        private int _updateInterval;

        [ImportingConstructor]
        public ListBaseEntityViewModel(T entity, IRepository repository)
        {
            Repository = repository;
            _entity = entity;
            UpdateCommand = new DelegateCommand(Update, CanUpdate);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
            FetchCommand = new DelegateCommand(Fetch);
            UpdateList();
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LoadFromFileCommand { get; set; }
        public ICommand SaveToFileCommand { get; set; }
        public ICommand FetchCommand { get; set; }

        public int UpdateInterval
        {
            get { return _updateInterval == default(int) ? 1000 : _updateInterval; }
            set { _updateInterval = value; }
        }

        /// <summary>
        ///     Constantly update the list async every 1000 miliseconds
        /// </summary>
        private async void UpdateList()
        {
            //TODO: Dynamic set true/false based on selected tab
            while (true)
            {
                await Task.Delay(UpdateInterval);
                IList<T> localList = null;
                localList = string.IsNullOrEmpty(Search)
                                ? (await Repository.GetListAsync<T>()).ToList()
                                : (await Repository.GetListAsync<T>(SearchCriteria)).ToList();
                if (Entitys == null || !localList.SequenceEqual(Entitys))
                {
                    Entitys = localList;
                }
            }
        }

        protected virtual void Update(object arg)
        {
            Messenger.Default.Send(Entity, "Update");
            Debug.WriteLine("Updatecalled");
        }

        protected virtual bool CanUpdate(object arg)
        {
            return Entity != null;
        }

        protected virtual void Delete(object arg)
        {
            Messenger.Default.Send(arg ?? _entity, "Delete");
            Repository.Delete(_entity);
        }

        protected virtual bool CanDelete(object arg)
        {
            return Entity != null;
        }

        protected virtual void Fetch(object arg = null)
        {
            Entitys = Repository.GetList<T>().ToList();
        }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }
    }
}