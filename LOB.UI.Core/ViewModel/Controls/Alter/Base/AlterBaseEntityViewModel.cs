#region Usings

using System.ComponentModel.Composition;
using System.Diagnostics;
using System.Windows.Input;
using GalaSoft.MvvmLight.Messaging;
using LOB.Dao.Interface;
using LOB.Domain.Base;
using LOB.UI.Core.Command;
using LOB.UI.Core.ViewModel.Base;
using LOB.UI.Interface;

#endregion

namespace LOB.UI.Core.ViewModel.Controls.Alter.Base
{
    public interface IAlterEntity
    {
        ICommand SaveChangesCommand { get; set; }
    }

    [InheritedExport]
    public class AlterBaseEntityViewModel<T> : BaseViewModel, IAlterEntity where T : BaseEntity
    {
        private CrudOperationType _typeOfOperation;
        #region Props

        private T _entity;

        protected T Entity
        {
            get { return _entity; }
            set
            {
                if (_entity == value) return;
                _entity = value;
                OnPropertyChanged();
            }
        }

        public int Code
        {
            get { return Entity != null ? Entity.Code : default(int); }
        }

        #endregion

        [ImportingConstructor]
        public AlterBaseEntityViewModel(T entity, IRepository repository)
        {
            _entity = entity;
            Repository = repository;
            SaveChangesCommand = new DelegateCommand(SaveChanges, CanSaveChanges);
            CancelCommand = new DelegateCommand(Cancel, CanCancel);
            QuickSearchCommand = new DelegateCommand(QuickSearch);
        }

        protected IRepository Repository { get; set; }
        public ICommand CancelCommand { get; set; }
        public ICommand SaveChangesCommand { get; set; }
        public ICommand QuickSearchCommand { get; set; }
        public virtual bool CanSaveChanges(object arg)
        {
            return Entity != null;
        }
        public virtual bool CanCancel(object arg)
        {
            return Entity != null;
        }
        public int? CancelIndex { get; set; }

        public virtual void SaveChanges(object arg)
        {
            Debug.Write("Saving changes...");
            Repository.SaveOrUpdate(Entity);
            Cancel(arg);
        }

        public virtual void Cancel(object arg)
        {
            Messenger.Default.Send(CancelIndex, "Cancel");
        }

        public virtual void QuickSearch(object arg)
        {
            Messenger.Default.Send(arg, "QuickSearchCommand");
        }

        public override void InitializeServices()
        {
            
        }
        public override void Refresh()
        {
            
        }
    }
}