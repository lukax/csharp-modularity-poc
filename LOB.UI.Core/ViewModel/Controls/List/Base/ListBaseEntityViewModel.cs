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
using LOB.UI.Core.Command;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.List.Base
{
    [InheritedExport]
    public class ListBaseEntityViewModel<T> : BaseViewModel where T : BaseEntity
    {
        #region Props

        protected IRepository Repository;

        private string _search;
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

        private Expression<Func<T, bool>> _searchCriteria;
        public virtual Expression<Func<T, bool>> SearchCriteria
        {
            get
            {
                try {
                    var converted = Convert.ToInt32(Search);
                    return _searchCriteria ?? (arg => arg.Code == converted);
                }
                catch (FormatException) {
                    return arg => false;
                }
            }
            set { _searchCriteria = value; }
        }

        public CrudOperationType OperationType { get; set; }

        private IList<T> _list;
        public IList<T> List
        {
            get { return _list; }
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }

        private T _entity;
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

        /// <summary>
        /// Constantly update the list async every 1000 miliseconds
        /// </summary>
        /// <param name="interval">Interval time in miliseconds</param>
        private async void UpdateList(int interval = 1000)
        {
            //TODO: Dynamic set true/false based on selected tab
            while (true)
            {
                IList<T> localList = null;
                localList = string.IsNullOrEmpty(Search) ? (await Repository.GetListAsync<T>()).ToList() : (await Repository.GetListAsync<T>(SearchCriteria)).ToList();
                if (List == null || !localList.SequenceEqual(List))
                {
                    List = localList;
                }

                await Task.Delay(interval);
            }
        }

        public virtual void Update(object arg)
        {
            Messenger.Default.Send(Entity, "Update");
            Debug.WriteLine("Updatecalled");
        }

        public virtual bool CanUpdate(object arg)
        {
            return Entity != null;
        }

        public virtual void Delete(object arg)
        {
            Messenger.Default.Send(arg ?? _entity, "Delete");
            Repository.Delete(_entity);
        }

        public virtual bool CanDelete(object arg)
        {
            return Entity != null;
        }

        public virtual void Fetch(object arg = null)
        {
            List = Repository.GetList<T>().ToList();
        }

        public override void InitializeServices()
        {
        }

        public override void Refresh()
        {
        }
    }
}