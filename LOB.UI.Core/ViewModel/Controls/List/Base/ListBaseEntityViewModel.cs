#region Usings

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
    public interface IListEntity
    {
        ICommand UpdateCommand { get; set; }
        ICommand DeleteCommand { get; set; }
        ICommand LoadFromFileCommand { get; set; }
        ICommand SaveToFileCommand { get; set; }
        ICommand FetchCommand { get; set; }
    }

    [InheritedExport]
    public abstract class ListBaseEntityViewModel<T> : BaseViewModel, IListEntity where T : BaseEntity
    {
        public T LocalEntity;
        protected IRepository Repository;
        private T _entity;
        private IList<T> _list;

        [ImportingConstructor]
        public ListBaseEntityViewModel(T entity, IRepository repository)
        {
            Repository = repository;
            Entity = entity;
            UpdateCommand = new DelegateCommand(Update, CanUpdate);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
            FetchCommand = new DelegateCommand(Fetch);

            Fetch();
        }

        public CrudOperationType OperationType { get; set; }

        public IList<T> List
        {
            get { return _list; }
            set
            {
                _list = value;
                OnPropertyChanged();
            }
        }

        protected T Entity
        {
            get { return _entity; }
            set
            {
                _entity = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand DeleteCommand { get; set; }
        public ICommand LoadFromFileCommand { get; set; }
        public ICommand SaveToFileCommand { get; set; }
        public ICommand FetchCommand { get; set; }

        public void Update(object arg)
        {
            Messenger.Default.Send(Entity, "UpdateCommand");
            Debug.WriteLine("Updatecalled");
        }

        public bool CanUpdate(object arg)
        {
            return Entity != null;
        }

        public void Delete(object arg)
        {
            Messenger.Default.Send(arg ?? _entity, "DeleteCommand");
            Repository.Delete(_entity);
        }

        public bool CanDelete(object arg)
        {
            return Entity != null;
        }

        public void Fetch(object arg = null)
        {
            List = Repository.GetList<T>().ToList();
        }

        public override void InitializeServices()
        {
            throw new NotImplementedException();
        }

        public override void Refresh()
        {
            throw new NotImplementedException();
        }
    }
}