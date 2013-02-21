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
    public abstract class AlterBaseEntityViewModel<T> : BaseViewModel, IAlterEntity where T : BaseEntity
    {
        private CrudOperationType _typeOfOperation;
        protected IRepository Repository { get; set; }
        private T _entity;
        
        public ICommand SaveChangesCommand { get; set; }
        public ICommand CancelCommand { get; set; }
        public int? CancelIndex { get; set; }

        #region Props
        protected T Entity
        {
            get { return _entity; }
            set
            {
                _entity = value;
                OnPropertyChanged();
            }
        }
        public int Code
        {
            get { return _entity.Code; }
        }
        #endregion

        [ImportingConstructor]
        public AlterBaseEntityViewModel(T entity, IRepository repository)
        {
            Entity = entity;

            SaveChangesCommand = new DelegateCommand(SaveChanges, CanSaveChanges);
            CancelCommand = new DelegateCommand(Cancel);
        }

        public void SaveChanges(object arg)
        {
            Debug.Write("Saving changes...");
            Repository.SaveOrUpdate(Entity);
            Cancel(arg);
        }

        public bool CanSaveChanges(object arg)
        {
            return Entity != null;
        }

        public virtual void Cancel(object arg)
        {
            Messenger.Default.Send(CancelIndex, "Cancel");
        }

        public override void InitializeServices()
        {
            throw new System.NotImplementedException();
        }

        public override void Refresh()
        {
            throw new System.NotImplementedException();
        }
    }
}